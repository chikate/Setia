<template>
  <div class="engine-wrap">
    <canvas ref="canvas" class="canvas" />
    <div class="overlay">
      <div>W/A/S/D: Move · Q/E: Up/Down · Mouse: Look · R: Reset</div>
      <div>
        Loaded OBJ: <strong>{{ modelName }}</strong>
      </div>
    </div>
  </div>
</template>

<script setup>
defineOptions({
  name: "Space Engine",
  icon: "🌌",
});

const canvas = ref(null);
const modelName = ref("none");

// ---------- Simple WebGPU Engine Core (single-file for clarity) ----------
// This SFC contains a compact but well-structured engine core: device init,
// renderer, mesh upload, OBJ loader, simple scene/transform, camera, and input.

// --- Utilities ---
function mat4Identity() {
  return new Float32Array([1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1]);
}
function mat4Perspective(fovy, aspect, near, far) {
  const f = 1.0 / Math.tan(fovy / 2);
  const nf = 1 / (near - far);
  const out = new Float32Array(16);
  out[0] = f / aspect;
  out[5] = f;
  out[10] = (far + near) * nf;
  out[11] = -1;
  out[14] = 2 * far * near * nf;
  return out;
}
function mat4Translate(out, v) {
  out[12] += v[0];
  out[13] += v[1];
  out[14] += v[2];
}
function mat4Multiply(a, b) {
  const out = new Float32Array(16);
  for (let i = 0; i < 4; i++) {
    for (let j = 0; j < 4; j++) {
      out[i * 4 + j] =
        a[i * 4 + 0] * b[0 * 4 + j] +
        a[i * 4 + 1] * b[1 * 4 + j] +
        a[i * 4 + 2] * b[2 * 4 + j] +
        a[i * 4 + 3] * b[3 * 4 + j];
    }
  }
  return out;
}
function rad(deg) {
  return (deg * Math.PI) / 180;
}

// --- Simple OBJ Loader (no external deps) ---
function parseOBJ(text) {
  const lines = text.split("\n");
  const positions = [];
  const normals = [];
  const uvs = [];
  const faces = [];

  for (let raw of lines) {
    const line = raw.trim();
    if (!line || line.startsWith("#")) continue;
    const parts = line.split(/\s+/);
    const type = parts[0];
    if (type === "v") positions.push(parts.slice(1).map(Number));
    else if (type === "vn") normals.push(parts.slice(1).map(Number));
    else if (type === "vt") uvs.push(parts.slice(1).map(Number));
    else if (type === "f") {
      // faces can reference v/vt/vn, v//vn, v/vt
      const verts = parts.slice(1).map((s) => {
        const [vidx, vtidx, vnidx] = s
          .split("/")
          .map((x) => (x === "" ? undefined : Number(x)));
        return {
          v: vidx ? vidx - 1 : undefined,
          vt: vtidx ? vtidx - 1 : undefined,
          vn: vnidx ? vnidx - 1 : undefined,
        };
      });
      // triangulate if quad/polygon
      for (let i = 1; i < verts.length - 1; i++)
        faces.push([verts[0], verts[i], verts[i + 1]]);
    }
  }

  // build interleaved arrays and indices
  const vertexMap = new Map();
  const vertices = [];
  const indices = [];
  let idx = 0;
  for (const f of faces) {
    for (const vref of f) {
      const key = `${vref.v}/${vref.vt}/${vref.vn}`;
      if (!vertexMap.has(key)) {
        const pos = positions[vref.v] || [0, 0, 0];
        const uv = vref.vt !== undefined ? uvs[vref.vt] : [0, 0];
        const nrm = vref.vn !== undefined ? normals[vref.vn] : [0, 0, 1];
        vertices.push(...pos, ...nrm, ...uv);
        vertexMap.set(key, idx++);
      }
      indices.push(vertexMap.get(key));
    }
  }

  return {
    vertexData: new Float32Array(vertices),
    indexData: new Uint32Array(indices),
    vertexCount: idx,
    indexCount: indices.length,
  };
}

// --- GPU Device + Helpers ---
async function initWebGPU(canvasEl) {
  if (!navigator.gpu)
    throw new Error(
      "WebGPU not supported. Use Chrome/Edge Canary or enable it."
    );
  const adapter = await navigator.gpu.requestAdapter();
  if (!adapter) throw new Error("Failed to get GPU adapter");
  const device = await adapter.requestDevice();
  const context = canvasEl.getContext("webgpu");
  return { device, adapter, context };
}

