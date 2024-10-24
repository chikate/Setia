import { apiRequest } from '@/helpers'

const storeName = 'FileManager'

export const driveStore = defineStore('driveStore', () => {
  const upload = async (formFile: FormData, compressIt: boolean) =>
    await apiRequest(`${storeName}/Upload`, { formFile, compressIt }, 'post')

  const download = async (filePath: string) =>
    await apiRequest(`${storeName}/Download`, { filePath })

  const getFolderContent = async (filePath?: string) =>
    await apiRequest(`${storeName}/GetFolderContent`, filePath)

  const getAllPartitions = async () => await apiRequest(`${storeName}/GetAllPartitions`)

  const getRegistryNumberFromFile = async (filePath: string) =>
    await apiRequest(`${storeName}/GetRegistryNumberFromFile`, filePath)

  return {
    upload,
    download,
    getFolderContent,
    getAllPartitions,
    getRegistryNumberFromFile
  }
})
