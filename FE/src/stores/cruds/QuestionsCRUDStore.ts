import type { Question } from '@/interfaces'

export const useQuestionsCRUDStore = useCRUDStore('Questions', {
  title: 'Title',
  options: [''],
  selection: [''],
  author: undefined
} as Question)
