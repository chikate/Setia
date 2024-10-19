import { apiRequest } from '@/helpers'
import type { IAuthenticationDTO, User } from '@/interfaces'

export const authStore = defineStore('authStore', () => {
  const login = (loginCredentials: IAuthenticationDTO) =>
    apiRequest(`Auth/Login`, loginCredentials).then((response) => {
      localStorage.setItem('token', response.token)
      localStorage.setItem('user', JSON.stringify(response.user))
      return true
    })

  const register = (email: string, username: string, password: string) =>
    apiRequest(`Auth/Register`, { email, username, password }, 'post')

  const recoverAccount = (email: string) => apiRequest(`Auth/RecoverAccount`, { email }, 'post')

  const checkUserRight = (right: string) =>
    (JSON.parse(String(localStorage.getItem('user'))) as User)?.tags.find((tag) =>
      tag.includes(right)
    )

  async function logOut() {
    localStorage.clear()
  }

  return {
    register,
    recoverAccount,
    login,
    logOut,
    checkUserRight
  }
})
