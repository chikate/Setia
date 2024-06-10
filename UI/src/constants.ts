export const API_URL: string = import.meta.env.VITE_API_URL
export const TOAST_BASE_HP: number = 3000
export const DEFAULT_ROWS_OPTIONS: number[] = [15, 30, 60, 100]
export const DEFAULT_ROWS_INDEX: number = 1
export const DARK_THEME_BACKGROUND_IMAGE: string =
  'https://www.ukri.org/wp-content/uploads/2021/10/STFC-041021-EuropeFromSpace-GettyImages-1284041267.jpg'

export const LIGHT_THEME_BACKGROUND_IMAGE: string =
  'https://cdn.shopify.com/s/files/1/1799/9277/files/How_to_create_views_of_real_nature_in_your_interiors_Learn_more_about_our_visual_connection_with_nature.3_2048x2048.jpg'

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
