export interface BaseAudit {
  executionDate?: string
  author?: string
  authorData?: User
}
export interface Definition extends BaseAudit {
  active: boolean
}
export interface User extends Definition {
  username: string
  password: string
  email: string
  name: string
}
export interface Pontaj extends Definition {
  id?: number
  user?: number
  userData?: User
  beginTime: string
  endTime: string
  description: string
}
export interface Taging extends Definition {
  tag?: string
  comments?: string
}
export interface Question extends Definition {
  id?: string
  title: string
  comment: string
  options: string[]
  selection: string[]
  expires: Date
}
export interface QuestionAnswer extends Definition {
  id?: string
  user: string
  userData: User
  question: string
  questionData: Question
  answer: string[]
}
export interface UserTag extends Definition {
  id?: string
  user: string
  tag: string
}
export interface INotification {
  id?: number
  icon: string
  title: string
  comment: string
}
