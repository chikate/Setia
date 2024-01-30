import { defineStore } from 'pinia'
import type { PontajStore, Pontaj } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'

const defaultEditables: Pontaj = {
  beginTime: new Date().toISOString(),
  endTime: new Date().toISOString(),
  description: '',
  active: true
}

export const usePontajStore = defineStore('Pontaj', {
  state: (): PontajStore => {
    return {
      allLoadedItems: [],
      selectedItem: defaultEditables
    }
  },
  actions: {
    async getAll(): Promise<Pontaj[]> {
      return await makeRequest(`${this.$id}/GetAll`, 'get').then(async (response: Response) => {
        return await checkRequest(response).then((data) => {
          return (this.allLoadedItems = data as Pontaj[])
        })
      })
    },
    async add() {
      return await makeRequest(`${this.$id}/Add`, 'post', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async update() {
      return await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async delete() {
      this.selectedItem ? (this.selectedItem.deleted = true) : null
      return await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
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
