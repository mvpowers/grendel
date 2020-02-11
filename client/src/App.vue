<template>
  <VApp>
    <VNavigationDrawer
      v-model="drawer"
      fixed
      clipped
      disable-resize-watcher
      disable-route-watcher
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
        <VListTile @click="submitFeatureRequest">
          <VListTileAction>
            <VIcon>add_to_queue</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Feature Request</VListTileTitle>
          </VListTileContent>
        </VListTile>
        <VListTile @click="submitBug">
          <VListTileAction>
            <VIcon>bug_report</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Bug</VListTileTitle>
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
<!--        TODO: move this out-->
        <VListTile @click="startSession">
          <VListTileAction>
            <VIcon>exit_to_app</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>START SESSION</VListTileTitle>
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
import { localStorageKeys, routes } from './constants';
import { SessionRequests } from './requests';

export default {
  name: 'App',
  data: () => ({
    ignoredToolbarRoutes: [routes.HOME, routes.RESET],
    drawer: false,
  }),
  methods: {
    signout() {
      localStorage.clear();
      this.drawer = false;
      this.$toast.info('Goodbye');
      this.$router.push({ name: routes.HOME });
    },
    submitQuestion() {
      this.drawer = false;
      this.$router.push({ name: routes.SUBMIT_QUESTION });
    },
    submitBug() {
      this.drawer = false;
      const errorTitle = ['Thank you'];
      const userName = localStorage.getItem(localStorageKeys.USER_NAME);

      if (userName) {
        errorTitle.push(userName);
      }

      this.$swal({
        icon: 'error',
        title: errorTitle.join(', '),
        text: 'Your discontent has been noted',
      });
    },
    submitFeatureRequest() {
      this.drawer = false;
      const errorTitle = ['Thank you'];
      const userName = localStorage.getItem(localStorageKeys.USER_NAME);

      if (userName) {
        errorTitle.push(userName);
      }

      this.$swal({
        icon: 'info',
        title: errorTitle.join(', '),
        text: 'Your idea has been noted',
      });
    },
    async startSession() {
      try {
        await SessionRequests.startSession();
        this.$toast.success('Session started');
        localStorage.clear();
        this.drawer = false;
        this.$router.push({ name: routes.HOME });
      } catch (e) {
        this.$toast.error('Session failed');
        console.error(e);
      }
    },
  },
};
</script>

<style>
  .swal2-modal, .v-snack {
    font-family: 'Roboto', sans-serif !important;
  }
</style>
