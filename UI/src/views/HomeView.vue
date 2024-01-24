<template>
  <main
    class="w-screen h-max flex flex-column flex-grow-1 align-items-center justify-content-center"
  >
    <div
      class="w-screen h-screen bg-cover bg-center flex flex-column"
      style="
        background-image: url('https://www.riotgames.com/darkroom/2000/6a2fc4addd2ad4793a915202395c8410:a762ac65c1b95d37880e5e267029cc38/son-riot-website.png');
      "
    >
      <div
        class="h-screen w-screen flex flex-column align-items-center justify-content-center"
        style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))"
      >
        <a class="text-8xl font-bold">Hello there</a>
        <a class="text-3xl font-bold">We are happy that you are visiting us!</a>
      </div>
    </div>
    <div class="flex flex-column py-8">
      <PlayersLeaderboard class="m-8 p-8" />
      <StoryChapter />
    </div>
  </main>
</template>

<script setup lang="ts">
import PlayersLeaderboard from '@/components/PlayersLeaderboard.vue'
import StoryChapter from '@/components/StoryChapter.vue'
</script>
