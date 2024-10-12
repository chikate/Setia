import { makeApiRequest } from '@/helpers'
import type { Taging } from '@/interfaces'

export const useCRUDStore = (storeName: string, defaultValues: any) =>
  defineStore(storeName, {
    state: (): {
      allLoadedItems?: (typeof defaultValues)[]
      selectedItems?: (typeof defaultValues)[]
      editItem?: typeof defaultValues
      filter?: (typeof defaultValues)[]
    } => {
      return {
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
        })).filter(
          (elem: typeof defaultValues) =>
            (elem.tags.indexOf('Deleted') ?? -1) <= -1 &&
            (elem.postData.tags.indexOf('Deleted') ?? -1) <= -1
          // (customFilter ? elem.author == useAuthStore().userData.username : true)
        )
      },
      async add(customAdds?: (typeof defaultValues)[]): Promise<(typeof defaultValues)[]> {
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
        if (customDelete) this.selectedItems = customDelete
        if (this.selectedItems && this.selectedItems[0]) {
          await makeApiRequest(
            `${storeName}/Update`,
            'put',
            this.selectedItems.map((elem: typeof defaultValues) =>
              elem.tags && elem.tags.find((elem: Taging) => elem.tag != 'Deleted')
                ? elem.tags.push('Deleted')
                : (elem.tags = ['Deleted'])
            )
          )
          this.get
        }
      },
      getDefaults() {
        return defaultValues
      },
      resetToDefaults() {
        return (this.editItem = defaultValues)
      }
    },
    persist: true
  })
