export interface BaseAudit {
  executionDate: string
  authorId: string | null
  author: User | null
}
export interface Definition extends BaseAudit {
  active: boolean
  deleted?: boolean
}
export interface User extends Definition {
  username: string
  password: string
  email: string
  name: string
}
export interface Pontaj extends Definition {
  id?: number
  id_User?: number
  user?: User
  beginTime: string
  endTime: string | null
  description: string
}
export interface Tag extends Definition {
  tag: string
  comments: string
}
export interface Question extends Definition {
  id?: string
  title: string
  comment: string
  Options: string[]
  availableUntil: Date
}
export interface QuestionAnswer {
  id?: string
  user: string
  question: string
  answer: string[]
}
