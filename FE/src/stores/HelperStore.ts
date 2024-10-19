import { apiRequest } from '@/helpers'

export const helperStore = defineStore('helperStore', () => {
  const allLoadedActions = ref<object[]>([])
  const focusedActions = ref<string>('')

  async function uploadFiles(file: File | File[]) {
    const formData = new FormData()
    if (Array.isArray(file)) {
      file.forEach((file: File, index: number) => {
        formData.append(`files[${index}]`, file)
        formData.append(`files`, file)
      })
    }
    await apiRequest(`Helper/Upload`, formData, 'post')
  }

  const getUserProfile = (username: string) => apiRequest(`Helper/GetUserProfile`, { username })

  const getCurentUserAvatar = (avatarUrl: string) =>
    apiRequest(`Helper/GetCurentUserAvatar`, { avatarUrl })

  const sendFriendRequest = (username: string) =>
    apiRequest(`Helper/SendFriendRequest`, { username })

  const getQuestionAnswereDistribution = (questionId: string) =>
    apiRequest(`QuestionAnswers/GetQuestionAnswereDistribution`, { questionId })

  return {
    allLoadedActions,
    focusedActions,
    uploadFiles,
    getUserProfile,
    getCurentUserAvatar,
    sendFriendRequest,
    getQuestionAnswereDistribution
  }
})
