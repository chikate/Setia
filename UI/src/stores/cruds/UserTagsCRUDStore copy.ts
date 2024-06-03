import type { UserTag } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useUserTagsCRUDStore = useCRUDStore('UserTags', {} as UserTag)
