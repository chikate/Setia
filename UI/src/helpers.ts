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
  // toast
  return response.json()
}

export function capitalizeString(input: string): string {
  // Check if the input string is not empty
  if (input.length === 0) {
    return input // Return the input string as is if it's empty
  }

  // Capitalize the first letter and concatenate it with the rest of the string
  return input.charAt(0).toUpperCase() + input.slice(1)
}
