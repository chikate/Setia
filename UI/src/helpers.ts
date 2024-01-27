import { API_BASE_URL } from '@/constants'

export async function makeRequest(path: string, method: string, body?: any) {
  return await fetch(API_BASE_URL + path, {
    method: method,
    body: body
  })
}

export async function checkRequest(response: Response) {
  return response.json()
}
