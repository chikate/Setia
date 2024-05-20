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
        selectedItem: defaultValues
      }
    },
    actions: {
      async getAll(): Promise<(typeof defaultValues)[]> {
        return (this.allLoadedItems = JSON.parse(
          (await makeApiRequest(`${this.$id}/Get`, 'get')) ?? []
        ))
      },
      async add(custom?: typeof defaultValues) {
        await makeApiRequest(`${this.$id}/Add`, 'post', custom ?? this.selectedItem).then(
          (result) => {
            console.log(result)
            if (result == 'Added') this.getAll()
          }
        )
      },
      async update(custom?: typeof defaultValues) {
        await makeApiRequest(`${this.$id}/Update`, 'put', custom ?? this.selectedItem).then(
          (result) => {
            console.log(result)
            if (result == 'Updated') this.getAll()
          }
        )
      },
      async delete() {
        if (this.selectedItem && 'delete' in this.selectedItem) {
          this.selectedItem.delete = false
          await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then((result) => {
            console.log(result)
            if (result == 'Deleted') this.getAll()
          })
        }
      },
      getDefaults() {
        return defaultValues
      },
      resetToDefaults() {
        return (this.selectedItem = this.getDefaults())
      }
    }
  })
