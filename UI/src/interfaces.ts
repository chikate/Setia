export interface Audit {
  creationDate?: string
  createdBy?: User
  lastUpdate?: string
  lastUpdateBy?: User
}

export interface Definition extends Audit {
  active?: boolean
  deleted?: boolean
}

// Stores
export interface PontajStore {
  allLoadedItems: Pontaj[]
  selectedItem: Pontaj
}
export interface UsersStore {
  allLoadedItems: User[]
  selectedItem: User
}

export interface Pontaj extends Definition {
  id?: number
  user?: User
  beginTime: string
  endTime: string
  description: string
}
export interface User extends Definition {
  id?: number
  email: string
  username: string
  password?: string
  name: string
  statusCode: number
  authorityCode: number
}

// Authentication
export interface AuthenticationStore {
  user: User
}

// Settings
export interface SettingsStore {
  useDarkMode: boolean
}
