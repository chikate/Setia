<script setup>
const router = useRouter()
const padding = defineModel('padding', { type: Number, required: false, default: 3 })
</script>

<template>
  <div class="flex-row align-items-center justify-content-center">
    <div class="bg-gray-500 h-full flex-grow-1 flex">
      <div class="flex-grow-1" />
      <div :class="`flex-row align-items-center justify-content-end p-${padding}`">
        <Menu
          :model="[
            {
              label: 'USER SETTINGS',
              icon: 'pi pi-file',
              items: [
                {
                  label: 'Home',
                  icon: 'pi pi-home',
                  command: async () => router.push(`/`)
                },
                {
                  label: 'Settings',
                  icon: 'pi pi-cog',
                  command: async () =>
                    await useHelperStore()
                      .getUserProfile(String(useAuthStore().userData?.username))
                      .then((result) =>
                        result ? router.push(`/profile/${result.username}`) : undefined
                      ),

                  items: [
                    {
                      label: 'Document',
                      icon: 'pi pi-file'
                    },
                    {
                      label: 'Image',
                      icon: 'pi pi-image'
                    },
                    {
                      label: 'Video',
                      icon: 'pi pi-video'
                    }
                  ]
                },
                {
                  label: 'Privacy & Safety',
                  icon: 'pi pi-folder-open'
                }
              ]
            },
            {
              label: 'Administration',
              icon: 'pi pi-file-edit',
              items: [
                {
                  label: 'Users',
                  command: () => router.push('/adm'),
                  icon: 'pi pi-copy'
                },
                {
                  label: 'Delete',
                  icon: 'pi pi-times'
                }
              ]
            },
            {
              label: 'Rights',
              icon: 'pi pi-search'
            },
            {
              separator: true
            },
            {
              label: 'Share',
              icon: 'pi pi-share-alt',
              items: [
                {
                  label: 'Slack',
                  icon: 'pi pi-slack'
                },
                {
                  label: 'Whatsapp',
                  icon: 'pi pi-whatsapp'
                }
              ]
            },
            {
              separator: true
            }
          ]"
        >
          <template #end>
            <Button
              v-ripple
              class="border-0 bg-primary-reverse cursor-pointer gap-2 w-full px-2 m-0"
              @click="
                async () =>
                  await useHelperStore()
                    .getUserProfile(String(useAuthStore().userData?.username))
                    .then((result) =>
                      result ? router.push(`/profile/${result.username}`) : undefined
                    )
              "
            >
              <Avatar
                image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png"
                shape="circle"
              />
              <div class="flex-column align-items-start">
                <span class="font-bold">Amy Elsner</span>
                <span class="text-sm">Admin</span>
              </div>
            </Button>
          </template>
        </Menu>
      </div>
    </div>
    <div :class="`p-${padding}`" style="width: 50rem">
      <router-view v-slot="{ Component }">
        <transition name="fade" mode="out-in">
          <component :is="Component" />
        </transition>
      </router-view>
    </div>
    <div class="flex-grow-1">
      <Button icon="pi pi-times" rounded :class="`absolute top-0 mt-${padding}`" />
    </div>
  </div>
</template>

<style>
.fade-leave-to,
.fade-enter-from {
  opacity: 0;
}
.fade-leave-from,
.fade-enter-to {
  opacity: 1;
}
.fade-enter-active,
.fade-leave-active {
  transition: all 0.1s ease;
}

.p-treeselect-label {
  white-space: wrap !important;
  flex-wrap: wrap !important;
  display: flex !important;
  row-gap: 0.25rem !important;
}
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
</style>
