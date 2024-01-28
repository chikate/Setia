export interface Audit {
  creationDate?: string
  id_CreatedBy?: number
  createdBy?: number
  lastUpstringstring?: string
  id_LastUpstringBy?: number
  lastUpstringBy?: number
}

export interface Definition extends Audit {
  active?: boolean
  deleted?: boolean
}

// Pontaj
export interface PontajStore {
  allLoadedItems: Pontaj[]
  selectedItem: Pontaj
}
export interface Pontaj extends Definition {
  id?: number
  id_User?: number
  beginTime?: string
  endTime?: string
  description?: boolean
}

// User
export interface UsersStore {
  allLoadedItems: User[]
  selectedItem: User
}
export interface User extends Definition {
  id?: number
  email?: string
  username?: string
  password?: string
  name?: string
  statusCode?: number
  authorityCode?: number
}

// Authentication
export interface AuthenticationStore {
  user?: User
}

// Settings
export interface SettingsStore {
  useDarkMode: boolean
}
