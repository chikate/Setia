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
    class="flex flex-column border-round justify-content-center align-self-start h-full overflow-auto"
  >
    <Divider layout="vertical" />
    <img :src="``" />
    <div class="flex flex-column">
      <label>Title{{ title }}</label>
      <label class="text-justify">Asdasd{{ description }}</label>
      <label class="text-justify">{{ price }}</label>
    </div>
  </div>
</template>
