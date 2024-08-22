import type { User } from '@/interfaces'

export const useUsersCRUDStore = useCRUDStore('Users', {
  username: '',
  email: '',
  name: '',
  tags: ['']
} as User)
