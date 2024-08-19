<template>
  <div class="flex-column gap-1">
    <Avatar
      class="shadow-1 cursor-pointer opacity-05"
      image="https://c4.wallpaperflare.com/wallpaper/397/667/639/landscape-fantasy-art-fantasy-city-artwork-wallpaper-preview.jpg"
      size="xlarge"
      shape="circle"
    />
    <InputText v-model="createdServerData.name" class="text-center" />
    <Button
      label="Create"
      @click="
        () => {
          if (createdServer.name.length >= 3) $router.push(`/servers/${createdServer.name}`)
        }
      "
    />
  </div>
</template>

<script setup lang="ts">
const createdServerData = defineModel('createdServer', {
  type: Object,
  required: false,
  default: { name: '', avatar: '', settings: {}, members: [] }
})
</script>
