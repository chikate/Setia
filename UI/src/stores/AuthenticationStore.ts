import { defineStore } from 'pinia'

interface AuthenticationStore {
  user: User | null
}

interface User {
  name: string
  coins: number
}

export const useAuthenticationStore = defineStore('AuthenticationStore', {
  state: (): AuthenticationStore => {
    return {
      user: null
    }
  },
  actions: {
    async tryLogin(username: string, password: string) {
      try {
        const response = await fetch(
          `https://localhost:44381/api/User/Login?username=${username}&password=${password}`,
          {
            method: 'POST',
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            }
          }
        )

        if (!response.ok) {
          throw new Error('Network response was not ok')
        }

        const data = await response.json()
        this.user = data
        console.log(data)
      } catch (error) {
        console.error(error)
      }
    },
    async tryRegister(email: string, username: string, password: string) {
      try {
        const response = await fetch(
          `https://localhost:44381/api/User/Register?email=${email}&username=${username}&password=${password}`,
          {
            method: 'POST',
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            }
          }
        )

        if (!response.ok) {
          throw new Error('Network response was not ok')
        }

        const data = await response.json()
        this.user = data
        console.log(data)
      } catch (error) {
        console.error(error)
      }
    }
  }
})
