<template>
  <nav
    class="fixed z-5 w-screen flex flex-row gap-8 pointer-events-none px-8"
    :class="[
      !(scrollPosition < scrollThresHold && $route.name == 'home')
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
    <RouterLink class="pointer-events-auto" to="/universe"> {{ 'Universe' }} </RouterLink>
    <div class="flex-grow-1 pointer-events-none" />
    <div class="flex flex-row gap-3 pointer-events-auto">
      <i
        v-tooltip.left="{
          value: useSettingsStore().useDarkMode ? 'Switch to light mode' : 'Switch to dark mode',
          showDelay: 700,
          hideDelay: 100
        }"
        class="pi cursor-pointer"
        :class="useSettingsStore().useDarkMode ? 'pi-moon' : 'pi-sun'"
        @click="useSettingsStore().useDarkMode = !useSettingsStore().useDarkMode"
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
          {{ 'Download' }}
        </RouterLink>
      </div>

      <div @click="accountOverlay.toggle($event)" class="cursor-pointer">
        <i class="pi pi-user">
          <OverlayPanel ref="accountOverlay" class="p-3 m-1">
            <LoginComponent
              @onClose="accountOverlay.toggle($event)"
              @onLoginClick="
                useAuthenticationStore().tryLogin($event.inputUsername, $event.inputPassword)
              "
            />
          </OverlayPanel>
        </i>
        <a>
          {{
            useAuthenticationStore().user == null
              ? 'Account'
              : useAuthenticationStore().user?.email ?? 'User'
          }}
        </a>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
// Components
import LoginComponent from './authentication/LoginComponent.vue'
import OverlayPanel from 'primevue/overlaypanel'
import Menu from 'primevue/menu'
import { useToast } from 'primevue/usetoast'

// Vue
import { ref } from 'vue'

// Stores
import { useAuthenticationStore } from '@/stores/AuthenticationStore'
import { useSettingsStore } from '@/stores/SettingsStore'

// Variabels
const accountOverlay = ref()
const languageOverlay = ref()

const scrollPosition = ref<number>(0)
const scrollThresHold = ref<number>(200)
const languages = ref([
  {
    label: 'English',
    command: () => {
      languageOverlay.value.hide()
      useToast().add({
        severity: 'success',
        summary: 'Changed language to English',
        detail: '',
        life: 3000
      })
    }
  },
  {
    label: 'Română - Romanian',
    command: () => {
      languageOverlay.value.hide()
      useToast().add({
        severity: 'success',
        summary: 'Schimbat limba în Română',
        detail: '',
        life: 3000
      })
    }
  }
])

// Functions
window.addEventListener('scroll', function () {
  scrollPosition.value = window.scrollY
  if (languageOverlay.value.visible == true) {
    languageOverlay.value.hide()
  }
  if (accountOverlay.value.visible == true) {
    accountOverlay.value.hide()
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