function configureSwapChain(context, device, format) {
  context.configure({
    device,
    format,
    alphaMode: "opaque",
  });
}

// --- Simple Camera ---
class Camera {
  constructor() {
    this.position = [0, 1.2, 4];
    this.yaw = 0;
    this.pitch = 0;
    this.fov = rad(60);
    this.aspect = 1;
    this.near = 0.1;
    this.far = 100.0;
  }
  viewMatrix() {
    // build a simple lookAt from yaw/pitch
    const cosPitch = Math.cos(this.pitch);
    const dir = [
      Math.sin(this.yaw) * cosPitch,
      Math.sin(this.pitch),
      Math.cos(this.yaw) * cosPitch,
    ];
    const up = [0, 1, 0];
    // compute lookAt matrix quickly (camera space -> world) then invert
    const target = [
      this.position[0] + dir[0],
      this.position[1] + dir[1],
      this.position[2] + dir[2],
    ];
    // use a simple lookAt implementation
    const z0 = this.position[0] - target[0];
    const z1 = this.position[1] - target[1];
    const z2 = this.position[2] - target[2];
    let zlen = Math.hypot(z0, z1, z2) || 1;
    const zx = z0 / zlen,
      zy = z1 / zlen,
      zz = z2 / zlen;
    const xx = up[1] * zz - up[2] * zy;
    const xy = up[2] * zx - up[0] * zz;
    const xz = up[0] * zy - up[1] * zx;
    const xlen = Math.hypot(xx, xy, xz) || 1;
    const ux = zx * xy - zz * xx; // cross z x x = up dot? skip exactness for brevity
    const uxN = ux; // not used further; we'll create a view matrix by building a rotation + translate

    const xnx = xx / xlen,
      xny = xy / xlen,
      xnz = xz / xlen;
    const ynx = zy * xnz - zz * xny; // cross z x x gives y. This is approximate but sufficient here.
    const view = new Float32Array(16);
    view[0] = xnx;
    view[1] = ynx;
    view[2] = zx;
    view[3] = 0;
    view[4] = xny;
    view[5] = /*...*/ 1; // fallback
    // For clarity and safety we'll use a robust approach: compute mat4 from lookAt via helper below
    return lookAt(this.position, target, up);
  }
}

// robust lookAt
function lookAt(eye, center, up) {
  const z0 = eye[0] - center[0];
  const z1 = eye[1] - center[1];
  const z2 = eye[2] - center[2];
  let zlen = Math.hypot(z0, z1, z2) || 1;
  const zx = z0 / zlen,
    zy = z1 / zlen,
    zz = z2 / zlen;
  const xx = up[1] * zz - up[2] * zy;
  const xy = up[2] * zx - up[0] * zz;
  const xz = up[0] * zy - up[1] * zx;
  const xlen = Math.hypot(xx, xy, xz) || 1;
  const xnx = xx / xlen,
    xny = xy / xlen,
    xnz = xz / xlen;
  const ynx = zy * xnz - zz * xny;
  const yny = zz * xnx - zx * xnz;
  const ynz = zx * xny - zy * xnx;
  const out = new Float32Array(16);
  out[0] = xnx;
  out[1] = ynx;
  out[2] = zx;
  out[3] = 0;
  out[4] = xny;
  out[5] = yny;
  out[6] = zy;
  out[7] = 0;
  out[8] = xnz;
  out[9] = ynz;
  out[10] = zz;
  out[11] = 0;
  out[12] = -(xnx * eye[0] + xny * eye[1] + xnz * eye[2]);
  out[13] = -(ynx * eye[0] + yny * eye[1] + ynz * eye[2]);
  out[14] = -(zx * eye[0] + zy * eye[1] + zz * eye[2]);
  out[15] = 1;
  return out;
}

// --- Renderer ---
class Renderer {
  constructor(device, context, format) {
    this.device = device;
    this.context = context;
    this.format = format;
    this.pipeline = null;
    this.depthTexture = null;
    this.uniformBuffer = null;
    this.bindGroup = null;
    this.modelBindGroupLayout = null;

    this.createPipeline();
  }

