import { API_URL } from '@/constants'
import { useToast } from 'primevue/usetoast'

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
  await toast()
  return response.json()
}

export function capitalizeString(input: string): string {
  return input.charAt(0).toUpperCase() + input.slice(1)
}

async function toast() {
  useToast().add({
    severity: 'success',
    summary: 'Success Message',
    detail: 'response.statusText',
    group: 'bl',
    life: 3000
  })
}
