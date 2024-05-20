import { makeApiRequest } from '@/helpers'
import { defineStore } from 'pinia'

export const useHelperStore = defineStore('Helper', {
  state: (): {
    allLoadedActions: object[]
    focusedActions: string
  } => {
    return {
      allLoadedActions: [],
      focusedActions: ''
    }
  },
  actions: {
    async uploadFiles(file: File | File[]) {
      const formData = new FormData()
      if (Array.isArray(file)) {
        file.forEach((file: File, index: number) => {
          formData.append(`files[${index}]`, file)
          formData.append(`files`, file)
        })
      }
      await makeApiRequest(`${this.$id}/Upload`, 'post', formData)
    },
    async getAllActions(): Promise<object[]> {
      return (this.allLoadedActions = await makeApiRequest(`${this.$id}/GetAllActions`, 'get'))
    }
  }
})
