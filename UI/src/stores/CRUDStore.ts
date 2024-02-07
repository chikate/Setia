import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'

export const useCRUDStore = (storeName: string) =>
  defineStore(storeName, {
    state: (): {
      allLoadedItems: []
      selectedItem: any
    } => {
      return {
        allLoadedItems: [],
        selectedItem: {}
      }
    },
    actions: {
      async getAll(): Promise<[]> {
        return (this.allLoadedItems = (await makeApiRequest(`${storeName}/GetAll`, 'get')) ?? [])
      },
      async add() {
        await makeApiRequest(`${storeName}/Add`, 'post', this.selectedItem).then(() => {
          this.getAll()
        })
      },
      async update() {
        await makeApiRequest(`${storeName}/Update`, 'put', this.selectedItem).then(() => {
          this.getAll()
        })
      },
      async delete() {
        if (this.selectedItem && 'delete' in this.selectedItem) {
          this.selectedItem.delete = false
          await makeApiRequest(`${storeName}/Update`, 'put', this.selectedItem).then(() => {
            this.getAll()
          })
        }
      },
      getDefaults() {
        return {}
      },
      async setSelectionToDefaults() {
        this.selectedItem = {}
      }
    }
  })
