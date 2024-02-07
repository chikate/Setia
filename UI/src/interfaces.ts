export interface BaseAudit {
  creationDate?: string
  id_CreatedBy?: number | null
  createdBy?: User | null
  lastUpdateDate?: string | null
  id_LastUpdateBy?: number | null
  lastUpdateBy?: User | null
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
  statusCode: number | null
  rights: string[] | null
}
export interface Pontaj extends Definition {
  id?: number
  id_User?: number
  user?: User | null
  beginTime: string
  endTime?: string | null
  description: string | null
}
export interface Role extends Definition {
  id?: number
  name?: string
}
