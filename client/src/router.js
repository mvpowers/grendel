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
      meta: { requiresAuth: false },
    },
    {
      path: '/vote',
      name: routes.VOTE,
      component: () => import('./views/Vote'),
      meta: { requiresAuth: true },
    },
    {
      path: '/wait',
      name: routes.WAIT,
      component: () => import('./views/Wait'),
      meta: { requiresAuth: true },
    },
    {
      path: '/result',
      name: routes.RESULT,
      component: () => import('./views/Result'),
      meta: { requiresAuth: true },
    },
    {
      path: '/reset',
      name: routes.RESET,
      component: () => import('./views/Reset'),
      meta: { requiresAuth: false },
    },
  ],
});

router.beforeEach((to, from, next) => {
  const { requiresAuth } = to.meta;
  const authToken = localStorage.getItem(localStorageKeys.AUTH_TOKEN);

  if (requiresAuth && !authToken) {
    next({ name: routes.HOME });
  } else {
    next();
  }
});

export default router;
