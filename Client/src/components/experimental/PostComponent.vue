<template>
  <div
    :id="$options.__name"
    style="min-width: 45rem"
    class="border-round flex flex-column align-items-start"
    :class="
      thisPostData.message || chat ? 'shadow-1 p-4 bg-primary-reverse' : 'gap-4'
    "
  >
    <div
      v-if="thisPostData.message"
      class="flex flex-wrap gap-4 align-items-center w-full"
    >
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
          {{
            new Date(String(thisPostData.executionDate))
              .toUTCString()
              .replaceAll(" GMT", "")
          }}
        </div>
      </div>

      <!-- Tags -->
      <div
        v-if="thisPostData.message"
        class="flex flex-wrap justify-content-end gap-1"
      >
        <div
          v-for="(tag, i) in thisPostData.tags"
          :key="i"
          class="px-2 border-round shadow-1 font-semibold text-white"
          :style="`background-color: ${stringToColor(tag)}`"
        >
          {{ tag }}
        </div>
      </div>

      <Button
        v-if="authService.user?.username === authorData.username"
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
                command: () => postsCRUDService.delete([thisPostData]),
              },
              {
                label: 'Edit',
                icon: 'pi pi-pencil',
                command: () => (
                  (inEdit = true), (localMessage = thisPostData.message)
                ),
              },
              {
                label: 'Export',
                icon: 'pi pi-upload',
                command: () => {},
              },
              {
                label:
                  thisPostData.tags.indexOf('Private') > -1
                    ? 'Make it Public'
                    : 'Make it Private',
                icon:
                  thisPostData.tags.indexOf('Private') > -1
                    ? 'pi pi-eye'
                    : 'pi pi-eye-slash',
                command: () => {},
              },
            ],
          },
        ]"
      />
    </div>

    <div
      v-if="thisPostData.message"
      v-html="thisPostData.message"
      style="max-width: 45rem"
    />
    <Editor
      v-else
      v-model="localMessage"
      class="w-full card border-round bg-cover bg-center"
      style="max-width: 45rem"
    />

    <QuestionComponent
      answerMode
      v-if="thisPostData.questionId && thisPostData.questionData"
      v-model:question-data="thisPostData.questionData"
      class="mb-3"
    />

    <QuestionComponent
      v-if="thisPostData.questionData && !thisPostData.message"
      v-model:question-data="thisPostData.questionData"
    />

    <div class="flex flex-column align-items-start gap-4">
      <Button
        v-if="!thisPostData.message"
        class="shadow-1"
        :icon="thisPostData.questionData ? 'pi pi-minus' : 'pi pi-plus'"
        :label="thisPostData.questionData ? 'Remove Question' : 'Add Question'"
        @click="
          thisPostData.questionData
            ? (thisPostData.questionData = undefined)
            : addLocalQuestion()
        "
      />

      <SplitButton
        v-if="!thisPostData.message"
        class="shadow-1"
        :disabled="!localMessage"
        label="Post"
        @click="post(['Post'])"
        :model="[
          {
            label: 'News',
            command: () => {
              if (postsCRUDService.editItem.tags) {
                postsCRUDService.editItem.tags.push('News');
              } else {
                postsCRUDService.editItem.tags = ['News'];
              }
              post(['Post', 'News']);
            },
          },
          {
            label: 'Add Question',
            command: () => addLocalQuestion(),
          },
        ]"
      />
    </div>

    <div class="flex flex-column gap-4 w-full">
      <!-- Action buttons -->
      <div
        v-if="!chat && thisPostData.message"
        class="flex flex-wrap justify-content-around"
      >
        <CountButon
          v-for="(icon, index) in icons"
          :key="index"
          :icon="icon.name"
          :count="
            icon.name.includes('pi-thumbs-up')
              ? postsCRUDService.allLoadedItems?.filter(
                  (post: Post) =>
                    (post.tags.indexOf('Positive') ?? -1) > -1 && post.entityId == thisPostData.id
                ).length
              : icon.name.includes('pi-thumbs-down')
                ? postsCRUDService.allLoadedItems?.filter(
                    (post: Post) =>
                      (post.tags.indexOf('Negative') ?? -1) > -1 && post.entityId == thisPostData.id
                  ).length
                : icon.name.includes('pi-comments')
                  ? postsCRUDService.allLoadedItems?.filter(
                      (post: Post) =>
                        (post.tags.indexOf('Comment') ?? -1) > -1 &&
                        post.entityId == thisPostData.id
                    ).length
                  : undefined
          "
          @click="
            // Like
            icon.name == 'pi-thumbs-up'
              ? (showActions = index)
              : icon.name == 'pi-thumbs-up-fill'
              ? postsCRUDService
                  .delete(
                    postsCRUDService.allLoadedItems?.filter(
                      (post: Post) =>
                        (post.tags.indexOf('Positive') ?? -1) > -1 &&
                        post.entityId == thisPostData.id &&
                        post.author == authService.user?.username
                    )
                  )
                  .then(() => (icon.name = 'pi-thumbs-up'))
              : // Dislike
              icon.name == 'pi-thumbs-down'
              ? (showActions = index)
              : icon.name == 'pi-thumbs-down-fill'
              ? postsCRUDService
                  .delete(
                    postsCRUDService.loadedItems?.filter(
                      (post: Post) =>
                        (post.tags.indexOf('Negative') ?? -1) > -1 &&
                        post.entityId == thisPostData.id &&
                        post.author == authService.user?.username
                    )
                  )
                  .then(() => (icon.name = 'pi-thumbs-down'))
              : // Comments
              icon.name == 'pi-comments'
              ? (showActions = showActions == index ? undefined : index)
              : // Share
              icon.name == 'pi-send'
              ? (showActions = showActions == index ? undefined : index)
              : // Star
              icon.name == 'pi-star'
              ? userCollectionCRUDService
                  .addItems([{ postId: thisPostData.id }] as UserCollection[])
                  .then(() => (icon.name = 'pi-star-fill'))
              : icon.name == 'pi-star-fill'
              ? userCollectionCRUDService
                  .delete(
                    userCollectionCRUDService.loadedItems?.filter(
                      (elem: UserCollection) => elem.postId == thisPostData.id
                    )
                  )
                  .then(() => (icon.name = 'pi-star'))
              : // end
                undefined
          "
        />
      </div>

      <!-- Like Dislike EmojiPicker -->
      <EmojiPicker
        v-if="showActions == 0 || showActions == 1"
        native
        class="w-full"
        @select="
          postsCRUDService
            .addItems([
              {
                entityId: thisPostData.id,
                message: $event.i,
                tags: [
                  'Reaction',
                  'Emoji',
                  showActions == 0 ? 'Positive' : 'Negative',
                ],
              },
            ] as Post[])
            .then(
              () => (
                (icons[Number(showActions)].name =
                  showActions == 0
                    ? 'pi-thumbs-up-fill'
                    : 'pi-thumbs-down-fill'),
                (showActions = undefined)
              )
            )
        "
      />

      <!-- Comments -->
      <div v-if="showActions == 2" class="h-8">
        <PostComponent :postData="{ entityId: thisPostData.id }" />
        <PostComponent
          v-for="(postData, i) in postsCRUDService.loadedItems?.filter(
            (post: Post) => post.entityId == thisPostData.id
          )"
          :key="i"
          :post-data="postData"
          class="pt-4"
        />
      </div>

      <!-- Share -->
      <div
        v-if="showActions == 3"
        class="flex flex-wrap gap-4 justify-content-center"
      >
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

