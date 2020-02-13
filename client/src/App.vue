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
        <!-- SUBMIT QUESTION -->
        <VListTile @click="submitQuestion">
          <VListTileAction>
            <VIcon>add_circle</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Question</VListTileTitle>
          </VListTileContent>
        </VListTile>
        <!-- SUBMIT FEATURE REQUEST -->
        <VListTile @click="submitFeatureRequest">
          <VListTileAction>
            <VIcon>add_to_queue</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Feature Request</VListTileTitle>
          </VListTileContent>
        </VListTile>
        <!-- SUBMIT BUG -->
        <VListTile @click="submitBug">
          <VListTileAction>
            <VIcon>bug_report</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Submit a Bug</VListTileTitle>
          </VListTileContent>
        </VListTile>
        <VDivider />
        <!-- ADMIN CENTER -->
        <VListTile
          v-if="showAdminCenter"
          @click="adminCenter">
          <VListTileAction>
            <VIcon>vpn_lock</VIcon>
          </VListTileAction>
          <VListTileContent>
            <VListTileTitle>Admin Center</VListTileTitle>
          </VListTileContent>
        </VListTile>
        <!-- SIGNOUT -->
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
    drawer: false,
  }),
  computed: {
    showAdminCenter() {
      return this.$store.state.userIsAdmin;
    },
  },
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
      const { userName } = this.$store.state;

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
      const { userName } = this.$store.state;

      if (userName) {
        errorTitle.push(userName);
      }

      this.$swal({
        icon: 'info',
        title: errorTitle.join(', '),
        text: 'Your idea has been noted',
      });
    },
    async adminCenter() {
      this.drawer = false;
      this.$router.push({ name: routes.ADMIN_CENTER });
    },
  },
};
</script>

<style>
  .swal2-modal, .v-snack {
    font-family: 'Roboto', sans-serif !important;
  }
</style>