  createPipeline() {
    // Simple WGSL vertex + fragment
    const vs = `
struct Uniforms { mvp: mat4x4<f32>; model: mat4x4<f32>; lightDir: vec4<f32>; };
@binding(0) @group(0) var<uniform> uniforms : Uniforms;

struct VertexInput { @location(0) pos: vec3<f32>; @location(1) nrm: vec3<f32>; @location(2) uv: vec2<f32>; };
struct VertexOutput { @builtin(position) Position: vec4<f32>; @location(0) vNormal: vec3<f32>; @location(1) vPos: vec3<f32>; };

@vertex
fn main(input: VertexInput) -> VertexOutput {
  var out: VertexOutput;
  let worldPos = uniforms.model * vec4<f32>(input.pos, 1.0);
  out.Position = uniforms.mvp * vec4<f32>(input.pos, 1.0);
  out.vNormal = (uniforms.model * vec4<f32>(input.nrm, 0.0)).xyz;
  out.vPos = worldPos.xyz;
  return out;
}
`;
    const fs = `
@fragment
fn main(@location(0) vNormal: vec3<f32>, @location(1) vPos: vec3<f32>) -> @location(0) vec4<f32> {
  let n = normalize(vNormal);
  let light = normalize(vec3<f32>(0.5, 1.0, 0.3));
  let diff = max(dot(n, light), 0.0);
  let base = vec3<f32>(0.6, 0.7, 0.9);
  let col = base * (0.2 + diff * 0.8);
  return vec4<f32>(col, 1.0);
}
`;

    const shaderModuleVS = this.device.createShaderModule({ code: vs });
    const shaderModuleFS = this.device.createShaderModule({ code: fs });

    // uniform layout
    const uniformBindGroupLayout = this.device.createBindGroupLayout({
      entries: [
        {
          binding: 0,
          visibility: GPUShaderStage.VERTEX | GPUShaderStage.FRAGMENT,
          buffer: { type: "uniform" },
        },
      ],
    });

    this.pipeline = this.device.createRenderPipeline({
      layout: this.device.createPipelineLayout({
        bindGroupLayouts: [uniformBindGroupLayout],
      }),
      vertex: {
        module: shaderModuleVS,
        entryPoint: "main",
        buffers: [
          {
            arrayStride: (3 + 3 + 2) * 4,
            attributes: [
              { shaderLocation: 0, offset: 0, format: "float32x3" },
              { shaderLocation: 1, offset: 3 * 4, format: "float32x3" },
              { shaderLocation: 2, offset: 6 * 4, format: "float32x2" },
            ],
          },
        ],
      },
      fragment: {
        module: shaderModuleFS,
        entryPoint: "main",
        targets: [{ format: this.format }],
      },
      primitive: { topology: "triangle-list", cullMode: "back" },
      depthStencil: {
        format: "depth24plus",
        depthWriteEnabled: true,
        depthCompare: "less",
      },
    });

    this.uniformBindGroupLayout = uniformBindGroupLayout;
  }

  ensureDepthTexture(device, width, height) {
    if (
      this.depthTexture &&
      this.depthSize &&
      this.depthSize[0] === width &&
      this.depthSize[1] === height
    )
      return;
    this.depthSize = [width, height];
    this.depthTexture = device.createTexture({
      size: { width, height },
      format: "depth24plus",
      usage: GPUTextureUsage.RENDER_ATTACHMENT,
    });
  }
}

// --- Mesh wrapper ---
class Mesh {
  constructor(device, vertexData, indexData) {
    this.device = device;
    this.vertexCount = vertexData.length / 8; // 3 pos, 3 nrm, 2 uv
    this.indexCount = indexData.length;
    this.vertexBuffer = device.createBuffer({
      size: vertexData.byteLength,
      usage: GPUBufferUsage.VERTEX | GPUBufferUsage.COPY_DST,
    });
    this.indexBuffer = device.createBuffer({
      size: indexData.byteLength,
      usage: GPUBufferUsage.INDEX | GPUBufferUsage.COPY_DST,
    });
    device.queue.writeBuffer(
      this.vertexBuffer,
      0,
      vertexData.buffer,
      vertexData.byteOffset,
      vertexData.byteLength
    );
    device.queue.writeBuffer(
      this.indexBuffer,
      0,
      indexData.buffer,
      indexData.byteOffset,
      indexData.byteLength
    );
  }
}

// --- Scene / Entity ---
class Entity {
  constructor(mesh = null) {
    this.mesh = mesh;
    this.position = [0, 0, 0];
    this.rotation = [0, 0, 0];
    this.scale = [1, 1, 1];
  }
  modelMatrix() {
    let m = mat4Identity();
    mat4Translate(m, this.position);
    return m;
  }
}

