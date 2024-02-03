import { defineStore } from 'pinia'
import { makeRequest } from '@/helpers'
import type { Definition } from '@/interfaces'
import type { User } from './UserStore'

export interface Pontaj extends Definition {
  id?: number
  id_User?: number
  user?: User | null
  beginTime: string
  endTime?: string | null
  description: string | null
}

const defaultEditables: Pontaj = {
  beginTime: new Date().toISOString(),
  endTime: new Date().toISOString(),
  description: null,
  active: true
}

export const usePontajStore = defineStore('Pontaj', {
  state: (): {
    allLoadedItems: Pontaj[]
    selectedItem: Pontaj
  } => {
    return {
      allLoadedItems: [],
      selectedItem: defaultEditables
    }
  },
  actions: {
    async getAll(): Promise<Pontaj[]> {
      return (this.allLoadedItems = ((await makeRequest(`${this.$id}/GetAll`, 'get')) ??
        []) as Pontaj[])
    },
    async add() {
      await makeRequest(`${this.$id}/Add`, 'post', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async update() {
      await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async delete() {
      this.selectedItem ? (this.selectedItem.deleted = true) : null
      await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
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
