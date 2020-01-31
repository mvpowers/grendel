import Vue from 'vue';
import Router from 'vue-router';
import { localStorageKeys, routes } from './constants';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: routes.HOME,
      component: () => import('./views/Home'),
    },
    {
      path: '/vote',
      name: routes.VOTE,
      component: () => import('./views/Vote'),
    },
    {
      path: '/wait',
      name: routes.WAIT,
      component: () => import('./views/Wait'),
    },
    {
      path: '/result',
      name: routes.RESULT,
      component: () => import('./views/Result'),
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (to.name !== routes.HOME && !localStorage.getItem(localStorageKeys.AUTH_TOKEN)) {
    next({ name: routes.HOME });
  } else {
    next();
  }
});

export default router;
