<template>
  <main>
    <div
      class="bg-cover bg-center"
      style="
        background-image: url('https://www.riotgames.com/darkroom/2000/6a2fc4addd2ad4793a915202395c8410:a762ac65c1b95d37880e5e267029cc38/son-riot-website.png');
      "
    >
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-8xl font-bold">Hello there</a>
        <a class="text-3xl font-bold">We are happy that you are here!</a>
      </main>
    </div>
    <div class="flex flex-column py-8">
      <PlayersLeaderboard class="my-8 py-8 align-self-center" />
      <PontajTable class="my-8 py-8 align-self-center" />
      <UsersTable class="m-8 p-8 align-self-center" />
      <StoryChapter />
    </div>
  </main>
</template>

<script setup lang="ts">
import PlayersLeaderboard from '@/components/PlayersLeaderboard.vue'
import PontajTable from '@/CRUDs/PontajTable.vue'
import UsersTable from '@/CRUDs/UsersTable.vue'
import StoryChapter from '@/components/StoryChapter.vue'
</script>
