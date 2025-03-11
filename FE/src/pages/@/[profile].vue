<template>
  <div v-show="loadingComplete" class="flex-column p-2 gap-2 overflow-auto">
    <div class="flex flex-column gap-2">
      <LiveVideo :comments="[]" />
      <div class="relative flex border-round overflow-auto">
        <TabMenu
          class="w-full"
          v-model:activeIndex="activeIndex"
          scrollable
          :model="[
            { name: 'About', image: '', count: ' ' },
            { name: 'Posts', image: '', count: '11' },

            { name: 'Followers', image: '', count: '10M' },
            { name: 'Following', image: '', count: '324' },
            { name: 'Friends', image: '', count: '123' },

            { name: 'Reposts', image: '', count: '35K' },
            { name: 'Likes', image: '', count: '100K' },
            { name: 'Favorites', image: '', count: '46K' },
          ]"
        >
          <template #item="{ item, props }">
            <div class="flex-column h-full justify-content-end p-2">
              <div v-if="item.name == 'About'" class="flex-row gap-2">
                <Avatar
                  class="shadow-1"
                  :image="profileUserData.avatar"
                  style="height: 100px; width: 100px"
                  shape="circle"
                  @click="showUploadAvatar = isMyProfile && !showUploadAvatar"
                  :class="isMyProfile ? 'cursor-pointer' : undefined"
                />
                <div class="flex flex-column justify-content-around">
                  <label id="username" class="font-bold text-2xl">
                    {{ profileUserData.username }}
                    {{ $route.params.profile }}
                  </label>
                  <label>Ceva de scris aici</label>
                  <div class="flex-wrap gap-2">
                    <label>Playing</label>
                    <a
                      id="activity"
                      class="font-medium cursor-pointer hover:underline"
                    >
                      League of Legends
                    </a>
                  </div>
                  <a class="opacity-50" v-if="profileUserData.executionDate">
                    Joined
                    {{
                      new Date(profileUserData.executionDate ?? "")
                        .toDateString()
                        .toLowerCase()
                    }}
                  </a>
                </div>
              </div>
              <div
                class="flex gap-1 align-items-center cursor-pointer gap-2"
                v-else
              >
                <label class="cursor-pointer">{{ item.name }}</label>
                <Badge class="font-bold" :value="item.count" />
              </div>
            </div>
          </template>
        </TabMenu>
        <div class="absolute right-0 flex-row gap-2">
          <Button icon="pi pi-heart" label="Follow" />
          <Button icon="pi pi-heart" label="Subscribe" />
        </div>
      </div>
      <div class="flex-wrap" v-if="activeIndex == 1">
        <PostComponent v-if="profileUserData.id == authService.userData?.id" />
      </div>
      <ProfileAbout v-if="activeIndex == 1" />
      <!-- <PostComponent
        v-if="profileUserData.id == authService.userData?.id"
        :post-data="usePostsCRUDStore?.editItem"
      /> -->
    </div>
    <!-- <div
      class="flex flex-column gap-4 py-4"
      style="max-height: 90vh"
      @vue:beforeMount="usePostsCRUDStore.get()"
    >
      <PostComponent
        v-for="(postData, i) in usePostsCRUDStore.allLoadedItems?.filter(
          (post: Post) =>
            post.author === profileUserData.username && (post.tags.indexOf('Post') ?? -1) > -1
        )"
        :key="i"
        :post-data="postData"
      />
    </div> -->
    <Dialog v-model:visible="showUploadAvatar" modal>
      <div class="align-items-center w-full flex flex-column gap-3">
        <Image :src="avatarUrl" width="250" preview imageClass="border-round" />
        <InputText
          @vue:beforeMount="avatarUrl = profileUserData.avatar"
          v-model="avatarUrl"
          @keypress.enter="
            helperService.getCurentUserAvatar(avatarUrl),
              (profileUserData.avatar = avatarUrl),
              (showUploadAvatar = false)
          "
        />
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { count } from "console";

// import type { Post, User } from "@/global/interfaces";

const profileUserData = ref<User>({} as User);
const showUploadAvatar = ref();
const avatarUrl = ref<string>("");

const isMyProfile = computed<boolean>(
  () => profileUserData.value.id === authService.user?.id
);

const loadingComplete = ref<boolean>(false);
const activeIndex = ref(0);

onBeforeMount(init);
async function init() {
  try {
    if (useRoute().query.id != null)
      profileUserData.value = await helperService.getUserProfile(
        String(useRoute().params.profile)
      );

    loadingComplete.value = true;
  } catch {
    useRouter().push("/profile");
  }
}
</script>
