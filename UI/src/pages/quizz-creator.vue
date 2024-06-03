<template>
  <main v-if="useAuthStore().checkUserRights">
    <div class="flex gap-8 h-screen py-8 align-items-center">
      <QuestionComponent v-model:answerMode="answerMode" v-model:checksList="checksList" />
      <CRUDT
        :store="useQuestionsCRUDStore()"
        @rowClick="(answerMode = true), (checksList = [])"
        @addClick="useQuestionsCRUDStore().resetToDefaults(), (answerMode = false)"
      />
      <CRUDT :store="useQuestionAnswersCRUDStore()" readonly />
    </div>
  </main>
</template>

<script setup lang="ts">
const answerMode = ref(false)
const checksList = ref<boolean[]>([])
</script>
