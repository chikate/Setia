<script setup lang="ts">
import EmojiPicker from 'vue3-emoji-picker'
import type { Post, User, UserCollection, Question } from '@/interfaces'
defineProps({
  // postData: { type: Object as PropType<Post>, required: false },
  chat: { type: Boolean, required: false }
})

const thisPostData = defineModel('postData', { type: Object as PropType<Post>, required: false })
console.log(thisPostData.value)

const showComments = ref<boolean>(false)
const showShareList = ref<boolean>(false)
const showEmojiPicker = ref<boolean>(false)
const onSelectEmoji = ref<string>()
const authorData = ref<User>({} as User)
const localMessage = ref(thisPostData.value?.message)
// const localQuestions = ref<Question[]>([props.postData?.questionData] as Question[])
const localQuestion = ref<Question>()
const menuRef = ref()
const inEdit = ref<boolean>(false)
const route = useRoute()

const icons = ref([
  { name: 'pi-thumbs-up' },
  { name: 'pi-thumbs-down' },
  { name: 'pi-comments' },
  { name: 'pi-send' },
  { name: route.name == '/collection' ? 'pi-star-fill' : 'pi-star' }
])

onBeforeMount(async () => {
  if (thisPostData.value?.author)
    authorData.value = await useHelperStore().getUserProfile(String(thisPostData.value?.author))
})

function hashCode(str: string) {
  var hash = 0
  for (var i = 0; i < str.length; i++) {
    hash = str.charCodeAt(i) + ((hash << 5) - hash)
  }
  return hash
}

function intToRGBA(i: number, alpha: number) {
  var r = (i >> 16) & 0xff
  var g = (i >> 8) & 0xff
  var b = i & 0xff
  var a = Math.round(alpha * 255) & 0xff // Ensure alpha is in the range [0, 255]
  return ((r << 24) | (g << 16) | (b << 8) | a).toString(16).toUpperCase().padStart(8, '0')
}

async function post(tag?: string) {
  let questionsData = undefined
  if (localQuestion.value) questionsData = (await useQuestionsCRUDStore().add()) as Question[]
  await usePostsCRUDStore()?.add([
    {
      ...usePostsCRUDStore()?.editItem,
      message: localMessage.value,
      questionData: questionsData ? questionsData[0] : undefined,
      questionId: questionsData ? questionsData[0].id : undefined,
      // comment
      tags: thisPostData.value?.toPostId ? ['Comment'] : [tag],
      toPostId: thisPostData.value?.toPostId
    }
  ] as Post[])
}

async function addLocalQuestion() {
  localQuestion.value = new Object() as Question
}
</script>

