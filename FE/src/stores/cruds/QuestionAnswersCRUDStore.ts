import type { QuestionAnswer } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useQuestionAnswersCRUDStore = useCRUDStore('QuestionAnswers', {
  author: '',
  questionData: { title: '' },
  answer: ['']
} as QuestionAnswer)
