<template>
  <canvas ref="canvas" :width="width" :height="height" />
</template>

<script lang="ts" setup>
// Props
const props = defineProps({
  width: {
    type: Number,
    default: 800, // Width of the canvas
  },
  height: {
    type: Number,
    default: 600, // Height of the canvas
  },
  sites: {
    type: Number,
    default: 5, // Number of Voronoi sites
  },
});

// Refs
const canvas = ref<HTMLCanvasElement>();

// Computed property to generate the site positions
const generateSites = () => {
  const sites = [];
  for (let i = 0; i < props.sites; i++) {
    sites.push({
      x: Math.random() * props.width,
      y: Math.random() * props.height,
    });
  }
  return sites;
};

// Function to generate Voronoi texture
const generateVoronoi = () => {
  if (!canvas.value) return;
  const ctx = canvas.value.getContext("2d");
  if (!ctx) return;

  ctx.clearRect(0, 0, props.width, props.height);

  // Get the fixed points (sites)
  const points = generateSites();

  // Create an array to store the colors of the Voronoi cells
  const cellColors = points.map(
    (_, index) => `hsl(${(index * 360) / points.length}, 100%, 70%)`
  );

  // Draw the Voronoi diagram with irregular cells
  points.forEach((point, index) => {
    ctx.beginPath();

    // Create a series of random offsets for the organic effect
    const numEdges = 8; // Number of edges for the irregular shape
    const radius = 30; // Base radius from the point
    const angleStep = (Math.PI * 2) / numEdges;

    for (let j = 0; j < numEdges; j++) {
      const angle = j * angleStep;
      const randomOffset = Math.random() * 15; // Randomness for the organic shape
      const x = point.x + Math.cos(angle) * (radius + randomOffset);
      const y = point.y + Math.sin(angle) * (radius + randomOffset);

      if (j == 0) {
        ctx.moveTo(x, y); // Move to the starting point
      } else {
        ctx.lineTo(x, y); // Draw line to the next point
      }
    }
    ctx.closePath();

    // Fill the cell with color
    ctx.fillStyle = cellColors[index];
    ctx.fill();

    // Draw the border
    ctx.lineWidth = 2;
    ctx.strokeStyle = "black"; // Border color
    ctx.stroke();
  });

  // Draw cell centers (optional for visualization)
  ctx.fillStyle = "black";
  points.forEach((point) => {
    ctx.beginPath();
    ctx.arc(point.x, point.y, 3, 0, Math.PI * 2);
    ctx.fill();
  });
};

// Generate Voronoi on mount
onBeforeMount(generateVoronoi);
</script>
