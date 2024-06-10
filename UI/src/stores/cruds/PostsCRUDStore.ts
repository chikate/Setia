import type { Post } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const usePostsCRUDStore = useCRUDStore('Posts', {
  message: ''
} as Post)
