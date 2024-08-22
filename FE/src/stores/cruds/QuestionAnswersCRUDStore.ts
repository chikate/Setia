import type { QuestionAnswer } from '@/interfaces'

export const useQuestionAnswersCRUDStore = useCRUDStore('QuestionAnswers', {
  author: '',
  questionData: { title: '' },
  answer: ['']
} as QuestionAnswer)
