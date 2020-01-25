<template>
  <VApp>
    <VToolbar
      v-if="!ignoredToolbarRoutes.includes($router.currentRoute.name)"
      app>
      <VToolbarTitle class="headline text-uppercase">
        <span class="font-weight-light">
          Super
        </span>
        <span>Trivia</span>
        <span class="font-weight-light">
          Wednesday
        </span>
      </VToolbarTitle>
      <VSpacer />
      <VBtn
        flat
        href="https://github.com/vuetifyjs/vuetify/releases/latest"
        target="_blank">
        <VIcon>more_vert</VIcon>
      </VBtn>
    </VToolbar>

    <VContent>
      <RouterView />
    </VContent>
  </VApp>
</template>

<script>
export default {
  name: 'App',
  data: () => ({
    ignoredToolbarRoutes: ['home'],
  }),
};
</script>
