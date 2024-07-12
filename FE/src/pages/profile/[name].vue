<script setup lang="ts">
import type { Post, User } from '@/interfaces'

const profileUserData = ref<User>({} as User)
const router = useRouter()
const showUploadAvatar = ref()
const avatarUrl = ref<string>('')
const isMyProfile = computed<boolean>(
  () => profileUserData.value?.id === useAuthStore().userData?.id
)

const loadingComplete = ref<boolean>(false)
onBeforeMount(async () => {
  try {
    profileUserData.value = await useHelperStore().getUserProfile(
      String((useRoute().params as any)?.name)
    )

    loadingComplete.value = true
  } catch {
    router.push('/profile')
  }
})
</script>

<!-- <FileUpload
class="opacity-0"
mode="basic"
name="demo[]"
accept="image/*"
customUpload
@uploader="useHelperStore().uploadFiles($event.files)"
:maxFileSize="1000000"
/> -->

<template>
  <main v-show="loadingComplete" class="max-h-screen flex-row gap-4">
    <PanelMenu
      toggleall
      :model="[
        {
          label: 'Security',
          icon: 'pi pi-file',
          items: [
            {
              label: 'Recovery',
              icon: 'pi pi-file',
              items: [
                {
                  label: 'Invoices',
                  icon: 'pi pi-file-pdf',
                  items: [
                    {
                      label: 'Pending',
                      icon: 'pi pi-stop'
                    },
                    {
                      label: 'Paid',
                      icon: 'pi pi-check-circle'
                    }
                  ]
                },
                {
                  label: 'Clients',
                  icon: 'pi pi-users'
                }
              ]
            },
            {
              label: 'Recover password',
              icon: 'pi pi-image'
            },
            {
              label: 'Change password',
              icon: 'pi pi-image'
            }
          ]
        },
        {
          label: 'Cloud',
          icon: 'pi pi-cloud',
          items: [
            {
              label: 'Upload',
              icon: 'pi pi-cloud-upload'
            },
            {
              label: 'Download',
              icon: 'pi pi-cloud-download'
            },
            {
              label: 'Sync',
              icon: 'pi pi-refresh'
            }
          ]
        },
        {
          label: 'Devices',
          icon: 'pi pi-desktop',
          items: [
            {
              label: 'Phone',
              icon: 'pi pi-mobile'
            },
            {
              label: 'Desktop',
              icon: 'pi pi-desktop'
            },
            {
              label: 'Tablet',
              icon: 'pi pi-tablet'
            }
          ]
        }
      ]"
      multiple
    />
    <div class="flex flex-column gap-4">
      <div class="flex flex-wrap align-items-center gap-3">
        <Avatar
          class="shadow-1"
          :image="profileUserData?.avatar"
          size="xlarge"
          shape="circle"
          @click="showUploadAvatar = isMyProfile && !showUploadAvatar"
          :class="isMyProfile ? 'cursor-pointer' : undefined"
        />
        <div class="flex flex-column">
          <a class="font-bold">{{ profileUserData?.username }}</a>
          <a class="opacity-50">
            Joined {{ new Date(profileUserData?.executionDate ?? '').toDateString().toLowerCase() }}
          </a>
        </div>
      </div>
      <PostComponent
        v-if="profileUserData?.id == useAuthStore().userData?.id"
        :post-data="usePostsCRUDStore().editItem"
      />
    </div>
    <div
      class="flex flex-column gap-4 py-4 overflow-y-auto"
      style="max-height: 90vh"
      @vue:beforeMount="usePostsCRUDStore().get()"
    >
      <PostComponent
        v-for="(postData, i) in usePostsCRUDStore().allLoadedItems?.filter(
          (post: Post) =>
            post.author === profileUserData.username && (post.tags.indexOf('Post') ?? -1) > -1
        )"
        :key="i"
        :post-data="postData"
      />
    </div>
    <Dialog v-model:visible="showUploadAvatar" modal>
      <div class="align-items-center w-full flex flex-column gap-3">
        <Image :src="avatarUrl" width="250" preview imageClass="border-round" />
        <InputText
          @vue:beforeMount="avatarUrl = profileUserData.avatar"
          v-model="avatarUrl"
          @keypress.enter="
            useHelperStore().setUserAvatar(avatarUrl),
              (profileUserData.avatar = avatarUrl),
              (showUploadAvatar = false)
          "
        />
      </div>
    </Dialog>
  </main>
</template>
