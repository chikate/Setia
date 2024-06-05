<template>
  <div
    class="flex flex-column gap-2 surface-hover border-round p-2"
    :class="answerMode ? 'align-items-start' : 'align-items-center'"
    style="min-width: 20rem"
    v-if="answerMode ? true : useAuthStore().checkUserRights(`${useQuestionsCRUDStore().$id}.Add`)"
  >
    {{ answerMode ? 'Answer' : '' }}

    <div class="text-xl" v-if="answerMode">
      {{ useQuestionsCRUDStore().editItem.title }}
    </div>

    <InputText v-else :readonly="answerMode" v-model="useQuestionsCRUDStore().editItem.title" />

    <InputText v-if="customAnswere" v-model="customAnswere" />
    <div
      v-else
      class="flex gap-2 align-items-center"
      v-for="(option, i) in useQuestionsCRUDStore().editItem.options"
      :key="i"
    >
      <Checkbox
        v-model="checksList[i]"
        @update:modelValue="
          ;(useQuestionsCRUDStore().editItem.selection = []),
            checksList.forEach((elem: Boolean, i) =>
              elem
                ? useQuestionsCRUDStore().editItem.selection.push(
                    useQuestionsCRUDStore().editItem.options[i]
                  )
                : ''
            )
          console.log(useQuestionsCRUDStore().editItem.selection)
        "
        :binary="true"
      />
      <div class="text-md" v-if="answerMode">
        {{ useQuestionsCRUDStore().editItem.options[i] }}
      </div>
      <InputText
        v-else
        :readonly="answerMode"
        v-model="useQuestionsCRUDStore().editItem.options[i]"
      />
      <i
        v-if="!answerMode"
        class="pi pi-trash hover:text-red-700"
        @click="
          useQuestionsCRUDStore().editItem.options.splice(
            useQuestionsCRUDStore().editItem.options.indexOf(option),
            1
          )
        "
      />
    </div>

    <div class="flex flex-wrap justify-content-center gap-2 w-full">
      <Button
        v-if="!answerMode"
        icon="pi pi-plus"
        label="Add option"
        class="flex-grow-1"
        @click="
          useQuestionsCRUDStore().editItem.options?.push('') ??
            (useQuestionsCRUDStore().editItem.options = [''])
        "
      />
      <Button
        v-if="answerMode && !customAnswere"
        icon="pi pi-pencil"
        label="Custom"
        class="flex-grow-1"
        @click="customAnswere = 'Type here...'"
      />
      <div v-else-if="customAnswere">Remove everything to return to options</div>
      <Button
        v-if="
          answerMode
            ? () => useAuthStore().checkUserRights(`${useQuestionAnswersCRUDStore().$id}.Add`)
            : () => useAuthStore().checkUserRights(`${useQuestionsCRUDStore().$id}.Add`)
        "
        icon="pi pi-send"
        label="Submit"
        class="flex-grow-1"
        @click="
          answerMode
            ? useQuestionAnswersCRUDStore().add([
                {
                  Question: useQuestionsCRUDStore().editItem.id,
                  Answer: customAnswere
                    ? [customAnswere]
                    : checksList
                        .map((value, index) => (value ? index.toString() : null))
                        .filter((index) => index != null)
                }
              ])
            : useQuestionsCRUDStore().add()
        "
      />
    </div>
  </div>
</template>

<script setup lang="ts">
const answerMode = defineModel('answerMode', { type: Boolean, required: false, default: false })
const checksList = defineModel('checksList', { type: Array<Boolean>, required: false, default: [] })
const customAnswere = defineModel('customAnswere', { type: String, required: false, default: '' })
</script>