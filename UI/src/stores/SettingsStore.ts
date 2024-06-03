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
        linkElement.href.includes('dark') ? (this.theme = 'light') : (this.theme = 'dark')
        linkElement.href = `/node_modules/primevue/resources/themes/aura-${this.theme}-noir/theme.css`
      }
      // document.querySelector('app')?.classList.toggle('dark-theme', this.useDarkMode)
    }
  }
})
