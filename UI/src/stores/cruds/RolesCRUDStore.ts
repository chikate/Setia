import type { Role } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useRoleCRUDStore = useCRUDStore('Roles', {
  name: '',
  active: true
} as Role)
