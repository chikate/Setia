<template>
  <div class="flex flex-row gap-3 p-3 h-full">
    <!-- <DataTable
      id="comunitiesList"
      :value="[{ name: 'asdasd' }]"
      :show-headers="false"
    >
      <Column field="avatar">
        <Avatar
          image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png"
        />
      </Column>
    </DataTable>
    <DataTable id="inbox" :value="[{ name: 'asdasd' }]" :show-headers="false">
      <Column field="avatar">
        <Avatar
          image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png"
        />
      </Column>
      <Column field="name" />
    </DataTable> -->

    <div
      id="directMessage"
      class="flex flex-column-reverse gap-2 w-full h-full overflow-auto"
    >
      <div class="flex flex-row gap-2">
        <Textarea rows="1" class="w-full" v-model="message" />
        <div class="flex flex-row gap-2">
          <Button
            icon="pi pi-send"
            label="SEND"
            @click="messages.push({ sender: 'me', message })"
          />
        </div>
      </div>
      <div id="messages" class="flex flex-column w-full gap-1 overflow-auto">
        <div id="message" v-for="message in messages" :key="message.id">
          <Avatar />
          <div>{{ message.author }}</div>
          <label>{{ message.message }}</label>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "Messanger",
  icon: "💬",
});

const messages = ref([]);
const message = ref();

onBeforeMount(init);
async function init() {}
</script>