<template>
  <div
    :id="$options.__name"
    style="min-width: 45rem"
    class="border-round flex flex-column m-1 align-items-start"
    :class="thisPostData?.message || chat ? 'shadow-1 p-4 bg-primary-reverse' : 'gap-4'"
  >
    <div v-if="thisPostData?.message" class="flex flex-wrap gap-4 align-items-center w-full">
      <Avatar
        class="shadow-1"
        v-if="!chat"
        :image="authorData.avatar"
        size="large"
        shape="circle"
      />
      <div class="flex-grow-1 flex flex-column justify-content-center">
        <div class="text-lg opacity-90">{{ authorData.username }}</div>
        <div class="text-xs opacity-40">
          {{ new Date(String(thisPostData?.executionDate)).toUTCString().replaceAll(' GMT', '') }}
        </div>
      </div>

      <!-- Tags -->
      <div v-if="thisPostData?.message" class="flex flex-wrap justify-content-end gap-1">
        <div
          v-for="(tag, i) in thisPostData?.tags"
          :key="i"
          class="px-2 border-round shadow-1"
          :style="`background-color: #${intToRGBA(hashCode(tag), 0.5)}; color: white`"
        >
          {{ tag }}
        </div>
      </div>

      <Button
        v-if="useAuthStore().userData?.username === authorData.username"
        type="button"
        icon="pi pi-ellipsis-v"
        @click="menuRef.toggle($event)"
        aria-haspopup="true"
        aria-controls="overlay_menu"
        class="shadow-1"
      />
      <Menu
        ref="menuRef"
        popup
        :model="[
          {
            label: 'Options',
            items: [
              {
                label: 'Delete',
                icon: 'pi pi-trash',
                command: () => {
                  usePostsCRUDStore().delete([thisPostData])
                }
              },
              {
                label: 'Edit',
                icon: 'pi pi-pencil',
                command: () => {
                  ;(inEdit = true), (localMessage = thisPostData?.message)
                }
              },
              {
                label: 'Export',
                icon: 'pi pi-upload',
                command: () => {}
              },
              {
                label:
                  thisPostData?.tags?.indexOf('Private') > -1
                    ? 'Make it Public'
                    : 'Make it Private',
                icon: thisPostData?.tags?.indexOf('Private') > -1 ? 'pi pi-eye' : 'pi pi-eye-slash',
                command: () => {}
              }
            ]
          }
        ]"
      />
    </div>

    <div
      v-if="thisPostData?.message"
      v-html="thisPostData?.message"
      style="max-width: 45rem"
      class=""
    />
    <Editor
      v-else
      v-model="localMessage"
      class="w-full shadow-1 border-round bg-cover bg-center"
      style="max-width: 45rem"
    />
    <QuestionComponent answerMode v-if="thisPostData?.questionId" />
    <QuestionComponent v-if="localQuestion" :question-data="localQuestion" />
    <div class="flex flex-column align-items-start gap-4">
      <Button
        v-if="!thisPostData?.message"
        class="shadow-1"
        :icon="localQuestion ? 'pi pi-minus' : 'pi pi-plus'"
        :label="localQuestion ? 'Remove Question' : 'Add Question'"
        @click="localQuestion ? (localQuestion = undefined) : addLocalQuestion()"
      />

      <SplitButton
        v-if="!thisPostData?.message"
        class="shadow-1"
        :disabled="!localMessage"
        label="Post"
        @click="post('Post')"
        :model="[
          {
            label: 'News',
            command: () => {
              if (usePostsCRUDStore()?.editItem?.tags) {
                usePostsCRUDStore()?.editItem?.tags?.push('News')
              } else {
                usePostsCRUDStore().editItem.tags = ['News']
              }
              post('News')
            }
          },
          {
            label: 'Add Question',
            command: () => addLocalQuestion()
          }
        ]"
      />
    </div>

    <div class="flex flex-column gap-4 w-full">
      <!-- Reaction buttons -->
      <div v-if="!chat && thisPostData?.message" class="flex flex-wrap justify-content-around">
        <CountButon
          v-for="(icon, index) in icons"
          :key="index"
          :icon="icon.name"
          :count="
            icon.name.includes('pi-thumbs-up')
              ? usePostsCRUDStore().allLoadedItems?.filter(
                  (post: Post) => (post.tags?.indexOf('Positive') ?? -1) > -1
                ).length
              : icon.name.includes('pi-comments')
                ? usePostsCRUDStore().allLoadedItems?.filter(
                    (post: Post) => (post.tags?.indexOf('Comment') ?? -1) > -1
                  ).length
                : undefined
          "
          @click="
            icon.name == 'pi-thumbs-up'
              ? ((showEmojiPicker = true),
                (icon.name = 'pi-thumbs-up-fill'),
                usePostsCRUDStore().add([
                  {
                    toPostId: thisPostData?.id,
                    message: onSelectEmoji,
                    tags: ['Reaction', 'Emoji', 'Positive']
                  }
                ] as Post[]))
              : icon.name == 'pi-thumbs-up-fill'
                ? ((showEmojiPicker = true), (icon.name = 'pi-thumbs-up'))
                : icon.name == 'pi-thumbs-down'
                  ? ((showEmojiPicker = true),
                    (icon.name = 'pi-thumbs-down-fill'),
                    usePostsCRUDStore().add([
                      {
                        toPostId: thisPostData?.id,
                        message: onSelectEmoji,
                        tags: ['Reaction', 'Emoji', 'Negative']
                      }
                    ] as Post[]))
                  : icon.name == 'pi-thumbs-down-fill'
                    ? ((showEmojiPicker = true), (icon.name = 'pi-thumbs-down'))
                    : icon.name == 'pi-comments'
                      ? (showComments = !showComments)
                      : icon.name == 'pi-send'
                        ? (showShareList = !showShareList)
                        : icon.name == 'pi-star'
                          ? (useUserCollectionCRUDStore().add([
                              { postId: thisPostData.id }
                            ] as UserCollection[]),
                            (icon.name = 'pi-star-fill'))
                          : icon.name == 'pi-star-fill'
                            ? (useUserCollectionCRUDStore().delete([
                                { id: thisPostData.id, postId: thisPostData.id }
                              ] as UserCollection[]),
                              (icon.name = 'pi-star'))
                            : undefined
          "
        />
      </div>

      <EmojiPicker
        v-if="showEmojiPicker"
        native
        class="w-full"
        @select="(onSelectEmoji = $event.i), (showEmojiPicker = false)"
      />

      <!-- Comments -->
      <div v-if="showComments" class="h-8 overflowY-auto">
        <PostComponent :postData="{ toPostId: thisPostData?.id } as Post" />
        <PostComponent
          v-for="(postData, i) in usePostsCRUDStore().allLoadedItems?.filter(
            (post: Post) => post.toPostId == thisPostData?.id
          )"
          :key="i"
          :postData
          class="pt-4"
        />
      </div>

      <!-- Share -->
      <div v-if="showShareList" class="flex flex-wrap gap-4 justify-content-center">
        <UserProfileComponent
          v-for="(username, i) in ['testUser', 'Dragos']"
          :key="i"
          :username
          class="flex-grow-1"
          style="max-width: 10rem"
        />
      </div>
    </div>
  </div>
</template>
