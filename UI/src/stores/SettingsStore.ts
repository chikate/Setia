import { defineStore } from 'pinia'

interface SettingsStore {
  useDarkMode: boolean
}

export const useSettingsStore = defineStore('SettingsStore', {
  state: (): SettingsStore => {
    return {
      useDarkMode: true
    }
  }
})
