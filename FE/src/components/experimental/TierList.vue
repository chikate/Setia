<script setup lang="ts">
import * as htmlToImage from 'html-to-image'
import { download } from '@/helpers'

const tierListDimension = defineModel('tierListDimension', {
  type: Number,
  required: false,
  default: 100
})
const tierListUniformGap = defineModel('tierListUniformGap', {
  type: Number,
  required: false,
  default: 0
})
const tierListitems = defineModel('tierListitems', {
  type: Array<string>,
  required: false,
  default: [
    'https://letsenhance.io/static/8f5e523ee6b2479e26ecc91b9c25261e/1015f/MainAfter.jpg',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmDwycVzCsWaZx5AaCTeqz6e8qLbt8UaQz7g&s',
    'https://images.pexels.com/photos/2662116/pexels-photo-2662116.jpeg?cs=srgb&dl=pexels-jaime-reimer-1376930-2662116.jpg&fm=jpg',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6WcdjgQXfUoz7afVYi53uNYsamoIvoWZpDg&s',
    //'https://upload.wikimedia.org/wikipedia/commons/c/c9/Napa_Valley.jpg',
    'https://images.unsplash.com/photo-1446308386271-523272773b92?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
    'https://media.post.rvohealth.io/wp-content/uploads/2020/08/banana-pink-background-thumb-1-732x549.jpg'
  ]
})

const loadingImage = ref<boolean>(false)

document.addEventListener('dragend', dragEnd)

async function saveImage() {
  loadingImage.value = true
  await htmlToImage.toPng(document.getElementById('tierList') as HTMLElement).then(download)
  // $el.children[1]
  setTimeout(() => (loadingImage.value = false), 300)
}

async function dragStart(ev: any) {
  ev.dataTransfer.setData('text', ev.target.src)
  ev.dataTransfer?.setData('sourceId', ev.target.id)
  ev.target.classList.add('opacity-05')
}
async function dragOver(ev: any) {
  ev.stopPropagation()
  ev.preventDefault()
}
async function dragEnd(ev: any) {
  ev.target.classList.remove('opacity-05')
}
// Needs improvement !!!!
async function drop(ev: any) {
  ev.stopPropagation()
  ev.preventDefault()

  const imageLink = ev.dataTransfer?.getData('text')
  if (imageLink) {
    const element = document.createElement('img') as HTMLImageElement
    element.id = 'drag'
    element.className = 'cursor-grab'
    element.crossOrigin = 'anonymous'
    element.src = imageLink
    element.height = tierListDimension.value
    element.ondrop = drop

    // Insert the image in the correct location
    if (ev.target.id == 'drag') ev.target.insertBefore(element, ev.target.firstChild)
    else ev.target.appendChild(element)

    const sourceId = ev.dataTransfer?.getData('sourceId')
    if (sourceId) {
      const sourceElement = document.getElementById(sourceId)
      if (sourceElement) sourceElement.remove()
    }
  }
}

// async function extractTierListOrder(): Promise<string[][]> {
//   return Array.from(
//     document.getElementById('tierList').querySelectorAll('[id^="droptarget-"]') || []
//   ).map((tier) => Array.from(tier.querySelectorAll('img')).map((img) => img.src))
// }
</script>

<template>
  <div class="flex-column gap-2 w-full align-items-start">
    <div class="flex-row align-items-center w-full">
      <Button
        class="button-gradient-effect"
        style="min-height: 3rem"
        icon="pi pi-save"
        label="Save / See the result in console log"
        :loading="loadingImage"
        @click="saveImage"
      />
      <Slider :min="64" :max="256" v-model="tierListDimension" class="w-full mx-3" />
    </div>

    <!-- <div id="tierListWithContent" :class="`flex-column overflow-auto gap-${tierListUniformGap}`"> -->

    <div
      id="tierList"
      :class="`flex-column bg-primary w-full border-round gap-${tierListUniformGap} p-${tierListUniformGap}`"
    >
      <div
        :id="`tierRow-${tierRowIndex}`"
        :class="`tierRow flex-row gap-${tierListUniformGap}`"
        v-for="(tierRowData, tierRowIndex) in [
          {
            title: 'S',
            color: 'ff7f7e'
          },
          {
            title: 'A',
            color: 'ffbf7f'
          },
          {
            title: 'B',
            color: 'ffdf80'
          },
          {
            title: 'C',
            color: 'feff7f'
          },
          {
            title: 'D',
            color: 'beff7f'
          },
          {
            title: 'F',
            color: '7eff80'
          }
        ]"
        :key="tierRowIndex"
      >
        <div
          :id="`tier-${tierRowIndex}`"
          class="tier text-xl flex-column justify-content-center"
          :style="`min-height: ${tierListDimension}px; min-width: ${tierListDimension}px; max-width: ${tierListDimension}px; width: ${tierListDimension}px; background-color: #${tierRowData.color}`"
        >
          <Textarea auto-resize v-model="tierRowData.title" class="bg-transparent text-center" />
        </div>
        <div
          :id="`droptarget-${tierRowIndex}`"
          :class="`droptarget flex-wrap w-full gap-${tierListUniformGap}`"
          :ondragover="dragOver"
          :ondrop="drop"
        />
      </div>
    </div>

    <div
      :id="`droptarget-container`"
      :class="`flex-wrap w-full min-h-max gap-${tierListUniformGap}`"
      :style="`min-height: ${tierListDimension}px;`"
      :ondragover="dragOver"
      :ondrop="drop"
    >
      <img
        v-for="(tierItem, tierItemIndex) in tierListitems"
        :key="tierItemIndex"
        :ondragstart="dragStart"
        :src="tierItem"
        :height="tierListDimension"
        class="cursor-grab"
        crossorigin="anonymous"
      />
    </div>
    <!-- </div> -->
  </div>
</template>

<style scoped>
.p-inputtext {
  border-style: none;
  border-radius: 0;
  box-shadow: none;
  vertical-align: middle;
}
#tierList > *:first-child > .tier {
  border-radius: 5px 0 0;
}
#tierList > *:last-child > .tier {
  border-radius: 0 0 0 5px;
}
</style>
