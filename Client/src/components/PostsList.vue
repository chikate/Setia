<template>
  <div class="flex flex-wrap">
    <PostComponent
      class="border-1 border-gray-700 p-2"
      :postData="{ message: '', tags: [] }"
    />
  </div>
</template>

<script setup lang="ts">
const loadedUsers = ref([]);

onBeforeMount(init);
async function init() {
  loadedUsers.value = []; // usersIds
}
</script>
