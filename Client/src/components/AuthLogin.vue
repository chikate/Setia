<template>
  <div
    class="h-full flex flex-column justify-content-center align-items-center"
  >
    <AuthForm
      v-model:input="input"
      :submitFunction="
        () =>
          new AuthApi()
            .apiAuthLoginPost({ authenticationDTO: input })
            .then((token) => {
              setCookie('access_token', token);
              $router.go(0);
            })
      "
    />
    <RouterLink class="no-underline" to="/"> Can't sign in? </RouterLink>
  </div>
</template>

<script setup lang="ts">
const input = ref({ username: "", password: "" });
</script>
