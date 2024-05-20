export const API_URL: string = import.meta.env.VITE_API_URL
export const DEFAULT_ROWS_OPTIONS: number[] = [5, 10, 20, 30]
export const DEFAULT_ROWS_INDEX: number = 1
export const FREE_DAYS = [
  new Date('1/1/' + new Date().getFullYear()),
  new Date('1/2/' + new Date().getFullYear()),
  new Date('1/6/' + new Date().getFullYear()),
  new Date('1/7/' + new Date().getFullYear()),
  new Date('1/24/' + new Date().getFullYear()),

  new Date('5/1/' + new Date().getFullYear()),
  new Date('5/5/' + new Date().getFullYear()),

  new Date('6/5/' + new Date().getFullYear()),
  new Date('6/23/' + new Date().getFullYear()),

  new Date('8/15/' + new Date().getFullYear()),

  new Date('9/30/' + new Date().getFullYear()),

  new Date('12/1/' + new Date().getFullYear()),
  new Date('12/25/' + new Date().getFullYear()),
  new Date('12/26/' + new Date().getFullYear()),
  new Date('12/31/' + new Date().getFullYear())
]
