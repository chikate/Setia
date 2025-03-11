<template>
  <div class="flex-row gap-2 border-round overflow-auto">
    <div
      class="bg-primary w-full border-round overflow-auto"
      style="line-height: 0"
    >
      <video
        controls
        class="w-full"
        style="aspect-ratio: 16/9; object-fit: cover"
      >
        <source
          :src="`/1/2262535539-98351197-299350c1-875d-457d-82dd-a962bfd00dde.mp4`"
          type="video/mp4"
        />
      </video>
    </div>
    <div
      style="width: 30rem"
      class="bg-primary flex-column p-2 border-round overflow-auto"
    >
      <div class="flex-column flex-grow-1">
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
