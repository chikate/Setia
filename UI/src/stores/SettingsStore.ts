import { defineStore } from 'pinia'

export const useSettingsStore = defineStore('Settings', {
  state: (): {
    theme: string
    language: 'ro' | 'en'
  } => {
    return {
      theme: 'dark',
      language: 'en'
    }
  },
  actions: {
    async toggleDarkMode() {
      const linkElement = document.getElementById('theme-link')
      if (linkElement) {
        ;(linkElement as HTMLAnchorElement).href.includes('dark')
          ? (this.theme = 'light')
          : (this.theme = 'dark')
        ;(linkElement as HTMLAnchorElement).href =
          `/node_modules/primevue/resources/themes/aura-${this.theme}-noir/theme.css`
      }
    }
  },
  persist: true
})
