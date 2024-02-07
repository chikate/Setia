import { API_URL } from '@/constants'

export async function makeApiRequest(path: string, method: string, body?: any): Promise<any> {
  const response = await fetch(API_URL + path, {
    method: method.toUpperCase(),
    headers: {
      'Content-Type': `application/${body ? 'json' : 'x-www-form-urlencoded'}`,
      Authorization: `Bearer ${localStorage.getItem('token')}`
    },
    body: body ? JSON.stringify(body) : undefined
  })

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
}

export function capitalizeString(input: string): string {
  return input.charAt(0).toUpperCase() + input.slice(1)
}
