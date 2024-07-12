import type { Taging } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useTagsCRUDStore = useCRUDStore('Tags', {
  tag: '',
  comments: ''
} as Taging)
