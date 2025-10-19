<template>
  <div class="flex flex-column gap-2 w-full">
    <LiveVideo v-if="false" :comments="[]" />
    <div class="relative flex h-full overflow-auto">
      <TabMenu
        class="w-full"
        v-model:activeIndex="activeTabIndex"
        scrollable
        :model="[
          { name: 'About', image: '' },
          { name: 'Posts', image: '', count: '11' },

          { name: 'Reposts', image: '', count: '35K' },
          { name: 'Likes', image: '', count: '100K' },
          { name: 'Favorites', image: '', count: '46K' },

          { separator: true },

          { name: 'Followers', image: '', count: '10M' },
          { name: 'Following', image: '', count: '324' },
          { name: 'Friends', image: '', count: '123' },
        ]"
      >
        <template #item="{ item, index, props }">
          <div class="flex flex-column h-full justify-content-end p-2">
            <div v-if="item.name == 'About'" class="flex flex-row gap-2">
              <Avatar
                class="shadow-1"
                :image="profileUserData.avatar"
                style="height: 100px; width: 100px"
                shape="circle"
                @click="showUploadAvatar = isMyProfile && !showUploadAvatar"
                :class="isMyProfile ? 'cursor-pointer' : undefined"
              />
              <div class="flex flex flex-column justify-content-around">
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
              :class="{ 'font-bold': activeTabIndex == index }"
              v-else
            >
              <label class="cursor-pointer">{{ item.name }}</label>
              <Badge class="font-bold" :value="item.count" />
            </div>
          </div>
        </template>
      </TabMenu>
      <div class="absolute top-0 right-0 flex flex-row-reverse gap-2 p-2">
        <Button icon="pi pi-heart" label="Subscribe" />
        <Button icon="pi pi-heart" label="Follow" />
      </div>
    </div>
    <ProfileAbout v-if="activeTabIndex == 0" />

    <PostsList v-if="activeTabIndex == 1" />
    <PostsList v-if="activeTabIndex == 2" />
    <PostsList v-if="activeTabIndex == 3" />
    <PostsList v-if="activeTabIndex == 4" />

    <UsersList v-if="activeTabIndex == 6" />
    <UsersList v-if="activeTabIndex == 7" />
    <UsersList v-if="activeTabIndex == 8" />
  </div>
</template>

<script setup lang="ts">
const user = ref();

defineOptions({
  name: "Profile",
  icon: "👤",
});

const profileUserData = ref<User>({ avatar: "" } as User);
const showUploadAvatarDialog = ref();

const isMyProfile = ref<boolean>(false);

const activeTabIndex = ref(0);

onBeforeMount(init);
async function init() {
  profileUserData.value = await helperService.getUserProfile(
    String(useRoute().params.profile)
  );
  isMyProfile.value = profileUserData.value.id == user?.id;
}
</script>

<style scoped>
:deep().p-tabmenu-tablist {
  align-items: end;
}
:deep().p-tabmenu-item {
  cursor: pointer;
  user-select: auto;
}
:deep().p-tabmenu-tablist {
  overflow-x: auto;
  overflow-y: hidden;
}
/* :deep().p-tabmenu-item:hover {
  background-color: teal;
  color: aliceblue;
  transition: all 0.1s ease-in-out;
  border-top-left-radius: var(--border-radius);
  border-top-right-radius: var(--border-radius);
} */
</style>
