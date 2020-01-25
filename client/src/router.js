import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('./views/Home'),
    },
    {
      path: '/vote',
      name: 'vote',
      component: () => import('./views/Vote'),
    },
    {
      path: '/wait',
      name: 'wait',
      component: () => import('./views/Wait'),
    },
    {
      path: '/result',
      name: 'result',
      component: () => import('./views/Result'),
    },
  ],
});