// --- App state and main setup ---
let engine = null;

onMounted(async () => {
  const canvasEl = canvas.value;
  canvasEl.width = Math.floor(window.innerWidth * devicePixelRatio);
  canvasEl.height = Math.floor(window.innerHeight * devicePixelRatio);
  canvasEl.style.width = window.innerWidth + "px";
  canvasEl.style.height = window.innerHeight + "px";

  try {
    const { device, context } = await initWebGPU(canvasEl);
    const format = navigator.gpu.getPreferredCanvasFormat();
    configureSwapChain(context, device, format);
    const renderer = new Renderer(device, context, format);

    // create uniform buffer: mvp(16) + model(16) + lightDir(4) = 36 floats -> 36*4=144 bytes
    const uniformBuffer = device.createBuffer({
      size: 16 * 4 * 2 + 16,
      usage: GPUBufferUsage.UNIFORM | GPUBufferUsage.COPY_DST,
    });
    const uniformBindGroup = device.createBindGroup({
      layout: renderer.uniformBindGroupLayout,
      entries: [{ binding: 0, resource: { buffer: uniformBuffer } }],
    });

    renderer.uniformBuffer = uniformBuffer;
    renderer.bindGroup = uniformBindGroup;

    // simple camera
    const cam = new Camera();
    function resize() {
      canvasEl.width = Math.floor(window.innerWidth * devicePixelRatio);
      canvasEl.height = Math.floor(window.innerHeight * devicePixelRatio);
      canvasEl.style.width = window.innerWidth + "px";
      canvasEl.style.height = window.innerHeight + "px";
      cam.aspect = canvasEl.width / canvasEl.height;
      configureSwapChain(context, device, format);
    }
    window.addEventListener("resize", resize);

    // sample cube fallback mesh
    const cubeObjText = `# cube
v -1 -1 -1
v 1 -1 -1
v 1 1 -1
v -1 1 -1
v -1 -1 1
v 1 -1 1
v 1 1 1
v -1 1 1
vn 0 0 -1
vn 0 0 1
vn 0 -1 0
vn 0 1 0
vn -1 0 0
vn 1 0 0
f 1//1 2//1 3//1 4//1
f 5//2 8//2 7//2 6//2
f 1//3 5//3 6//3 2//3
f 2//6 6//6 7//6 3//6
f 3//4 7//4 8//4 4//4
f 5//5 1//5 4//5 8//5`;
    const cubeParsed = parseOBJ(cubeObjText);
    const cubeMesh = new Mesh(
      device,
      cubeParsed.vertexData,
      cubeParsed.indexData
    );

    // create entity
    const entity = new Entity(cubeMesh);
    entity.position = [0, 0, 0];

    // input
    const input = {
      forward: 0,
      right: 0,
      up: 0,
      yaw: 0,
      pitch: 0,
      mouseDown: false,
    };
    const keys = {};
    window.addEventListener("keydown", (e) => {
      keys[e.key.toLowerCase()] = true;
      if (e.key.toLowerCase() === "r") {
        entity.position = [0, 0, 0];
        entity.rotation = [0, 0, 0];
      }
    });
    window.addEventListener("keyup", (e) => {
      keys[e.key.toLowerCase()] = false;
    });

    canvasEl.addEventListener("mousedown", (e) => {
      input.mouseDown = true;
      canvasEl.requestPointerLock?.();
    });
    document.addEventListener("mouseup", (e) => {
      input.mouseDown = false;
      document.exitPointerLock?.();
    });
    document.addEventListener("pointerlockchange", () => {});
    document.addEventListener("mousemove", (e) => {
      if (document.pointerLockElement === canvasEl || input.mouseDown) {
        cam.yaw -= e.movementX * 0.002;
        cam.pitch -= e.movementY * 0.002;
        cam.pitch = Math.max(
          -Math.PI / 2 + 0.01,
          Math.min(Math.PI / 2 - 0.01, cam.pitch)
        );
      }
    });

    // simple OBJ drag-and-drop
    canvasEl.addEventListener("dragover", (e) => {
      e.preventDefault();
    });
    canvasEl.addEventListener("drop", (e) => {
      e.preventDefault();
      const f = e.dataTransfer.files[0];
      if (!f) return;
      const reader = new FileReader();
      reader.onload = () => {
        const txt = reader.result;
        try {
          const parsed = parseOBJ(txt);
          const mesh = new Mesh(device, parsed.vertexData, parsed.indexData);
          entity.mesh = mesh;
          modelName.value = f.name;
        } catch (err) {
          console.error("OBJ parse error", err);
        }
      };
      reader.readAsText(f);
    });

    // animation loop
    let last = performance.now();
    function frame(now) {
      const dt = Math.min((now - last) / 1000, 0.05);
      last = now;

      // input-derived movement
      const speed = 3.0;
      const forward = (keys["w"] ? 1 : 0) - (keys["s"] ? 1 : 0);
      const right = (keys["d"] ? 1 : 0) - (keys["a"] ? 1 : 0);
      const up = (keys["q"] ? 1 : 0) - (keys["e"] ? 1 : 0);
      // camera-forward local move
      const cx = Math.sin(cam.yaw);
      const cz = Math.cos(cam.yaw);
      cam.position[0] += (cx * forward + cz * right) * speed * dt * -1;
      cam.position[2] += (cz * forward - cx * right) * speed * dt;
      cam.position[1] += up * speed * dt;

      // rotate entity slowly for demo
      entity.rotation[1] += dt * 0.6;

      // render
      const device = renderer.device;
      renderer.ensureDepthTexture(device, canvasEl.width, canvasEl.height);
      const commandEncoder = device.createCommandEncoder();
      const textureView = renderer.context.getCurrentTexture().createView();

      const renderPass = commandEncoder.beginRenderPass({
        colorAttachments: [
          {
            view: textureView,
            loadOp: "clear",
            clearValue: { r: 0.02, g: 0.02, b: 0.03, a: 1 },
            storeOp: "store",
          },
        ],
        depthStencilAttachment: {
          view: renderer.depthTexture.createView(),
          depthLoadOp: "clear",
          depthClearValue: 1.0,
          depthStoreOp: "store",
        },
      });

      renderPass.setPipeline(renderer.pipeline);

      // compute mvp and write uniform buffer
      const proj = mat4Perspective(cam.fov, cam.aspect, cam.near, cam.far);
      const view = lookAt(
        cam.position,
        [
          cam.position[0] + Math.sin(cam.yaw) * Math.cos(cam.pitch),
          cam.position[1] + Math.sin(cam.pitch),
          cam.position[2] + Math.cos(cam.yaw) * Math.cos(cam.pitch),
        ],
        [0, 1, 0]
      );
      const model = entity.modelMatrix();
      const mvp = mat4Multiply(proj, view);
      const mvpModel = new Float32Array(16 * 2 + 4);
      mvpModel.set(mvp, 0);
      mvpModel.set(model, 16);
      // lightDir placeholder
      mvpModel[32] = 0.5;
      mvpModel[33] = 1.0;
      mvpModel[34] = 0.3;
      mvpModel[35] = 0.0;
      device.queue.writeBuffer(
        renderer.uniformBuffer,
        0,
        mvpModel.buffer,
        mvpModel.byteOffset,
        mvpModel.byteLength
      );

      renderPass.setBindGroup(0, renderer.bindGroup);

      if (entity.mesh) {
        renderPass.setVertexBuffer(0, entity.mesh.vertexBuffer);
        renderPass.setIndexBuffer(entity.mesh.indexBuffer, "uint32");
        renderPass.drawIndexed(entity.mesh.indexCount, 1, 0, 0, 0);
      }

      renderPass.end();
      device.queue.submit([commandEncoder.finish()]);

      requestAnimationFrame(frame);
    }
    requestAnimationFrame(frame);

    // expose for debugging
    engine = { device, renderer, cam, entity };
  } catch (err) {
    console.error(err);
    alert("WebGPU init failed: " + err.message);
  }
});

onBeforeUnmount(() => {
  // TODO: cleanup resources if needed
});
</script>

<style scoped>
.engine-wrap {
  position: relative;
  width: 100%;
  height: 100%;
  overflow: hidden;
}
.canvas {
  display: block;
  width: 100%;
  height: 100%;
  touch-action: none;
}
.overlay {
  position: absolute;
  left: 12px;
  top: 12px;
  padding: 8px 12px;
  background: rgba(0, 0, 0, 0.4);
  color: #fff;
  font-family: system-ui;
  border-radius: 8px;
}
</style>
