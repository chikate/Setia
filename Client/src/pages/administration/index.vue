<template>
  <div class="flex-column p-2 gap-2">
    <Button label="Toggle Dark Mode" @click="darkMode().toggleDarkMode" />
    <CRUDT :service="usersCRUDService" class="custom-shadow-1" />
    <!-- <CRUDT :service="groupsCRUDService" class="custom-shadow-1" /> -->
  </div>
</template>
