export interface BaseAudit {
  creationDate: string
  creator_Id: number | null
  creator: User | null
}
export interface Definition extends BaseAudit {
  active: boolean
  deleted?: boolean
}
export interface User extends Definition {
  id?: number
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
  id?: string
  name: string
  parentTagId: string | undefined
  parentTag: Tag | undefined
}
export interface Right extends Definition {
  id?: number
  name: string
  action_id: number | null
  filter: string | null
}
