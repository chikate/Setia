import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'
import { Taging } from '@/interfaces'

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
        return (this.allLoadedItems = await makeApiRequest(
          `${storeName}/Get`,
          'get',
          customFilter ?? this.filter
        ).then((response: Response) => {
          return response.json()
        }))
      },
      async add(customAdds?: (typeof defaultValues)[]): Promise<(typeof defaultValues)[] | undefined> {
        return await makeApiRequest(`${storeName}/Add`, 'post', customAdds ?? [this.editItem]).then(
          (response: Response) => {
            this.get()
            return response.status === 200 ? response.json() : undefined
          }
        )
      },
      async update(customUpdates?: (typeof defaultValues)[]): Promise<(typeof defaultValues)[]> {
        return await makeApiRequest(
          `${storeName}/Update`,
          'put',
          customUpdates ?? [this.editItem]
        ).then((response: Response) => {
          this.get()
          return response.json()
        })
      },
      async delete(customDelete?: (typeof defaultValues)[]) {
        const toDelete = customDelete ?? [this.editItem]
        if (toDelete[0]) {
          toDelete?.forEach((elem: typeof defaultValues) => {
            if (elem.tags) {
              if (elem.tags?.find((elem: Taging) => !(elem.tag == 'Deleted'))) {
                elem.tags.push('Deleted')
              }
            } else {
              elem.tags = ['Deleted']
            }
          })
          await makeApiRequest(`${storeName}/Update`, 'put', toDelete).then(() => this.get())
        } else {
          const idsToDelete = toDelete?.map(
            (elem: typeof defaultValues) => elem[Object.keys(elem)[0]]
          )
          await makeApiRequest(`${storeName}/Delete`, 'delete', idsToDelete).then(() => this.get())
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
