<template>
  <main v-if="rights[0]">
    <div class="flex gap-8 h-screen py-8 align-items-start">
      <div
        class="flex flex-column gap-2 align-items-center align-self-center"
        style="width: 20rem"
        v-if="answerMode ? true : rights.includes(`${useQuestionsCRUDStore().$id}.Add`)"
      >
        <InputText :readonly="answerMode" v-model="useQuestionsCRUDStore().selectedItem.Title" />
        <div
          class="flex gap-2 align-items-center"
          v-for="(option, i) in useQuestionsCRUDStore().selectedItem.Options"
          :key="i"
        >
          <Checkbox v-model="checksList[i]" :binary="true" />
          <InputText
            :readonly="answerMode"
            v-model="useQuestionsCRUDStore().selectedItem.Options[i]"
          />
          <i
            v-if="!answerMode"
            class="pi pi-trash hover:text-red-700"
            @click="
              useQuestionsCRUDStore().selectedItem.Options.splice(
                useQuestionsCRUDStore().selectedItem.Options.indexOf(option),
                1
              )
            "
          />
        </div>
        <div class="flex gap-2">
          <Button
            v-if="!answerMode"
            icon="pi pi-plus"
            label="Add option"
            @click="
              useQuestionsCRUDStore().selectedItem.Options?.push('') ??
                (useQuestionsCRUDStore().selectedItem.Options = [''])
            "
          />
          <Button
            icon="pi pi-send"
            label="Submit"
            @click="
              answerMode
                ? useQuestionAnswersCRUDStore().add({
                    QuestionId: useQuestionsCRUDStore().selectedItem.Id,
                    Answer: checksList
                      .map((value, index) => (value ? index.toString() : null))
                      .filter((index) => index != null)
                  })
                : useQuestionsCRUDStore().add()
            "
          />
        </div>
      </div>
      <CRUDT
        class="h-full"
        :store="useQuestionsCRUDStore()"
        @rowClick="answerMode = true"
        @addClick="useQuestionsCRUDStore().resetToDefaults(), (answerMode = false)"
      />
      <CRUDT class="h-full" :store="useQuestionAnswersCRUDStore()" />
    </div>
  </main>
</template>

<script setup lang="ts">
import { useQuestionsCRUDStore } from '@/stores/cruds/QuestionsCRUDStore'
import { useQuestionAnswersCRUDStore } from '@/stores/cruds/QuestionAnswersCRUDStore'
import { ref } from 'vue'
import { useAuthStore } from '@/stores/AuthStore'
import { onBeforeMount } from 'vue'

const answerMode = ref(false)
const checksList = ref<boolean[]>([])
const rights = ref<string[]>([])

onBeforeMount(async () => {
  rights.value = await useAuthStore().checkUserTags([
    `${useQuestionsCRUDStore().$id}.Add`,
    `${useQuestionsCRUDStore().$id}.Edit`,
    `${useQuestionsCRUDStore().$id}.Get`,
    `${useQuestionsCRUDStore().$id}.Delete`
  ])
})
</script>
