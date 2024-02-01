import { defineStore } from 'pinia'

export const useSettingsStore = defineStore('Settings', {
  state: (): { useDarkMode: boolean } => {
    return {
      useDarkMode: true
    }
  },
  actions: {
    async toggleDarkMode() {
      this.useDarkMode = !this.useDarkMode
      // document.querySelector('app')?.classList.toggle('dark-theme', this.useDarkMode)
    }
  }
})
