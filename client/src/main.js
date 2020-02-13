import Vue from 'vue';
import Vuetify, { VSnackbar, VBtn, VIcon } from 'vuetify/lib';
import VuetifyToast from 'vuetify-toast-snackbar';
import Swal from 'sweetalert2';
import VueSweetalert2 from 'vue-sweetalert2';
import 'vuetify/src/stylus/app.styl';

import App from './App.vue';
import router from './router';
import store from './store';
import { SessionRequests } from './requests';
import { routes } from './constants';

Vue.config.productionTip = false;

Vue.use(Vuetify, {
  iconfont: 'md',
  components: {
    VSnackbar,
    VBtn,
    VIcon,
  },
});

Vue.use(VuetifyToast, {
  timeout: 3000,
  icon: 'info',
  dismissable: false,
  showClose: true,
  property: '$toast',
});

Vue.use(VueSweetalert2);

Vue.mixin({
  methods: {
    async routeVoteFlow() {
      try {
        const { data } = await SessionRequests.getUserSession();
        if (data.hasVotingExpired) return router.push({ name: routes.RESULT });
        if (data.hasActiveVote) return router.push({ name: routes.WAIT });
        return router.push({ name: routes.VOTE });
      } catch (e) {
        console.error(e);
        await Swal.fire('Session Error', 'Please login to continue', 'error');
        return router.push({ name: routes.HOME });
      }
    },
  },
});

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
