<template>
  <main class="flex flex-row p-8 gap-8">
    <!-- @vue:beforeMount="async () => await useTagsCRUDStore().get()" -->
    <Menu
      class="fixed top-0 left-0 m-8"
      :model="[
        {
          label: 'Pontaj',
          icon: 'pi pi-plus',
          command: () => scrollTo('Pontaj')
        },
        {
          label: 'Users',
          icon: 'pi pi-plus',
          command: () => scrollTo('Users')
        },
        {
          label: 'Tags',
          icon: 'pi pi-plus',
          command: () => scrollTo('Tags')
        },
        {
          label: 'Organizational Chart',
          icon: 'pi pi-plus',
          command: () => scrollTo('OrganizationalChart')
        }
        // useTagsCRUDStore()
        //   .allLoadedItems?.filter((elem: Taging) => elem.tag?.includes('Controller'))
        //   .reduce((uniqueTags: any[], elem: Taging) => {
        //     const label = elem.tag.replaceAll('Controller.', '').split('.')[0]
        //     if (!uniqueTags.find((item) => item.label === label)) {
        //       uniqueTags.push({
        //         label: label,
        //         icon: 'pi pi-plus',
        //         command: () => $router.push('/')
        //       })
        //     }
        //     return uniqueTags
        //   }, [])
      ]"
    />
    <div
      class="flex flex-column gap-8 justify-content-start align-items-start overflow-auto flex-grow-1"
      style="margin-left: 300px"
    >
      <CRUDT :store="usePontajCRUDStore()" />
      <CRUDT :store="useUsersCRUDStore()" />
      <CRUDT :store="useTagsCRUDStore()" />
      <!-- <CRUDT :store="useTagsCRUDStore()" /> -->
    </div>
  </main>
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
