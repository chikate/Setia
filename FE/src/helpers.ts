import { app } from '@/main'

export const apiRequest = async (
  path: string,
  body?: any,
  method: string = 'GET',
  headers: HeadersInit = {
    Authorization: `Bearer ${localStorage.getItem('token')}`,
    'Content-Type':
      body instanceof FormData ? '' : method == 'GET' ? 'text/plain' : 'application/json'
  }
): Promise<any> =>
  await fetch(
    `https://${import.meta.env.VITE_SERVER ?? 'localhost:44381'}/api/${path}` +
      (body && Object.values(body).every((value) => value != undefined)
        ? '?' + new URLSearchParams(body)
        : ''),
    {
      method,
      headers,
      body: method == 'GET' ? undefined : body instanceof FormData ? body : JSON.stringify(body)
    }
  ).then((response: Response) => {
    if (response.statusText)
      app.config.globalProperties.$toast?.add({
        summary: 'Server',
        detail: response.statusText,
        severity:
          response.status >= 200 && response.status < 300
            ? 'success'
            : response.status >= 400 && response.status < 600
              ? 'error'
              : 'info'
      })
    return response.json()
  })

export const capitalizeString = (input: string) => input.charAt(0).toUpperCase() + input.slice(1)

export const capitalizeWords = (input: string) =>
  capitalizeString(
    input.replace(/\b\w+/g, (word) =>
      ['if', 'of', 'to', 'at', 'a', 'with', 'my'].includes(word.toLowerCase())
        ? word
        : capitalizeString(word)
    )
  )

export const canUserAccessRoute = async (to: string) => await Boolean(to)

export function isValidISODate(dateString: string): boolean {
  const isoDatePattern = /^\d{4}-\d{2}-\d{2}$/
  if (!isoDatePattern.test(dateString)) return false
  const date = new Date(dateString)
  return !isNaN(date.getTime()) && dateString === date.toISOString().split('T')[0]
}

export function downloadInBrowser(url: any, name?: string) {
  const a: HTMLAnchorElement = document.createElement('a')
  a.href = url
  a.download = String(name ?? url?.split('/')?.pop() ?? 'download').replace(/[^0-9A-Z]+/gi, '')
  document.body.appendChild(a).click()
  document.body.removeChild(a)
}