<script setup lang="ts">
import EmojiPicker from "vue3-emoji-picker";
import type { Post, User, UserCollection, Question } from "@/global/interfaces";

defineProps({
  // postData: { type: Object as PropType<Post>, required: false },
  chat: { type: Boolean, required: false },
});

const thisPostData = defineModel("postData", {
  type: Object as PropType<Post>,
  required: true,
  default: {} as Post,
});

const showActions = ref<number>();
const authorData = ref<User>({} as User);
const localMessage = ref(thisPostData.value.message);
const menuRef = ref();
const user = ref(JSON.parse(localStorage.getItem("user") ?? "null")?.username);
const inEdit = ref<boolean>(false);

const icons = ref([
  {
    name: postsCRUDService.loadedItems?.find(
      (post: Post) =>
        (post.tags.indexOf("Positive") ?? -1) > -1 &&
        post.entityId == thisPostData.value.id &&
        post.author == user.value
    )
      ? "pi-thumbs-up-fill"
      : "pi-thumbs-up",
  },
  {
    name: postsCRUDService.loadedItems?.find(
      (post: Post) =>
        (post.tags.indexOf("Negative") ?? -1) > -1 &&
        post.entityId == thisPostData.value.id &&
        post.author == user.value
    )
      ? "pi-thumbs-down-fill"
      : "pi-thumbs-down",
  },
  { name: "pi-comments" },
  // { name: 'pi-send' },
  {
    name: userCollectionCRUDService.loadedItems?.find(
      (elem: UserCollection) => elem.postId == thisPostData.value.id
    )
      ? "pi-star-fill"
      : "pi-star",
  },
]);

