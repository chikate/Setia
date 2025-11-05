<template>
  <div class="flex flex-row border-round">
    <div class="bg-primary border-round overflow-hidden" style="line-height: 0">
      <video
        controls
        class=""
        style="aspect-ratio: 16/9; object-fit: cover; line-height: 0"
      >
        <source
          :src="`/1/2262535539-98351197-299350c1-875d-457d-82dd-a962bfd00dde.mp4`"
          type="video/mp4"
        />
      </video>
    </div>
    <div
      style="width: 30rem"
      class="bg-primary flex flex-column p-2 border-round overflow-auto"
    >
      <div class="flex flex-column flex-grow-1">
        <label v-for="comment in comments">{{ comment }}</label>
      </div>
      <InputText @keydown.enter="handleSendComment" />
    </div>
  </div>
</template>

<script setup lang="ts">
const comments = ref<Array<string>>([]);

function handleSendComment(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement;

  if (
    !(
      !target.value ||
      target.value == "" ||
      target.value == undefined ||
      target.value == null
    )
  )
    comments.value?.push(target.value);

  target.value = "";
}
</script>
