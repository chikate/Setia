import { API_URL } from '@/constants'

export async function makeRequest(path: string, method: string, body?: any): Promise<Response> {
  return await fetch(API_URL + path, {
    method: method.toUpperCase(),
    headers: {
      'Content-Type': `application/${body ? 'json' : 'x-www-form-urlencoded'}`
    },
    body: body ? JSON.stringify(body) : undefined
  })
}

export async function checkRequest(response: Response) {
  return response.json()
}
