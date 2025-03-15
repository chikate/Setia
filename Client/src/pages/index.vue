<template>
  <div
    class="flex-column align-items-center text-center p-3 overflow-auto h-full"
  >
    <a class="text-8xl font-bold">
      Hello {{ authService?.user?.username ?? "there !" }}
    </a>
    <a class="text-3xl font-bold">We are happy that you are here!</a>

    <div class="flex-wrap gap-8 justify-content-around p-5 w-full">
      <LoginComponent class="custom-shadow-2 p-3" style="min-width: 300px" />
      <RegisterComponent class="custom-shadow-2 p-3" style="min-width: 300px" />
      <RecoveryComponent class="custom-shadow-2 p-3" style="min-width: 300px" />
    </div>

    <!-- <BaseCardComponent label="Voronoi Texture Generator">
    <VoronoiTextureComponent />
  </BaseCardComponent> -->

    <!-- <div>
    <label>We are glat thet you decided to join us</label>
    <label>Here is the thingy! :)</label>
    <br />
    <br />
    <label>Have a great journey!</label>
  </div> -->
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
