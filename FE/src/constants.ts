export const ENV_NAME: string = import.meta.env.VITE_ENV_NAME
export const API_URL: string = `https://${import.meta.env.VITE_SERVER}/api/`
export const WEB_SOCKET_URL: string = `ws://${import.meta.env.VITE_SERVER}`

export const TOAST_BASE_HP: number = 3000

export const DEFAULT_ROWS_OPTIONS: number[] = [15, 30, 60, 100]
export const DEFAULT_ROWS_INDEX: number = 0
