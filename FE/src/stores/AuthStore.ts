import { makeApiRequest } from '@/helpers'
import type { IAuthenticationDTO, User } from '@/interfaces'

export const useAuthStore = defineStore('Auth', () => {
  const token = ref<string>()
  const userData = ref<User>()

  async function login(loginCredentials: IAuthenticationDTO): Promise<Boolean> {
    return await makeApiRequest(`Auth/Login`, 'post', loginCredentials).then(
      async (loginResponse: Response) => {
        if (loginResponse.status == 200) {
          const loginResult = await loginResponse.json()
          // this.token = loginResult.token
          localStorage.setItem('token', loginResult.token)
          // this.userData = loginResult.user
          localStorage.setItem('user', JSON.stringify(loginResult.user))
          // window.location.reload()
        }
        return loginResponse.status == 200
      }
    )
  }

  async function register(email: string, username: string, password: string): Promise<Boolean> {
    return await makeApiRequest(`Auth/Register`, 'post', {
      email,
      username,
      password
    }).then((registerResponse: Response) => {
      return registerResponse.status == 200
    })
  }

  function checkUserRight(right: string): boolean {
    return Boolean(userData.value?.tags.find((tag) => tag.includes(right)))
  }
  // checkUserRights(tag?: string | string[]): boolean {
  //   // return Boolean((this.userData.tags.indexOf(tag) ?? -1) > 0)
  //   return false
  // },
  async function logOut() {
    // this.token = undefined
    // this.userData = undefined
    localStorage.setItem('token', '')
    localStorage.setItem('user', '')
    return window.location.replace('/')
  }

  return {
    token,
    userData,
    login,
    register,
    checkUserRight,
    logOut
  }
})
