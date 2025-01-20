<template>
  <!-- style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))" -->
  <div class="flex-column justify-content-center">
    <a class="text-8xl font-bold"> Hello {{ authService?.user?.username ?? 'there !' }} </a>
    <a class="text-3xl font-bold">We are happy that you are here!</a>
  </div>
</template>

<style scoped>
.p-treeselect-label {
  white-space: wrap !important;
  flex-wrap: wrap !important;
  display: flex !important;
  row-gap: 0.25rem !important;
}
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
</style>
