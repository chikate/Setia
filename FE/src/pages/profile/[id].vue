<script setup lang="ts">
import type { Post, User } from '@/global/interfaces'

const profileUserData = ref<User>({} as User)
const showUploadAvatar = ref()
const avatarUrl = ref<string>('')

const isMyProfile = computed<boolean>(() => profileUserData.value.id === authService.user?.id)

const loadingComplete = ref<boolean>(false)

onBeforeMount(init)
async function init() {
  try {
    profileUserData.value = await helperService.getUserProfile(
      String((useRoute().params as any).name)
    )

    loadingComplete.value = true
  } catch {
    useRouter().push('/profile')
  }
}
</script>

<!-- <FileUpload
class="opacity-0"
mode="basic"
name="demo[]"
accept="image/*"
customUpload
@uploader="helperStore().uploadFiles($event.files)"
:maxFileSize="1000000"
/> -->

<template>
  <div v-show="loadingComplete" class="flex-column justify-content-center">
    <div class="flex flex-column gap-4">
      <div class="flex flex-wrap align-items-center gap-3">
        <Avatar
          class="shadow-1"
          :image="profileUserData.avatar"
          size="xlarge"
          shape="circle"
          @click="showUploadAvatar = isMyProfile && !showUploadAvatar"
          :class="isMyProfile ? 'cursor-pointer' : undefined"
        />
        <div class="flex flex-column">
          <a class="font-bold">{{ profileUserData.username }}</a>
          <a class="opacity-50">
            Joined
            {{ new Date(profileUserData.executionDate ?? '').toDateString().toLowerCase() }}
          </a>
        </div>
      </div>
      <PostComponent
        v-if="profileUserData.id == authService().userData?.id"
        :post-data="usePostsCRUDStore().editItem"
      />
    </div>
    <div
      class="flex flex-column gap-4 py-4"
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
            (helperService.getCurentUserAvatar(avatarUrl),
            (profileUserData.avatar = avatarUrl),
            (showUploadAvatar = false))
          "
        />
      </div>
    </Dialog>
  </div>
</template>
