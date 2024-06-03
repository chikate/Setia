import { API_URL } from '@/constants'

export async function makeApiRequest(path: string, method: string, body?: any): Promise<Response> {
  // Manage headers
  const headers = new Headers()
  if (!(body instanceof FormData)) {
    headers.append(
      'Content-Type',
      `${method.toUpperCase() == 'GET' ? 'text/plain' : body ? 'application/json' : 'x-www-form-urlencoded'}`
    )
  }
  // headers.append('Authorization', `Bearer ${useAuthStore().token ?? ''}`)
  headers.append('Authorization', `Bearer ${localStorage.getItem('token')}`)

  // && !Array.isArray(body)

  // curl -X 'GET'
  // 'https://localhost:44381/api/Tags/Get?
  //  filters=%7B%0A%20%20%22active%22%3A%20true%2C%0A%20%20%22tag%22%3A%20%7B%7D%2C%0A%20%20%22comments%22%3A%20%22string%22%0A%7D
  // &filters=%7B%0A%20%20%22active%22%3A%20true%2C%0A%20%20%22tag%22%3A%20%7B%7D%2C%0A%20%20%22comments%22%3A%20%22string%22%0A%7D'
  // -H 'accept: text/plain'

  //  filters=%7B%22tag%22%3A%22Controller.*%22%2C%22comments%22%3A%22a%22%7D&filters=%7B%22tag%22%3A%22Con2%22%7D
  // let urlParams = ''
  // if (method.toUpperCase() == 'GET') {
  //   urlParams = Object.values(body)
  //     ?.map((filter) => 'filters=' + encodeURIComponent(JSON.stringify(filter)))
  //     .join('&')
  // }
  // console.log(urlParams)

  // Fetch function
  return await fetch(API_URL + path, {
    method,
    headers,
    body:
      body && method.toUpperCase() != 'GET'
        ? body instanceof FormData
          ? body
          : JSON.stringify(body)
        : undefined
  })

  // await response

  // if (response.status === 200) {
  //   if (response.headers.get('Content-Type')?.includes('application/json')) {
  //     return await response.json()
  //   }
  //   if (response.headers.get('Content-Type')?.includes('text/plain')) {
  //     return await response.text()
  //   } else {
  //     return response
  //   }
  // }

  // return response.status
}

export function capitalizeString(input: string): string {
  return input[0].toUpperCase() + input.substring(1)
}

export function isValidISODate(dateString: string): boolean {
  const isoDatePattern = /^\d{4}-\d{2}-\d{2}$/
  if (!isoDatePattern.test(dateString)) {
    return false
  }
  const date = new Date(dateString)
  return !isNaN(date.getTime()) && dateString === date.toISOString().split('T')[0]
}
