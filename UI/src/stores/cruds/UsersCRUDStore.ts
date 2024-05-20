import type { User } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useUsersCRUDStore = useCRUDStore('Users', {
  username: '',
  password: '',
  email: '',
  name: '',
  active: true
} as User)
