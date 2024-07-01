export interface BaseAudit {
  executionDate?: string
  author?: string
  authorData?: User
}
export interface Definition extends BaseAudit {
  tags: string[]
}
export interface User extends Definition {
  id?: string
  username: string
  password: string
  email: string
  name: string
  avatar: string
}
export interface Post extends Definition {
  id?: string
  message?: string
  questionId?: string
  questionData?: Question
  entityId?: string
  entity?: string
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
  questionId: string
  questionData: Question
  answer: string[]
}
export interface UserTag extends Definition {
  id?: string
  user: string
  tag: string
}
export interface UserCollection extends Definition {
  id?: string
  postId: string
  postData: Post
}
export interface INotification extends Definition {
  id?: string
  icon?: string
  title?: string
  comment?: string
}
