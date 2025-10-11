<template>
  <div class="flex flex-wrap">
    <PostComponent :postData="{ message: 'asd', tags: [] }" />
  </div>
</template>

<script lang="ts" setup>
const loadedUsers = ref([]);

onBeforeMount(init);
async function init() {
  loadedUsers.value = []; // usersIds
}
</script>
