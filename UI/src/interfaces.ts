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
  email: string
  username: string
  password: string
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
