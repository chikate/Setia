<script setup lang="ts">
import type { User } from "src/global/interfaces";

const profileUserData = ref<User>({} as User);
const isMyProfile = computed<boolean>(
  () => profileUserData.value.id == authService.user?.id
);

const props = defineProps({
  username: { type: String, required: true },
});

onBeforeMount(async () => {
  profileUserData.value = await helperService.getUserProfile(
    String(props.username)
  );
});
</script>

<template>
  <div
    class="flex flex-wrap p-2 hover:bg-gray-200 column- border-round cursor-pointer justify-content-center align-self-start"
    @click="$emit('click')"
  >
    <div class="align-self-center">
      <Avatar
        class="shadow-1"
        :image="profileUserData.avatar"
        size="large"
        shape="circle"
      />
      <Badge
        class="p-1 border-2 border-primary absolute"
        style="transform: translate(-0.8rem, 2.2rem)"
        :severity="isMyProfile ? 'success' : 'contrast'"
      />
    </div>

    <div class="flex flex flex-column text-center align-items-start">
      <a class="font-bold sm:">{{ profileUserData.username }}</a>

    </div>
  </div>
</template>
