import type { QuestionAnswer } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useQuestionAnswersCRUDStore = useCRUDStore('QuestionAnswers', {
  user: '',
  question: '',
  answer: []
} as QuestionAnswer)
