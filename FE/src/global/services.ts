import type {
  IAuthenticationDTO,
  User,
  INotification,
  UserCollection,
  Pontaj,
  Post,
  QuestionAnswer,
  Question,
  Taging
} from '@/global/interfaces'

export const helperService = new (class HelperService {
  getUserProfile = async (username: string) =>
    await apiRequest(`Helper/GetUserProfile`, { username })

  getCurentUserAvatar = async (avatarUrl: string) =>
    await apiRequest(`Helper/GetCurentUserAvatar`, { avatarUrl })

  sendFriendRequest = async (username: string) =>
    await apiRequest(`Helper/SendFriendRequest`, { username })

  getQuestionAnswereDistribution = async (questionId: string) =>
    await apiRequest(`QuestionAnswers/GetQuestionAnswereDistribution`, { questionId })
})()

export const fileManager = new (class FileManager {
  upload = async (formFile: FormData, compressIt: boolean) =>
    await apiRequest(`FileManager/Upload`, { formFile, compressIt }, 'post')

  download = async (filePath: string) => await apiRequest(`FileManager/Download`, { filePath })

  SearchAndGetFile = async (input: string) =>
    await apiRequest(`FileManager/SearchAndGetFile`, { input })

  getFolderContent = async (filePath?: string) =>
    await apiRequest(`FileManager/GetFolderContent`, { filePath })

  getAllPartitions = async () => await apiRequest(`FileManager/GetAllPartitions`)

  GetFileRegistryNumber = async (filePath: string) =>
    await apiRequest(`FileManager/GetFileRegistryNumber`, { filePath })

  GetFileFromRegistryNumber = async (regestryNumber: string) =>
    await apiRequest(`FileManager/GetFileFromRegistryNumber`, { regestryNumber })
})()

export const authService = new (class AuthService {
  user = JSON.parse(localStorage.getItem('user') ?? 'null')
  login = async (loginCredentials: IAuthenticationDTO) =>
    await apiRequest(`Auth/Login`, loginCredentials).then((response) => {
      localStorage.setItem('token', response)
      // localStorage.setItem('user', JSON.stringify(response.user))
      return true
    })

  register = (email: string, username: string, password: string) =>
    apiRequest(`Auth/Register`, { email, username, password }, 'post')

  recoverAccount = (email: string) => apiRequest(`Auth/RecoverAccount`, { email }, 'post')

  checkUserRight = (right: string) =>
    (JSON.parse(String(localStorage.getItem('user') ?? 'null')) as User)?.tags.find((tag: string) =>
      tag.includes(right)
    )

  logOut = () => localStorage.clear()
})()

class CRUDService {
  name: string = ''
  defaultValues: object = {}
  loadedItems: Array<any> = JSON.parse(localStorage.getItem('LoadedItemsFor' + this.name) ?? 'null')

  constructor(name: string, defaultValues: any) {
    this.name = name
    this.defaultValues = defaultValues
  }

  async getItems(getFilter?: any) {
    localStorage.setItem(
      'LoadedItemsFor' + this.name,
      JSON.stringify(await apiRequest(`${this.name}/Get`, getFilter))
    )
    return this.loadedItems
  }

  addItems = async (items?: (typeof this.defaultValues)[]) =>
    await apiRequest(`${this.name}/Add`, items, 'post').then((response) => {
      if (response) this.getItems()
      return response
    })

  updateItems = async (items?: (typeof this.defaultValues)[]) =>
    await apiRequest(`${this.name}/Update`, items, 'put').then((response) => {
      if (response) this.getItems()
      return response
    })

  deleteItems = async (ids?: string[]) =>
    await apiRequest(`${this.name}/Delete`, ids, 'delete').then((response) => {
      if (response) this.getItems()
      return response
    })
}

export const notificationsCRUDService = new CRUDService('Notifications', {} as INotification)
export const userCollectionCRUDService = new CRUDService('UserCollection', {} as UserCollection)

export const pontajCRUDService = new CRUDService('Pontaj', {
  beginTime: new Date().toISOString(),
  endTime: '',
  description: ''
} as Pontaj)

export const postsCRUDService = new CRUDService('Posts', {
  message: ''
} as Post)

export const questionAnswersCRUDService = new CRUDService('QuestionAnswers', {
  author: '',
  questionData: { title: '' },
  answer: []
} as unknown as QuestionAnswer)

export const questionsCRUDService = new CRUDService('Questions', {
  title: 'Title',
  options: [],
  selection: [],
  author: undefined
} as unknown as Question)

export const tagsCRUDService = new CRUDService('Tags', {
  tag: '',
  comments: ''
} as Taging)

export const usersCRUDService = new CRUDService('Users', {
  username: '',
  email: '',
  name: '',
  tags: []
} as unknown as User)
