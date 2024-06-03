import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'

export const useCRUDStore = (storeName: string, defaultValues: any) =>
  defineStore(storeName, {
    state: (): {
      allLoadedItems?: (typeof defaultValues)[]
      selectedItems?: (typeof defaultValues)[]
      editItem?: typeof defaultValues
      filter?: (typeof defaultValues)[]
    } => {
      return {
        selectedItems: [defaultValues],
        editItem: defaultValues
      }
    },
    actions: {
      async get(customFilter?: (typeof defaultValues)[]): Promise<(typeof defaultValues)[]> {
        return (this.allLoadedItems = await (
          await makeApiRequest(`${storeName}/Get`, 'get', customFilter ?? this.filter)
        ).json())
      },
      async add(customAdds?: (typeof defaultValues)[]) {
        await makeApiRequest(`${storeName}/Add`, 'post', customAdds ?? [this.editItem]).then(
          (response: Response) => {
            console.log(response)
            console.log(response.status)
            if (response.status == 200) this.get()
          }
        )
      },
      async update(customUpdates?: (typeof defaultValues)[]) {
        await makeApiRequest(`${storeName}/Update`, 'put', customUpdates ?? [this.editItem]).then(
          (response: Response) => {
            console.log(response)
            console.log(response.status)
            if (response.status == 200) this.get()
          }
        )
      },
      async delete(customDelete?: (typeof defaultValues)[]) {
        const toDelete = customDelete ?? [this.editItem]

        if (toDelete[0]) {
          toDelete?.forEach((element: typeof defaultValues) => {
            element.deleted = true
          })
          await makeApiRequest(`${storeName}/Update`, 'put', toDelete).then(
            (response: Response) => {
              console.log(response)
              console.log(response.status)
              if (response.status == 200) this.get()
            }
          )
        } else {
          const idsToDelete = toDelete?.map(
            (elem: typeof defaultValues) => elem[Object.keys(elem)[0]]
          )
          await makeApiRequest(`${storeName}/Delete`, 'delete', idsToDelete).then(
            (response: Response) => {
              console.log(response)
              console.log(response.status)
              if (response.status == 200) this.get()
            }
          )
        }
      },
      getDefaults() {
        return defaultValues
      },
      resetToDefaults() {
        return (this.editItem = defaultValues)
      }
    }
  })
