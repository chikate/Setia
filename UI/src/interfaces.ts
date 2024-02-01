import type { User } from "./stores/UserStore"

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