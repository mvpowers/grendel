<template>
  <VApp>

    <VNavigationDrawer
      v-model="drawer"
      fixed
      app>
      <VList>

        <VListTile @click="submitQuestion">
          <VListTileAction>
            <VIcon>add_circle</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Question</VListTileTitle>
          </VListTileContent>
        </VListTile>

        <VDivider />

        <VListTile @click="signout">
          <VListTileAction>
            <VIcon>exit_to_app</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Signout</VListTileTitle>
          </VListTileContent>
        </VListTile>

      </VList>
    </VNavigationDrawer>

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
      <VToolbarSideIcon @click.stop="drawer = !drawer" />
    </VToolbar>

    <VContent>
      <RouterView />
    </VContent>
  </VApp>
</template>

<script>
import { routes } from './constants';

export default {
  name: 'App',
  data: () => ({
    ignoredToolbarRoutes: [routes.HOME, routes.RESET],
    drawer: null,
  }),
  methods: {
    signout() {
      localStorage.clear();
      this.$router.push({ name: routes.HOME });
    },
    submitQuestion() {
      console.log('submit question');
    },
  },
};
</script>
