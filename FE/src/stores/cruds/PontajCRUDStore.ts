import type { Pontaj } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const usePontajCRUDStore = useCRUDStore('Pontaj', {
  beginTime: new Date().toISOString(),
  endTime: '',
  description: ''
} as Pontaj)
