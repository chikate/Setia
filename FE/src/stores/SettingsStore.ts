export const settingsStore = defineStore('settingsStore', () => {
  const INPUT_CLASS = ref<string>(`m-0 p-2 px-3 justify-content-start min-w-13px w-13px max-w-13px`)
  const DEFAULT_ROWS_OPTIONS = ref<number[]>([15, 30, 60, 100])
  const DEFAULT_ROWS_INDEX = ref<number>(0)
  const theme = ref<string>('')
  const language = ref<string>('en')

  async function toggleDarkMode() {
    const linkElement = document.getElementById('theme-link')
    if (linkElement) {
      localStorage.setItem(
        'theme',
        (linkElement as HTMLAnchorElement).href.includes('dark') ? 'light' : 'dark'
      )
      ;(linkElement as HTMLAnchorElement).href =
        `/node_modules/primevue/resources/themes/aura-${localStorage.getItem('theme')}-noir/theme.css`
    }
  }

  return {
    INPUT_CLASS,
    DEFAULT_ROWS_OPTIONS,
    DEFAULT_ROWS_INDEX
  }
})
