import type { Tag } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useTagsCRUDStore = useCRUDStore('Tags', {
  tag: '',
  active: true
} as Tag)
