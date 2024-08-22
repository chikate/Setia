import type { Post } from '@/interfaces'

export const usePostsCRUDStore = useCRUDStore('Posts', {
  message: ''
} as Post)
