<template>
  <main>
    <div class="flex gap-8 h-screen py-8 align-items-center">
      <QuestionComponent
        v-model:answerMode="answerMode"
        v-model:checksList="checksList"
        v-model:question-data="useQuestionsCRUDStore().editItem"
      />
      <CRUDT
        :store="useQuestionsCRUDStore()"
        @rowClick="(answerMode = true), (checksList = [])"
        @addClick="useQuestionsCRUDStore().resetToDefaults(), (answerMode = false)"
      />
      <CRUDT :store="useQuestionAnswersCRUDStore()" readonly />
    </div>
    <Chart
      @vue:before-mounted="useQuestionAnswersCRUDStore().get()"
      type="doughnut"
      :data="{
        labels: useQuestionsCRUDStore().editItem.options,
        datasets: [
          {
            data: datasetsData
          }
        ]
      }"
    />
  </main>
</template>

<script setup lang="ts">
const answerMode = ref(false)
const datasetsData = ref()
const checksList = ref<boolean[]>([])

watch(
  () => useQuestionsCRUDStore().editItem.id,
  async () =>
    (datasetsData.value = await useHelperStore().getQuestionAnswereDistribution(
      useQuestionsCRUDStore().editItem.id
    ))
)
</script>
