import { defineStore } from 'pinia'

export const useSettingsStore = defineStore('Settings', {
  state: (): {
    useDarkMode: boolean
    language: 'ro' | 'en'
  } => {
    return {
      useDarkMode: true,
      language: 'en'
    }
  },
  actions: {
    async toggleDarkMode() {
      this.useDarkMode = !this.useDarkMode
      // document.querySelector('app')?.classList.toggle('dark-theme', this.useDarkMode)
    }
  },
  persist: true
})
