import { defineStore } from 'pinia'
import type { SettingsStore } from '@/interfaces'

export const useSettingsStore = defineStore('SettingsStore', {
  state: (): SettingsStore => {
    return {
      useDarkMode: true
    }
  }
})
