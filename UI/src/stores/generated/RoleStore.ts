import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'
import type { Role } from '@/interfaces'

const defaultEditables: Role = {
  name: '',
  active: true
}

export const useRoleStore = defineStore('Roles', {
  state: (): {
    allLoadedItems: Role[]
    selectedItem: Role
  } => {
    return {
      allLoadedItems: [],
      selectedItem: defaultEditables
    }
  },
  actions: {
    async getAll(): Promise<Role[]> {
      return (this.allLoadedItems = ((await makeApiRequest(`${this.$id}/GetAll`, 'get')) ??
        []) as Role[])
    },
    async add() {
      await makeApiRequest(`${this.$id}/Add`, 'post', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async update() {
      await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async delete() {
      this.selectedItem ? (this.selectedItem.deleted = true) : null
      await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    getDefaults() {
      return defaultEditables
    },
    async setSelectionToDefaults() {
      this.selectedItem = this.getDefaults()
    }
  }
})
