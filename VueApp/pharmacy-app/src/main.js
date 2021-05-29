import 'mutationobserver-shim'

import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import router from '@/router'
import store from '@/store'

import '@/plugins/firebase'
import '@/plugins/gmap'

Vue.config.productionTip = false;

const initApp = function () {
  new Vue({
    router,
    store,
    render: h => h(App)
  }).$mount("#app");
}

store.dispatch('user/setAuthStateChange', initApp);

//initApp();