import type { Tag } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useTagsCRUDStore = useCRUDStore('Tags', {
  name: '',
  active: true
} as Tag)
