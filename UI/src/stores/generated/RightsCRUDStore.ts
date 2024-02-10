import type { Right } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useRightsCRUDStore = useCRUDStore('Rights', {
  name: '',
  action_id: null,
  filter: null,
  active: true
} as Right)
