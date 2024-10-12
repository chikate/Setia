export const useAppStore = defineStore('App', () => {
  const appSplitterDistribution = computed((): number =>
    useRoute()?.fullPath.includes('home') ? 40 : 15
  )

  return {
    appSplitterDistribution
  }
})
