<template>
  <footer class="flex-row w-screen gap-4 py-4 px-8 border-top-1">
    <label class="flex flex-grow-1">
      © {{ new Date().getFullYear() }} Dragos, Inc. All Rights Reserved.
    </label>
    <i class="pi pi-discord" @click="true" />
    <i class="pi pi-youtube" @click="true" />
    <i class="pi pi-twitter" @click="true" />
  </footer>
</template>
