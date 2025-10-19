<template>
  <canvas ref="canvas" />
  <div class="overlay">
    W/A/S/D · E/Q · Mouse · R · Loaded: <strong>{{ modelName }}</strong>
  </div>
  <div v-if="selected" class="overlay right-0 gap-1 flex flex-column">
    <div
      v-for="key in propsToShow"
      :key="key"
      class="flex gap-1 align-items-center"
    >
      <label style="min-width: 100px; width: 100px; max-width: 100px">
        {{ key }}
      </label>
      <InputNumber v-model="selected[key].x" show-buttons />
      <InputNumber v-model="selected[key].y" show-buttons />
      <InputNumber v-model="selected[key].z" show-buttons />
    </div>
  </div>
</template>

<script setup>
import * as BABYLON from "babylonjs";

defineOptions({
  name: "Space Engine Editor",
  icon: "🌌",
});

const canvas = ref();
const modelName = ref("none");
const selected = ref();
const propsToShow = ["position", "rotation", "scaling"];

function parseOBJ(txt) {
  const pos = [];
  const nrm = [];
  const faces = [];
  const verts = [];
  const idxs = [];
  const map = new Map();
  txt.split("\n").forEach((l) => {
    l = l.trim();
    if (!l || l.startsWith("#")) return;
    const p = l.split(/\s+/);
    if (p[0] == "v") pos.push(p.slice(1).map(Number));
    else if (p[0] == "vn") nrm.push(p.slice(1).map(Number));
    else if (p[0] == "f") {
      const vs = p.slice(1).map((x) => {
        const [vi, , ni] = x
          .split("/")
          .map((n) => (n ? Number(n) - 1 : undefined));
        return { v: vi, n: ni };
      });
      for (let i = 1; i < vs.length - 1; i++)
        faces.push([vs[0], vs[i], vs[i + 1]]);
    }
  });
  let c = 0;
  faces.forEach((f) =>
    f.forEach((vr) => {
      const k = `${vr.v}/${vr.n}`;
      if (!map.has(k)) {
        verts.push(
          ...(pos[vr.v] || [0, 0, 0]),
          ...(vr.n !== undefined ? nrm[vr.n] : [0, 0, 1])
        );
        map.set(k, c++);
      }
      idxs.push(map.get(k));
    })
  );
  return { vertices: new Float32Array(verts), indices: new Uint32Array(idxs) };
}

onMounted(() => {
  const engine = new BABYLON.Engine(canvas.value, true);
  const scene = new BABYLON.Scene(engine);
  const cam = new BABYLON.UniversalCamera(
    "cam",
    new BABYLON.Vector3(2, 2, 6),
    scene
  );
  cam.attachControl(canvas.value, true);
  cam.keysUp.push(87);
  cam.keysDown.push(83);
  cam.keysLeft.push(65);
  cam.keysRight.push(68);
  cam.keysUpward.push(69);
  cam.keysDownward.push(81);

  // Components
  new BABYLON.DirectionalLight(
    "l",
    new BABYLON.Vector3(-1, -2, -1),
    scene
  ).intensity = 1;
  let currentMesh = BABYLON.MeshBuilder.CreateBox("box", { size: 2 }, scene);
  const gizmo = new BABYLON.GizmoManager(scene);
  gizmo.positionGizmoEnabled = true;

  canvas.value.addEventListener("dragover", (e) => e.preventDefault());
  canvas.value.addEventListener("drop", (e) => {
    e.preventDefault();
    const f = e.dataTransfer.files[0];
    if (!f) return;
    const reader = new FileReader();
    reader.onload = () => {
      try {
        const obj = parseOBJ(reader.result);
        if (currentMesh) currentMesh.dispose();
        currentMesh = new BABYLON.Mesh("obj", scene);
        const vd = new BABYLON.VertexData();
        vd.positions = Array.from(obj.vertices.filter((_, i) => i % 6 < 3));
        vd.normals = Array.from(obj.vertices.filter((_, i) => i % 6 >= 3));
        vd.indices = Array.from(obj.indices);
        vd.applyToMesh(currentMesh);
        modelName.value = f.name;
      } catch (err) {
        console.error(err);
      }
    };
    reader.readAsText(f);
  });

  // Selection
  scene.onPointerObservable.add((info) => {
    if (info.type === BABYLON.PointerEventTypes.POINTERPICK) {
      const m = info.pickInfo.pickedMesh;
      if (m && m != scene.getMeshByName("ground")) selected.value = m;
      console.log(selected.value);
    }
  });

  window.addEventListener("keydown", (e) => {
    if (e.key.toLowerCase() === "r") {
      cam.position = new BABYLON.Vector3(2, 2, 6);
      cam.setTarget(BABYLON.Vector3.Zero());
    }
  });

  engine.runRenderLoop(() => scene.render());
  window.addEventListener("resize", () => engine.resize());
});
</script>

<style scoped>
canvas {
  width: 100%;
  height: 100%;
  display: block;
  position: absolute;
  touch-action: none;
}
.overlay {
  position: absolute;
  padding: 0.25rem;
  margin: 0.25rem;
  background: rgba(0, 0, 0, 0.3);
  color: #fff;
  font-family: system-ui;
  overflow: auto;
  border-radius: 5px;
}
</style>
