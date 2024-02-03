import { API_URL } from '@/constants'
import { useAuthenticationStore } from './stores/AuthenticationStore'

export async function makeRequest(path: string, method: string, body?: any): Promise<any> {
  return await fetch(API_URL + path, {
    method: method.toUpperCase(),
    headers: {
      'Content-Type': `application/${body ? 'json' : 'x-www-form-urlencoded'}`,
      Authorization: `Bearer ${useAuthenticationStore().token ?? ''}`
    },
    body: body ? JSON.stringify(body) : undefined
  }).then(async (response: Response) => {
    switch (response.headers.get('Content-Type')) {
      case 'text/html':
        // HTML response
        break
      case 'text/javascript':
        // JavaScript response
        break
      case 'application/json':
        // JSON response
        return await response.json() // Successful response
      case 'text/plain':
        // Plain text response
        return await response.text()
      case 'application/xml':
        // XML response
        break
      case 'image/jpeg':
        // JPEG image response
        break
      default:
        // Handle other response types
        return response.body
    }
  })
}

export function capitalizeString(input: string): string {
  return input.charAt(0).toUpperCase() + input.slice(1)
}
