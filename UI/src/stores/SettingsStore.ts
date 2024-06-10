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
        localStorage.setItem(
          'theme',
          (linkElement as HTMLAnchorElement).href.includes('dark') ? 'light' : 'dark'
        )
        ;(linkElement as HTMLAnchorElement).href =
          `/node_modules/primevue/resources/themes/aura-${localStorage.getItem('theme')}-noir/theme.css`
      }
    }
  },
  getters: {
    theme: () => localStorage.getItem('theme') ?? 'dark'
  },
  persist: true
})
