import { API_URL } from '@/constants'
// import { useAuthStore } from './stores/AuthStore'

export async function makeApiRequest(path: string, method: string, body?: any): Promise<any> {
  // Manage headers
  const headers = new Headers()
  if (!(body instanceof FormData)) {
    headers.append(
      'Content-Type',
      `application/${method == 'get' && body ? 'x-www-form-urlencoded' : body ? 'json' : 'x-www-form-urlencoded'}`
    )
  }
  // headers.append('Authorization', `Bearer ${useAuthStore().token ?? ''}`)
  headers.append('Authorization', `Bearer ${localStorage.getItem('token')}`)

  // Fetch function
  const response = await fetch(
    API_URL +
      path +
      // Auto append parameters for [FromQuery]
      (body && method.toUpperCase() == 'GET'
        ? `?${Object.keys(body)
            .map((elem) => `${elem}=${body[elem]}`)
            .join('&')}`
        : ''),
    {
      method,
      headers,
      body:
        body && method.toUpperCase() != 'GET'
          ? body instanceof FormData
            ? body
            : capitalizeKeysJSON(body)
          : undefined
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

export function capitalizeKeysJSON(obj: any): string {
  if (Array.isArray(obj)) {
    return JSON.stringify(obj.map(capitalizeKeysJSON))
  } else if (obj !== null && typeof obj === 'object') {
    return JSON.stringify(
      Object.keys(obj).reduce(
        (acc, key) => {
          const capitalizedKey = capitalizeString(key)
          acc[capitalizedKey] = obj[key]
          return acc
        },
        {} as { [key: string]: any }
      )
    )
  } else {
    return obj
  }
}
