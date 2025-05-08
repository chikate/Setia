// Interfaces

export interface IAuthenticationDTO {
  username?: string
  password?: string
}
export interface IBaseAudit {
  executionDate?: string
  author?: string
  authorData?: User
}
export interface IDefinition extends IBaseAudit {
  tags: string[]
}
export interface User extends IDefinition {
  id?: string
  username: string
  password: string
  email: string
  name: string
  avatar: string
}
export interface IPost extends IDefinition {
  id?: string
  message?: string
  questionId?: string
  questionData?: Question
  entityId?: string
  entity?: string
}
export interface Pontaj extends IDefinition {
  id?: number
  user?: number
  userData?: User
  beginTime: string
  endTime: string
  description: string
}
export interface Taging extends IDefinition {
  tag?: string
  comments?: string
}
export interface Question extends IDefinition {
  id?: string
  title: string
  comment: string
  options: string[]
  selection: string[]
  expires: Date
}
export interface QuestionAnswer extends IDefinition {
  id?: string
  user: string
  userData: User
  questionId: string
  questionData: Question
  answer: string[]
}
export interface UserTag extends IDefinition {
  id?: string
  user: string
  tag: string
}
export interface UserCollection extends IDefinition {
  id?: string
  postId: string
  postData: IPost
}
export interface INotification extends IDefinition {
  id?: string
  icon?: string
  title?: string
  comment?: string
}
