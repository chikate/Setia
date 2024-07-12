import type { INotification } from '@/interfaces'
import { useCRUDStore } from '../CRUDStore'

export const useNotificationsCRUDStore = useCRUDStore('Notifications', {} as INotification)
