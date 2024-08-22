import type { Taging } from '@/interfaces'

export const useTagsCRUDStore = useCRUDStore('Tags', {
  tag: '',
  comments: ''
} as Taging)
