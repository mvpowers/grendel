import Vue from 'vue';
import Router from 'vue-router';
import Swal from 'sweetalert2';
import store from './store';
import {actions, localStorageKeys, routes} from './constants';

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
    {
      path: '/submit-question',
      name: routes.SUBMIT_QUESTION,
      component: () => import('./views/SubmitQuestion'),
      meta: { requiresAuth: true },
    },
    {
      path: '/admin',
      name: routes.ADMIN_CENTER,
      component: () => import('./views/Admin'),
      meta: { requiresAuth: true },
    },
  ],
});

router.beforeEach(async (to, from, next) => {
  const { requiresAuth } = to.meta;
  const authToken = localStorage.getItem(localStorageKeys.AUTH_TOKEN);

  if (requiresAuth && !authToken) {
    await Swal.fire('Unauthorized', 'Please login to continue', 'error');
    return next({ name: routes.HOME });
  }

  if (requiresAuth && !store.state.userDataLoaded) {
    await store.dispatch(actions.GET_USER_INFO, { token: authToken });
  }

  return next();
});

export default router;
