// All these constants sould exist inside a db in the future

export const INPUT_CLASS = ref<string>(
  `m-0 p-2 px-3 justify-content-start min-w-13px w-13px max-w-13px`
)
export const DEFAULT_ROWS_OPTIONS = ref<number[]>([15, 30, 60, 100])
export const DEFAULT_ROWS_INDEX = ref<number>(0)

export const TOAST_LIFETIME = ref<number>(3000)

export const FILE_ICONS: Record<string, string> = {
  folder: 'pi pi-folder',
  png: 'pi pi-image',
  jpg: 'pi pi-image',
  jpeg: 'pi pi-image',
  gif: 'pi pi-image',
  bmp: 'pi pi-image',
  svg: 'pi pi-image',
  zip: 'pi pi-book',
  rar: 'pi pi-book',
  '7z': 'pi pi-book',
  tar: 'pi pi-book',
  gz: 'pi pi-book',
  pdf: 'pi pi-file-pdf',
  doc: 'pi pi-file-word',
  docx: 'pi pi-file-word',
  xls: 'pi pi-file-excel',
  xlsx: 'pi pi-file-excel',
  ppt: 'pi pi-file-powerpoint',
  pptx: 'pi pi-file-powerpoint',
  mp3: 'pi pi-volume-up',
  wav: 'pi pi-volume-up',
  ogg: 'pi pi-volume-up',
  flac: 'pi pi-volume-up',
  mp4: 'pi pi-video',
  avi: 'pi pi-video',
  mkv: 'pi pi-video',
  mov: 'pi pi-video',
  wmv: 'pi pi-video',
  txt: 'pi pi-file-word',
  log: 'pi pi-file',
  html: 'pi pi-code',
  css: 'pi pi-code',
  js: 'pi pi-code',
  json: 'pi pi-code',
  xml: 'pi pi-code',
  exe: 'pi pi-cog',
  bat: 'pi pi-cog',
  sh: 'pi pi-cog',
  code: 'pi-code'
}

export const MENU_ICONS: Record<string, string> = {
  default: 'pi pi-circle',
  universe: 'pi pi-sun',
  administration: 'pi pi-cog',
  home: 'pi pi-home',
  downloads: 'pi pi-download',
  drive: 'pi pi-box',
  flow: 'pi pi-arrow-right-arrow-left',
  lists: 'pi pi-bars',
  code: 'pi pi-code'
}
