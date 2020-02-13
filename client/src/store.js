/* eslint-disable no-param-reassign */
import Vue from 'vue';
import Vuex from 'vuex';
import { mutations } from './constants';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userName: '',
    userIsAdmin: false,
    userToken: '',
  },
  mutations: {
    [mutations.SET_USER_INFO](state, { token, name, isAdmin }) {
      state.userToken = token.toString();
      state.userName = name;
      state.userIsAdmin = isAdmin;
    },
  },
});
