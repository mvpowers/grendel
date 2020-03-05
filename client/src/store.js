/* eslint-disable no-param-reassign */
import Vue from 'vue';
import Vuex from 'vuex';
import { mutations, actions } from './constants';
import { UserRequests } from './requests';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userDataLoaded: false,
    userName: '',
    userIsAdmin: false,
    userToken: '',
  },
  mutations: {
    [mutations.SET_USER_INFO](state, { token, name, isAdmin }) {
      state.userDataLoaded = true;
      state.userToken = token.toString();
      state.userName = name;
      state.userIsAdmin = isAdmin;
    },
    [mutations.SET_USER_INFO_FAILURE](state) {
      state.userDataLoaded = false;
      state.userName = '';
      state.userIsAdmin = false;
      state.userToken = '';
    },
  },
  actions: {
    async [actions.GET_USER_INFO]({ commit }, { token }) {
      try {
        if (!token) throw new Error('Token not available');

        const { data } = await UserRequests.getUserFromAuthToken({ token });
        commit(mutations.SET_USER_INFO, data);
      } catch (e) {
        commit(mutations.SET_USER_INFO_FAILURE);
        console.error('SET_USER_INFO_FAILURE', e);
      }
    },
  },
});
