<script setup lang="ts">
import { Question, QuestionAnswer } from '@/interfaces'

const answerMode = defineModel('answerMode', { type: Boolean, default: false })
const customAnswere = defineModel('customAnswere', { type: String, default: '' })
const thisQuestionData = defineModel('questionData', {
  type: Object as PropType<Question>,
  required: true,
  default: {} as Question
})

const checksList = ref([])
const answered = ref<boolean>()

onBeforeMount(async () => await refresh())

async function refresh() {
  await useQuestionAnswersCRUDStore().get()
  answered.value = useQuestionAnswersCRUDStore().allLoadedItems?.find(
    (elem: QuestionAnswer) =>
      elem.author == useAuthStore().userData?.username &&
      elem.questionId == thisQuestionData.value.id
  )
}
</script>

<template>
  <div
    class="flex flex-column gap-2 surface-hover border-round p-2 shadow-1"
    :class="
      answerMode
        ? answered
          ? 'align-items-start border-1 border-blue-500'
          : 'align-items-start'
        : 'align-items-center'
    "
    style="min-width: 20rem"
  >
    <div class="text-xl" v-if="answerMode">
      {{ thisQuestionData.title }}
    </div>

    <InputText v-else :readonly="answerMode" v-model="thisQuestionData.title" />

    <InputText v-if="customAnswere" v-model="customAnswere" />
    <div
      v-else
      class="flex gap-2 align-items-center"
      v-for="(option, i) in thisQuestionData.options"
      :key="i"
    >
      <Checkbox
        v-if="!answered"
        v-model="checksList[i]"
        @update:modelValue="
          (thisQuestionData.selection = []),
            checksList.forEach((elem: Boolean, i) =>
              elem ? thisQuestionData.selection.push(thisQuestionData.options[i]) : ''
            )
        "
        binary
      />
      <div class="text-md" v-if="answerMode">
        {{ thisQuestionData.options[i] }}
      </div>
      <InputText v-else :readonly="answerMode" v-model="thisQuestionData.options[i]" />
      <i
        v-if="!answerMode"
        class="pi pi-minus hover:text-red-700"
        @click="thisQuestionData.options.splice(thisQuestionData.options.indexOf(option), 1)"
      />
    </div>

    <div
      v-if="!answered"
      class="flex flex-wrap justify-content-center align-items-center gap-2 w-full"
    >
      <Button
        v-if="!answerMode"
        icon="pi pi-plus"
        label="Add option"
        class="flex-grow-1 shadow-1"
        @click="thisQuestionData.options?.push('') ?? (thisQuestionData.options = [''])"
      />
      <Button
        v-if="answerMode && !customAnswere"
        icon="pi pi-pencil"
        label="Custom"
        class="flex-grow-1 shadow-1"
        @click="customAnswere = 'My answere'"
      />
      <div v-else-if="customAnswere">Remove everything to return to options</div>
      <Button
        v-if="answerMode"
        class="shadow-1"
        icon="pi pi-send"
        label="Answere"
        @click="
          useQuestionAnswersCRUDStore()
            .add([
              {
                QuestionId: thisQuestionData.id,
                Answer: customAnswere
                  ? [customAnswere]
                  : checksList
                      .map((value, index) => (value ? index.toString() : null))
                      .filter((index) => index != null)
              }
            ])
            .then(async () => await refresh())
        "
      />
      <SplitButton
        v-else
        label="Submit"
        class="flex-grow-1"
        @click="
          useQuestionsCRUDStore()
            .add()
            .then(async () => await refresh())
        "
        :model="[
          {
            label: 'Submit with custom option',
            command: () => useQuestionsCRUDStore().add()
          }
        ]"
      />
    </div>
  </div>
</template>
