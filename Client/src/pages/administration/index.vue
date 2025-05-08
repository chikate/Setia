<template>
  <div class="flex-column p-2 gap-2">
    <Button label="Toggle Dark Mode" @click="darkMode().toggleDarkMode" />
    <CRUDT :service="usersCRUDService" class="custom-shadow-1" />
    <CRUDT :service="postsCRUDService" class="custom-shadow-1" />
    <!-- <CRUDT :service="groupsCRUDService" class="custom-shadow-1" /> -->
    <!-- <Dropdown :options="['Admin', 'System', 'Server', 'Me']" />
    <InputText @keydown.enter="console.log" />
    <MultiSelect :options="[1, 2, 3]" /> -->
    <SelectButton v-model="value" :options="['Audit', 'Notifications']" />
    <div id="auditLog" class="flex-column">
      <div v-for="audit in audits">{{ audit }}</div>
    </div>
  </div>
</template>

<script setup>
const value = ref();
const audits = ref(["asd"]);
</script>
