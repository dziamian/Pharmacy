import 'mutationobserver-shim'

import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import router from '@/router'
import store from '@/store'

import '@/plugins/firebase'
import '@/plugins/gmap'

Vue.config.productionTip = false;

store.dispatch('user/setAuthStateChange');

var app = new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');