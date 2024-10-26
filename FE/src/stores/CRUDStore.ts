import { apiRequest } from '@/helpers'

export const CRUDStore = (storeName: string, defaultValues: any) => {
  const getItems = async (getFilter?: any) => await apiRequest(`${storeName}/Get`, getFilter)

  const addItems = (items?: (typeof defaultValues)[]) =>
    apiRequest(`${storeName}/Add`, items, 'post').then((response) => {
      if (response) getItems()
      return response
    })

  const updateItems = (items?: (typeof defaultValues)[]) =>
    apiRequest(`${storeName}/Update`, items, 'put').then((response) => {
      if (response) getItems()
      return response
    })

  const deleteItems = (ids?: string[]) =>
    apiRequest(`${storeName}/Delete`, ids, 'delete').then((response) => {
      if (response) getItems()
      return response
    })

  return {
    getItems,
    addItems,
    updateItems,
    deleteItems,
    storeName,
    defaultValues
  }
}
