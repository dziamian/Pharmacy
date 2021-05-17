import 'mutationobserver-shim'

import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import '@/plugins/axios'
import router from '@/router'

Vue.config.productionTip = false;

var app = new Vue({
  router,
  render: h => h(App)
}).$mount('#app');