import Vue from 'vue';
import Vuetify, { VSnackbar, VBtn, VIcon } from 'vuetify/lib';
import VuetifyToast from 'vuetify-toast-snackbar';
import VueSweetalert2 from 'vue-sweetalert2';
import 'vuetify/src/stylus/app.styl';
import App from './App.vue';
import router from './router';

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

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
