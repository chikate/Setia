<template>
  <div class="flex-column gap-1">
    <InputText v-model="search" class="card hover:bg-gray-50" placeholder="Search" />
    <div class="flex-wrap">
      <div
        v-for="(friend, i) in friends?.filter((elem) => elem.name.includes(search))"
        :key="i"
        class="card cursor-pointer hover:bg-gray-50 bg-primary-reverse border-round p-2 gap-2 flex-row align-items-center justify-content-center"
      >
        <Avatar size="large" shape="circle" :image="friend.avatar" />
        <Badge class="bg-teal-500" />
        {{ friend.name }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const friends = defineModel('friends', {
  type: Array<{ name: string; avatar: string }>,
  required: false,
  default: [{ name: 'Dragos', avatar: undefined }]
})
const search = defineModel('search', {
  type: String,
  required: false,
  default: ''
})
</script>
