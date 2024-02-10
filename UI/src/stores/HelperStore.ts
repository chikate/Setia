import { makeApiRequest } from '@/helpers'
import { defineStore } from 'pinia'

export const useHelperStore = defineStore('Helper', {
  state: (): {} => {
    return {}
  },
  actions: {
    async uploadFiles(files: any[], details: string) {
      const formData = new FormData()
      files.forEach((file: any, index: number) => {
        formData.append(`Files[${index}]`, file)
      })
      formData.append('details', details)
      await makeApiRequest(`${this.$id}/Upload`, 'post', formData)
    }
  }
})
