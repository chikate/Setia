<template>
  <div
    class="item m-2 flex flex-column border-round overflow-hidden bg-gray-500 custom-shadow-1 p-2 cursor-pointer border-1 hover:border-green-500">
    <img :src="`https://freepngimg.com/save/162038-laptop-notebook-free-clipart-hq/1153x886`" width="256"
      style="object-fit: cover" />
    <label class="text-justify">Laptop{{}}</label>
    <label class="text-justify text-red-500 text-xl font-bold">4999.99</label>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  username: { type: String },
});

onBeforeMount(async () => {
  // profileUserData.value = await helperService.getUserProfile(
  //   String(props.username)
  // );
});
</script>

<style scoped>
.item {
  transition: .2s
}

.item:hover {
  transform: scale(1.02);
  box-shadow: 0 4px 10px rgba(0, 0, 0, .15)
}
</style>
