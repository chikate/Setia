<template>
  <div>
    <div class="w-full flex flex-row justify-content-between">
      <Button label="find session" />
      <AvatarGroup>
        <Avatar
          v-for="(
            sessionConnection, sessionConnection_index
          ) in sessionsConnections"
          :key="sessionConnection_index"
          :image="sessionConnection.avatar"
          shape="circle"
          class="border-2"
          :style="{ 'border-color': stringToColor(sessionConnection.avatar) }"
        />
      </AvatarGroup>
    </div>
    <div @mouseup="stopDragging" @mousemove="onDrag">
      <!-- line -->
      <svg class="absolute w-full h-full">
        <line
          v-if="items.length > 1"
          :x1="items[0].position.x - boxSize / 2"
          :y1="items[0].position.y + boxSize / 2"
          :x2="items[1].position.x - boxSize / 2"
          :y2="items[1].position.y + boxSize / 2"
          stroke="black"
          stroke-width="2"
        />
      </svg>
      <!-- forms -->
      <div
        v-for="item in items"
        :key="item.name"
        class="absolute border-1"
        :style="{
          left: item.position.x + 'px',
          top: item.position.y + 'px',
        }"
        @mousedown="startDragging(item, $event)"
      >
        <iframe
          src="https://www.youtube.com/embed/zrFWHAyI2W0"
          loading="lazy"
        />
        <iframe
          src="https://trello.com/b/GXLc34hk/epic-games-store-roadmap"
          loading="lazy"
        />
        <!-- https://www.google.com/maps/@45.9366075,25.0279113,7.5z?hl=en&entry=ttu&g_ep=EgoyMDI1MDEyMi4wIKXMDSoASAFQAw%3D%3D -->
        <iframe
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d445079.3495250492!2d24.155654338673064!3d45.9366075!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zNDUuOTM2NjA3NSwyNS4wMjc5MTEz!5e0!3m2!1sen!2sus!4v1674740745943!5m2!1sen!2sus"
          width="600"
          height="450"
          loading="lazy"
          referrerpolicy="no-referrer-when-downgrade"
        />
      </div>
    </div>
    <div
      v-for="(cursorPosition, cursorPosition_index) in cursorsPositions"
      :key="cursorPosition_index"
      :id="`cursor_${cursorPosition_index}`"
      class="fixed pointer-events-none"
      :style="{
        top: cursorPosition.y + 'px',
        left: cursorPosition.x + 'px',
      }"
    >
      <div
        style="width: 20px; height: 20px; border-radius: 0 5px 5px 5px"
        class="absolute"
        :style="{
          'background-color': stringToColor(
            sessionsConnections[cursorPosition_index].avatar,
            1
          ),
        }"
      />
      <Avatar
        :image="sessionsConnections[cursorPosition_index].avatar"
        shape="circle"
        class="absolute border-2"
        :style="{
          'border-color': stringToColor(
            sessionsConnections[cursorPosition_index].avatar,
            1
          ),
        }"
        v-tooltip="sessionsConnections[cursorPosition_index].name"
      />
    </div>
  </div>
</template>

<script lang="ts" setup>
defineOptions({
  name: "Canvas",
  icon: "⬜",
});

const sessionsConnections = ref([
  {
    avatar:
      "https://primefaces.org/cdn/primevue/images/avatar/asiyajavayant.png",
    name: "Asiya Javayant",
  },
  {
    avatar: "https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png",
    name: "Amy Elsner",
  },
  {
    avatar: "https://primefaces.org/cdn/primevue/images/avatar/xuxuefeng.png",
    name: "Xuxue Feng",
  },
]);
const cursorsPositions = ref([
  { x: 100, y: 100 },
  { x: 200, y: 200 },
  { x: 300, y: 300 },
]);

signalRConn.on("ReceiveMousePosition", (data) => {
  cursorsPositions.value[0] = data;
});

let sendCursorData = true;
let lastMousePosition = { x: 0, y: 0 };
document.addEventListener("mousemove", (event: MouseEvent) => {
  if (
    sendCursorData &&
    (lastMousePosition.x != event.clientX ||
      lastMousePosition.y != event.clientY)
  ) {
    sendCursorData = false;
    lastMousePosition = { x: event.clientX, y: event.clientY };
    setTimeout(() => {
      signalRConn
        .invoke("SendMousePosition", lastMousePosition.x, lastMousePosition.y)
        .catch((err) => console.error("Error sending mouse position:", err));
      sendCursorData = true;
    }, 2);
  }
});

const isDragging = ref(false);
const draggedItem = ref(null);
const items = ref([
  { name: "test1", position: { x: 100, y: 100 } },
  { name: "test2", position: { x: 200, y: 200 } },
]);
const dragStart = ref({ x: 0, y: 0 });
const boxSize = 50; // size of the box to center the line

const startDragging = (item: any, event: any) => {
  isDragging.value = true;
  draggedItem.value = item;
  dragStart.value = {
    x: event.clientX - item.position.x,
    y: event.clientY - item.position.y,
  };
};

const stopDragging = () => {
  isDragging.value = false;
  draggedItem.value = null;
};

const onDrag = (event: any) => {
  if (isDragging.value && draggedItem.value) {
    draggedItem.value.position.x = event.clientX - dragStart.value.x;
    draggedItem.value.position.y = event.clientY - dragStart.value.y;
  }
};
</script>
