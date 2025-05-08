<script setup lang="ts">
onBeforeMount(init);
async function init() {
  useRouter().push(`/@/${authService.user.username}`);
}
</script>
