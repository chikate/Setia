<template>
  <div class="flex-column gap-1">
    <CRUDT :store="usePontajCRUDStore()" />
    <CRUDT :store="useUsersCRUDStore()" />
    <CRUDT :store="useTagsCRUDStore()" />
    <!-- <CRUDT :store="useTagsCRUDStore()" /> -->
  </div>
</template>

<script setup lang="ts">
function scrollTo(elem: string, offset: number = 70) {
  const element = document.getElementById(elem)
  if (element) {
    const elementPosition = element.getBoundingClientRect().top + window.pageYOffset
    const offsetPosition = elementPosition - offset
    window.scrollTo({
      top: offsetPosition,
      behavior: 'smooth'
    })
  }
}
</script>
