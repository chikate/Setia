import type { User } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useUserCRUDStore = useCRUDStore('Users', {
  email: '',
  username: '',
  password: '',
  name: '',
  active: true
} as User)
