<template>
  <nav
    class="fixed z-5 w-screen flex flex-row gap-8 pointer-events-none px-8"
    :class="[
      !(scrollPosition < scrollThresHold && $route.name == '/')
        ? 'py-3 bg-gray-alpha-20 blur-10'
        : 'py-4 text-xl'
    ]"
    style="
      transition:
        font-size 0.4s ease,
        padding 0.4s ease;
    "
  >
    <RouterLink class="pointer-events-auto" to="/"> {{ 'DRAGOS' }} </RouterLink>
    <div class="flex flex-row gap-4 pointer-events-auto">
      <RouterLink to="/about"> {{ 'Our Mission' }} </RouterLink>
      <RouterLink to="/career"> {{ 'Join us' }} </RouterLink>
      <RouterLink to="/news"> {{ 'News' }} </RouterLink>
    </div>
    <div class="flex flex-row gap-3 pointer-events-auto flex-grow-1 justify-content-end">
      <i
        v-tooltip.left="{
          value: `Switch to ${useSettingsStore().useDarkMode ? 'light' : 'dark'} mode`,
          showDelay: 700,
          hideDelay: 100
        }"
        class="pi cursor-pointer"
        :class="useSettingsStore().useDarkMode ? 'pi-moon' : 'pi-sun'"
        @click="useSettingsStore().toggleDarkMode()"
      />

      <i
        v-tooltip.left="{
          value: 'Change language',
          showDelay: 700,
          hideDelay: 100
        }"
        class="pi pi-language cursor-pointer"
        @click="languageOverlay.toggle($event)"
      >
        <OverlayPanel ref="languageOverlay">
          <Menu class="border-0 m-0 p-0" :model="languages">
            <template #item="{ item }">
              <div class="flex flex-row align-items-center justify-content-center gap-2">
                <img
                  alt="flag"
                  src="https://primefaces.org/cdn/primevue/images/flag/flag_placeholder.png"
                  :class="`flag flag-romania`"
                  style="width: 24px"
                />
                <label>
                  {{ item.label }}
                </label>
              </div>
            </template>
          </Menu>
        </OverlayPanel>
      </i>

      <div class="cursor-pointer">
        <i class="pi pi-download"> </i>
        <RouterLink to="/download">
          {{ 'Downloads' }}
        </RouterLink>
      </div>

      <i
        v-badge="temp_notifications.length"
        v-if="async () => await useAuthStore().getToken()"
        v-tooltip.left="{
          value: `Notifications`,
          showDelay: 700,
          hideDelay: 100
        }"
        class="pi pi-bell cursor-pointer"
        @click="notificationsOverlay.toggle($event)"
      >
        <OverlayPanel ref="notificationsOverlay" class="p-0 m-0">
          <div class="flex flex-column">
            <Button
              v-for="notification in temp_notifications"
              :key="notification"
              :label="notification"
              class="p-1"
              text
            />
          </div>
        </OverlayPanel>
      </i>

      <div @click="accountOverlay.toggle($event)" class="cursor-pointer">
        <i class="pi pi-user">
          <OverlayPanel ref="accountOverlay" class="p-0 m-0">
            <LoginComponent
              v-if="!useAuthStore().getToken()"
              @close="accountOverlay.hide($event)"
            />
            <div v-else class="flex flex-column gap-3">
              <Button label="Profile" @click="$router.push('/profile')" />
              <Button label="Administration" @click="$router.push('/adm')" />
              <Button label="Quizz" @click="$router.push('/quizz-creator')" />
              <Button label="Logout" @click="useAuthStore().logOut()" />
            </div>
          </OverlayPanel>
        </i>
        <a>
          {{ useAuthStore().getToken() ? 'Dragos' : 'Account' }}
        </a>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/AuthStore'
import { useSettingsStore } from '@/stores/SettingsStore'
import { ref } from 'vue'

const accountOverlay = ref()
const notificationsOverlay = ref()
const languageOverlay = ref()

const temp_notifications = ref(['Notification1', 'Notification2', 'Notification3'])

const scrollPosition = ref<number>(0)
const scrollThresHold = ref<number>(200)
const languages = ref([
  {
    label: 'English',
    command: () => {
      languageOverlay.value.hide()
    }
  },
  {
    label: 'Română - Romanian',
    command: () => {
      languageOverlay.value.hide()
    }
  }
])

// Functions
window.addEventListener('scroll', function () {
  if (window.location.pathname == '/') {
    scrollPosition.value = window.scrollY
  }
})
</script>

<style scoped>
a {
  color: white;
  text-shadow: rgba(12, 12, 12, 0.2) 0 0 25px;
  font-weight: 500;
}

a:hover {
  text-shadow: rgba(12, 12, 12, 0.8) 0 0 25px;
}

i {
  padding-inline: 0.33rem;
}
</style>
