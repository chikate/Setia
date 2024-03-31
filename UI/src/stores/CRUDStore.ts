import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'

export const useCRUDStore = (storeName: string, defaultValues: any) =>
  defineStore(storeName, {
    state: (): {
      allLoadedItems: (typeof defaultValues)[]
      selectedItem: typeof defaultValues
    } => {
      return {
        allLoadedItems: [],
        selectedItem: {}
      }
    },
    actions: {
      async getAll(): Promise<(typeof this.allLoadedItems)[]> {
        return (this.allLoadedItems = (await makeApiRequest(`${this.$id}/GetAll`, 'post')) ?? [])
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
        if (this.selectedItem && 'delete' in this.selectedItem) {
          this.selectedItem.delete = false
          await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
            this.getAll()
          })
        }
      },
      getDefaults() {
        return defaultValues
      },
      async setSelectionToDefaults() {
        this.selectedItem = this.getDefaults()
      }
    },
    persist: true
  })