onBeforeMount(async () => {
  if (thisPostData.value.author)
    authorData.value = await helperService.getUserProfile(
      String(thisPostData.value.author)
    );

  await postsCRUDService.loadItems();

  icons.value = [
    {
      name: postsCRUDService.loadedItems?.find(
        (post: Post) =>
          (post.tags.indexOf("Positive") ?? -1) > -1 &&
          post.entityId == thisPostData.value.id &&
          post.author == authService.user?.username
      )
        ? "pi-thumbs-up-fill"
        : "pi-thumbs-up",
    },
    {
      name: postsCRUDService.loadedItems?.find(
        (post: Post) =>
          (post.tags.indexOf("Negative") ?? -1) > -1 &&
          post.entityId == thisPostData.value.id &&
          post.author == authService.user?.username
      )
        ? "pi-thumbs-down-fill"
        : "pi-thumbs-down",
    },
    { name: "pi-comments" },
    // { name: 'pi-send' },
    {
      name: userCollectionCRUDService.loadedItems?.find(
        (elem: UserCollection) => elem.postId == thisPostData.value.id
      )
        ? "pi-star-fill"
        : "pi-star",
    },
  ];
});

function stringToColor(str: string): string {
  // Create a hash from the string
  let hash = 0;
  for (let i = 0; i < str.length; i++) {
    hash = str.charCodeAt(i) + ((hash << 5) - hash);
  }

  // Convert the hash to an RGB color
  const r = (hash >> 16) & 0xff;
  const g = (hash >> 8) & 0xff;
  const b = hash & 0xff;

  const alpha = 0.6;

  // Return the color in RGBA format
  return `rgba(${r}, ${g}, ${b}, ${alpha})`;
}

async function post(tags?: string[]) {
  let questionsData: Question | undefined = undefined;
  if (thisPostData.value.questionData)
    questionsData = (
      await questionsCRUDService.addItems([
        thisPostData.value.questionData,
      ] as Question[])
    )[0];
  await postsCRUDService.addItems([
    {
      ...usePostsCRUDStore().editItem,
      message: localMessage.value,
      questionId: questionsData?.id,
      // comment
      tags: thisPostData.value.entityId ? ["Comment"] : tags,
      entityId: thisPostData.value.entityId,
    },
  ] as Post[]);
}

async function addLocalQuestion() {
  thisPostData.value.questionData = new Object() as Question;
}
</script>
