import type {
  INotification,
  Pontaj,
  Post,
  Question,
  QuestionAnswer,
  Taging,
  User,
  UserCollection
} from '@/interfaces'

export const notificationsCRUDStore = CRUDStore('Notifications', {} as INotification)
export const userCollectionCRUDStore = CRUDStore('UserCollection', {} as UserCollection)

export const pontajCRUDStore = CRUDStore('Pontaj', {
  beginTime: new Date().toISOString(),
  endTime: '',
  description: ''
} as Pontaj)

export const postsCRUDStore = CRUDStore('Posts', {
  message: ''
} as Post)

export const questionAnswersCRUDStore = CRUDStore('QuestionAnswers', {
  author: '',
  questionData: { title: '' },
  answer: []
} as unknown as QuestionAnswer)

export const questionsCRUDStore = CRUDStore('Questions', {
  title: 'Title',
  options: [],
  selection: [],
  author: undefined
} as unknown as Question)

export const tagsCRUDStore = CRUDStore('Tags', {
  tag: '',
  comments: ''
} as Taging)

export const usersCRUDStore = CRUDStore('Users', {
  username: '',
  email: '',
  name: '',
  tags: []
} as unknown as User)
