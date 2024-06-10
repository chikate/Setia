import type { UserCollection } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useUserCollectionCRUDStore = useCRUDStore('UserCollection', {} as UserCollection)
