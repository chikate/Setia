<template>
  <div>
    <div class="flex-wrap justify-content-around gap-2">
      <QuestionComponent
        v-model:answerMode="answerMode"
        v-model:checksList="checksList"
        v-model:question-data="useQuestionsCRUDStore().editItem"
      />
      <CRUDT
        class="w-full flex-grow-1"
        :store="useQuestionsCRUDStore()"
        @rowClick="(answerMode = true), (checksList = [])"
        @addClick="useQuestionsCRUDStore().resetToDefaults(), (answerMode = false)"
      />
      <CRUDT class="w-full flex-grow-1" :store="useQuestionAnswersCRUDStore()" readonly />
    </div>
  </div>
</template>

<script setup lang="ts">
const answerMode = ref(false)
const checksList = ref<boolean[]>([])
</script>
