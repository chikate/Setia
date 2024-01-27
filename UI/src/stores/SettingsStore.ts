import { defineStore } from 'pinia'
import type { SettingsStore } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'

export const useSettingsStore = defineStore('SettingsStore', {
  state: (): SettingsStore => {
    return {
      useDarkMode: true
    }
  }
})
