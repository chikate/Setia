<template>
  <div v-if="() => useAuthStore().checkUserRights" class="flex flex-row w-full flex-grow-1 p-8">
    dsa
    <div class="flex flex-column w-full h-full w-3"></div>
    <div class="flex flex-column w-full h-full">
      <div class="flex-grow-1">asd</div>
      <InputText />
    </div>
  </div>
</template>
