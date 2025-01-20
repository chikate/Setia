// Global code

import { app } from '@/main'
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export const apiRequest = async (
  path: string,
  body?: any,
  method: string = 'GET',
  headers: HeadersInit = {
    Authorization: `Bearer ${localStorage.getItem('token')}`,
    ContentType: body instanceof FormData ? '' : method == 'GET' ? 'text/plain' : 'application/json'
  }
): Promise<any> => {
  try {
    return await fetch(
      `/api/${path}${body && Object.values(body).every((value) => value != undefined) ? '?' + new URLSearchParams(body) : ''}`,
      {
        method,
        headers,
        body: method == 'GET' ? undefined : body instanceof FormData ? body : JSON.stringify(body)
      }
    ).then(async (response: Response) => {
      console.log(response.text())
      if (response.statusText)
        app.config.globalProperties.$toast?.add({
          summary: 'Server',
          detail: response.statusText,
          severity: response.ok
            ? 'success'
            : response.status >= 400 && response.status < 600
              ? 'error'
              : 'info'
        })

      const contentType = response.headers.get('ContentType') ?? undefined

      return contentType?.includes('application/json')
        ? response.json()
        : contentType?.includes('text/plain')
          ? response.text()
          : response.blob()
    })
  } catch (error) {
    console.error(error)
  }
}

export const capitalizeString = (input: string) => input.charAt(0).toUpperCase() + input.slice(1)

export const capitalizeWords = (input: string) =>
  capitalizeString(
    input.replace(/\b\w+/g, (word) =>
      ['if', 'of', 'to', 'at', 'a', 'with', 'my'].includes(word.toLowerCase())
        ? word
        : capitalizeString(word)
    )
  )

export const canUserAccessRoute = async (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) => {
  next()
  // if (await authService.checkUserRight(to.fullPath)) next()
  // else
  //   console.log(
  //     `you dont have permision to acces this page so you will remain on ${from.fullPath + to.hash}`
  //   )
}

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

export function stringToColor(str: string, alpha: number): string {
  // Create a hash from the string
  let hash = 0
  for (let i = 0; i < str.length; i++) {
    hash = str.charCodeAt(i) + ((hash << 5) - hash)
  }

  // Convert the hash to an RGB color
  const r = (hash >> 16) & 0xff
  const g = (hash >> 8) & 0xff
  const b = hash & 0xff

  // Return the color in RGBA format
  return `rgba(${r}, ${g}, ${b}, ${alpha ?? 0.6})`
}
