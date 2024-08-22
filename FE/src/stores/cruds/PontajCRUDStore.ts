import type { Pontaj } from '@/interfaces'

export const usePontajCRUDStore = useCRUDStore('Pontaj', {
  beginTime: new Date().toISOString(),
  endTime: '',
  description: ''
} as Pontaj)
