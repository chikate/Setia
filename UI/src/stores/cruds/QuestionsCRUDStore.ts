import type { Question } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useQuestionsCRUDStore = useCRUDStore('Questions', {
  title: 'Title',
  Options: ['']
} as Question)
