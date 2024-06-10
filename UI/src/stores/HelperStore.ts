import { makeApiRequest } from '@/helpers'
import { Post, User } from '@/interfaces'
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
    async getUserProfile(username: string): Promise<User> {
      return (await makeApiRequest(`${this.$id}/GetUserProfile`, 'get', { username })).json()
    },
    async setUserAvatar(avatarUrl: string) {
      await makeApiRequest(`${this.$id}/UpdateCurentUserAvatar`, 'get', { avatarUrl })
    }
  }
})
