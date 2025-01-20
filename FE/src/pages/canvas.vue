<template>
  <div>
    <Button class="absolute m-2" label="find session" />
    <AvatarGroup class="absolute right-0 m-2">
      <Avatar
        v-for="(sessionConnection, sessionConnection_index) in sessionsConnections"
        :key="sessionConnection_index"
        :image="sessionConnection.avatar"
        shape="circle"
        class="border-2"
        :style="{ 'border-color': stringToColor(sessionConnection.avatar) }"
      />
    </AvatarGroup>
    <div
      v-for="(cursorPosition, cursorPosition_index) in cursorsPositions"
      :key="cursorPosition_index"
      class="absolute pointer-events-none"
      style="z-index: 2147483647"
      :style="{
        top: cursorPosition.y + 'px',
        left: cursorPosition.x + 'px'
      }"
    >
      <div
        style="width: 20px; height: 20px; z-index: 2147483647; border-radius: 0 5px 5px 5px"
        class="absolute"
        :style="{
          'background-color': stringToColor(sessionsConnections[cursorPosition_index].avatar, 1)
        }"
      />
      <Avatar
        :image="sessionsConnections[cursorPosition_index].avatar"
        shape="circle"
        class="absolute border-2"
        style="z-index: 2147483647"
        :style="{
          'border-color': stringToColor(sessionsConnections[cursorPosition_index].avatar, 1)
        }"
        v-tooltip="sessionsConnections[cursorPosition_index].name"
      />
    </div>
  </div>
</template>

<script lang="ts" setup>
const sessionsConnections = ref([
  {
    avatar: 'https://primefaces.org/cdn/primevue/images/avatar/asiyajavayant.png',
    name: 'Asiya Javayant'
  },
  {
    avatar: 'https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png',
    name: 'Amy Elsner'
  },
  {
    avatar: 'https://primefaces.org/cdn/primevue/images/avatar/xuxuefeng.png',
    name: 'Xuxue Feng'
  }
])
const cursorsPositions = ref([
  { x: 100, y: 100 },
  { x: 200, y: 200 },
  { x: 300, y: 300 }
])

async function startSSE() {
  try {
    const sse = await fetch('/api/sse/StreamSSE', {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
        ContentType: 'text/event-stream',
        CacheControl: 'no-cache',
        Connection: 'keep-alive'
      }
    })

    const reader = sse.body?.getReader()
    if (!reader) return console.error('Failed to get reader from response body')

    while (reader) {
      const { value, done } = await reader.read()
      if (done) break
      const chunk = new TextDecoder('utf-8').decode(value, { stream: true })
      const newPositions = JSON.parse(chunk)
      const alpha = 0.9
      cursorsPositions.value.forEach((element, i) => {
        if (newPositions[i]) {
          element.x += (newPositions[i].x - element.x) * alpha
          element.y += (newPositions[i].y - element.y) * alpha
        }
      })
    }
  } catch (error) {
    console.error('Error with SSE connection:', error)
  }
}

startSSE()
let sendCursorData = true
let lastMousePosition = { x: 0, y: 0 }
document.addEventListener('mousemove', async (event: MouseEvent) => {
  if (
    sendCursorData &&
    (lastMousePosition.x != event.clientX || lastMousePosition.y != event.clientY)
  ) {
    sendCursorData = false
    lastMousePosition = { x: event.clientX, y: event.clientY }
    setTimeout(async () => {
      await apiRequest(`Sessions/MousePosition`, lastMousePosition, 'post')
      sendCursorData = true
    }, 10)
  }
})
</script>
