<template>
  <div
    class="flex flex-column w-full h-full justify-content-around align-items-center"
  >
    <div />
    <BaseCardComponent
      label="Register"
      icon="pi pi-user-plus"
      class="custom-shadow-1 p-2"
    >
      <RegisterComponent />
    </BaseCardComponent>

    <BaseCardComponent
      label="Login"
      icon="pi pi-sign-in"
      class="custom-shadow-1 p-2"
    >
      <LoginComponent />
    </BaseCardComponent>

    <BaseCardComponent
      label="Recover"
      icon="pi pi-envelope"
      class="custom-shadow-1 p-2"
    >
      <RecoveryComponent />
    </BaseCardComponent>
    <div />

    <!-- <BaseCardComponent label="Voronoi Texture Generator">
        <VoronoiTextureComponent />
      </BaseCardComponent> -->
  </div>
</template>

<script lang="ts" setup>
defineOptions({
  name: "Authentication",
  icon: "🗝️",
});
</script>
