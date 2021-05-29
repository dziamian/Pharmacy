import Vue from 'vue'
import Vuex from 'vuex'
import user from '@/store/modules/user'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    user: {
      namespaced: true,
      state: user.state,
      actions: user.actions,
      mutations: user.mutations,
      getters: user.getters
    }
  }
})
