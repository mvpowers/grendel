import Vue from 'vue';
import 'sweetalert2/dist/sweetalert2.min.css';
import './plugins/vuetify';
import App from './App.vue';
import router from './router';

Vue.config.productionTip = false;

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
