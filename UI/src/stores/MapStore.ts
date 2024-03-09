import { defineStore } from 'pinia'

export const useMapStore = defineStore('Map', {
  state: (): {
    zoomPercentage: number
  } => {
    return {
      zoomPercentage: 0.5
    }
  },
  actions: {
    async toggleDarkMode() {}
  }
})
