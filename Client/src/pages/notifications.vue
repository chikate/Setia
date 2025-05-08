<script setup lang="ts">
definePage({
  meta: {
    title: "Notifications",
    description: "Notifications Center",
  },
});

// window.addEventListener("new-notification", (event) => {
//   const { detail } = event as CustomEvent;
//   console.log("Received notification:", detail);
//   notifications.value.push(detail);
// });

const notifications = ref<IPost[]>(
  JSON.parse(localStorage.getItem("notifications"))
);

onBeforeMount(init);
async function init() {}
</script>

<template>
  <div
    id="notificationsCenter"
    class="flex-column gap-2 w-full justify-content-center"
  >
    center
    <div
      id="notification"
      class="flex-row w-full align-items-center gap-1 border-1 p-2"
      v-for="notification in notifications"
      :key="notification.id"
    >
      asd
      <Avatar />
      <div>{{ notification.author }}</div>
      <label>{{ notification.message }}</label>
      <label>{{ notification.executionDate }}</label>
    </div>
  </div>
</template>
