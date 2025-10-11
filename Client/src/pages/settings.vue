<template>
  <div
    class="flex flex-column w-full justify-content-around align-items-center"
  >
    <Button label="Toggle Dark Mode" @click="darkMode().toggleDarkMode()" />
    <ColorPicker @update:model-value="darkMode().applyThemeColor" />
    <!-- <CRUDT :service="usersCRUDService" class="custom-shadow-1" />
    <CRUDT :service="postsCRUDService" class="custom-shadow-1" /> -->
    <!-- <CRUDT :service="groupsCRUDService" class="custom-shadow-1" /> -->
    <!-- <Dropdown :options="['Admin', 'System', 'Server', 'Me']" />
    <InputText @keydown.enter="console.log" />
    <MultiSelect :options="[1, 2, 3]" /> -->
    <SelectButton v-model="value" :options="['Audit', 'Notifications']" />
    <LoginComponent />
    <RegisterComponent />
  </div>
</template>

<script setup>
const value = ref();
const audits = ref(["asd"]);

defineOptions({
  name: "Settings",
  icon: "⚙️",
});

auditConn.on("ReceiveAudit", (audit) => {
  console.log("New Audit Log:", audit);
  audits = audit;
});

auditConn.start().catch(console.error);
</script>
