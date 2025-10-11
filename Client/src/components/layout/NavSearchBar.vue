<script setup lang="ts">
const accountOverlay = ref()
</script>

<template>
  <nav
    class="fixed bg-primary-reverse shadow-3 border-1 border-priamry align-items-center border-round z-5 flex flex-row"
  >
    <InputText placeholder="Search" class="border-0 shadow-0" />
    <RouterLink
      v-for="(menu, i) in [
        { icon: 'home', label: 'Home', path: '/' },
        { icon: 'cog', label: 'Administration', path: '/adm', right: 'Admin' },
        { icon: 'globe', label: 'Universe', path: '/universe' },
        { icon: 'globe', label: 'vote-creator', path: '/vote-creator' },
        { icon: 'globe', label: 'messanger', path: '/messanger' },
        { icon: 'globe', label: 'collection', path: '/collection' }
      ]"
      :key="i"
      :to="menu.path"
      :class="{ 'text-blue-500': $route.path == menu.path }"
      v-tooltip="menu.label"
    >
      <i
        v-if="menu.right ? authService().checkUserRight(menu.right) : true"
        :class="`pi pi-${menu.icon} cursor-pointer`"
      />
    </RouterLink>
    <a
      @click="
        authService().userData
          ? $router.push(`/profile/${authService().userData?.username}`)
          : accountOverlay.toggle($event)
      "
      class="cursor-pointer flex flex-row align-items-center"
    >
      <Avatar
        class="mx-1"
        v-if="true"
        v-badge="useNotificationsCRUDStore().allLoadedItems?.length ?? ''"
        v-tooltip="'Profile'"
      />
      <div class="pr-2 font-semibold" v-else>
        <i class="pi pi-user" />
        <OverlayPanel ref="accountOverlay" class="p-0 m-0">
          <LoginComponent v-if="!authService().token" @close="accountOverlay.hide($event)" />
        </OverlayPanel>
      </div>
    </a>
  </nav>
</template>

<style scoped>
a {
  color: var(--primary);
  text-shadow: rgba(12, 12, 12, 0.2) 0 0 25px;
  font-weight: 500;
}

a:hover {
  text-shadow: rgba(12, 12, 12, 0.8) 0 0 25px;
}

i {
  padding-inline: 0.5rem;
}
</style>
