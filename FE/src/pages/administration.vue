<template>
  <div class="flex-column gap-1">
    <CRUDT :store="pontajCRUDStore" />
    <CRUDT :store="usersCRUDStore" />
  </div>
</template>

<script setup lang="ts">
onBeforeMount(init)
async function init() {
  console.log(await cryptoStore().getCoinCapCoin())
}
// function scrollTo(elem: string, offset: number = 70) {
//   const element = document.getElementById(elem)
//   if (element) {
//     const elementPosition = element.getBoundingClientRect().top + window.pageYOffset
//     const offsetPosition = elementPosition - offset
//     window.scrollTo({
//       top: offsetPosition,
//       behavior: 'smooth'
//     })
//   }
// }
</script>
