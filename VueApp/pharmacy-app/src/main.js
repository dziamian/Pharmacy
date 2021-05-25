import 'mutationobserver-shim'

import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import router from '@/router'
import store from '@/store'

Vue.config.productionTip = false;

var app = new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');