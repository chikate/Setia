import { API_URL } from '@/constants'
import { useAuthStore } from './stores/AuthStore'

export async function makeApiRequest(path: string, method: string, body?: any): Promise<any> {
  // Manage headers
  const headers = new Headers()
  if (!(body instanceof FormData)) {
    headers.append('Content-Type', `application/${body ? 'json' : 'x-www-form-urlencoded'}`)
  }
  headers.append('Authorization', `Bearer ${useAuthStore().token ?? ''}`)

  // Fetch function
  const response = await fetch(
    API_URL +
      path +
      // Auto append parameters for [FromQuerry]
      (body && method.toLowerCase() == 'get'
        ? `?${Object.keys(body)
            .map((elem) => `${elem}=${Object.values(elem)}`)
            .join('&')}`
        : ''),
    {
      method,
      headers,
      body: body instanceof FormData ? body : JSON.stringify(body)
    }
  )

  if (response.status === 200) {
    if (response.headers.get('Content-Type')?.includes('application/json')) {
      return await response.json()
    }
    if (response.headers.get('Content-Type')?.includes('text/plain')) {
      return await response.text()
    } else {
      return response
    }
  }

  return response.status
}

export function capitalizeString(input: string): string {
  return input.charAt(0).toUpperCase() + input.slice(1)
}
