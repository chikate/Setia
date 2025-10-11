<template><div id="map" /></template>

<script setup>
import L from "leaflet";
import osmtogeojson from "osmtogeojson";

defineOptions({
  name: "Romania",
  icon: "💒",
});

let map;

async function markEUMember() {
  const fetchEUMembersFromWiki = await (
    await fetch(
      "https://query.wikidata.org/sparql?format=json&query=" +
        encodeURIComponent(
          "SELECT ?iso WHERE { ?country wdt:P31 wd:Q6256;wdt:P463 wd:Q458;wdt:P297 ?iso.}"
        )
    )
  ).json();

  const osmData = await (
    await fetch(
      "https://overpass-api.de/api/interpreter?data=" +
        encodeURIComponent(
          `[out:json][timeout:120];(${fetchEUMembersFromWiki.results.bindings
            .map((b) => b.iso.value)
            .map((i) => `relation["ISO3166-1"="${i}"];`)
            .join("")});out body;>;out skel qt;`
        )
    )
  ).json();

  const euLayer = L.geoJSON(osmtogeojson(osmData), {
    style: { color: "#0057B7", weight: 1.5, fillOpacity: 0.25 },
  }).addTo(map);
}

onMounted(async () => {
  map = L.map("map", {
    zoomControl: false,
    attributionControl: false,
  }).setView([45, 25], 5);
  L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(map);

  const ZoomEUControl = L.Control.extend({
    onAdd() {
      const btn = L.DomUtil.create("button", "zoom-eu-btn");
      btn.innerHTML = "EU";
      btn.title = "Zoom to European Union";
      btn.onclick = (e) => {
        e.stopPropagation();
        // map.fitBounds(euLayer.getBounds());
        markEUMember();
      };
      return btn;
    },
  });
  map.addControl(new ZoomEUControl({ position: "topright" }));
});
</script>

<style scoped>
#map {
  height: 100%;
  width: 100%;
}
:deep().zoom-eu-btn {
  background: #0057b7;
  color: white;
  border: none;
  padding: 0.5rem 0.75rem;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 500;
}
:deep().zoom-eu-btn:hover {
  background: #003f8a;
}
</style>
