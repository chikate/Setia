import { apiRequest } from '@/helpers'

const storeName = 'FileManager'

export const driveStore = defineStore('driveStore', () => {
  const upload = async (formFile: FormData, compressIt: boolean) =>
    await apiRequest(`${storeName}/Upload`, { formFile, compressIt }, 'post')

  const download = async (filePath: string) =>
    await apiRequest(`${storeName}/Download`, { filePath })

  const getFolderContent = async (filePath?: string) =>
    await apiRequest(`${storeName}/GetFolderContent`, { filePath })

  const getAllPartitions = async () => await apiRequest(`${storeName}/GetAllPartitions`)

  const GetFileRegistryNumber = async (filePath: string) =>
    await apiRequest(`${storeName}/GetFileRegistryNumber`, { filePath })

  const GetFileFromRegistryNumber = async (regestryNumber: string) =>
    await apiRequest(`${storeName}/GetFileFromRegistryNumber`, { regestryNumber })

  return {
    upload,
    download,
    getFolderContent,
    getAllPartitions,
    GetFileRegistryNumber,
    GetFileFromRegistryNumber
  }
})
