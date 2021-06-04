import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import router from '@/router'
import store from '@/store'
import '@/plugins/mixins'
import '@/plugins/scrollto'

import '@/plugins/firebase'
import '@/plugins/gmap'

Vue.config.productionTip = false;

const initApp = function () {
    return new Vue({
        router,
        store,
        render: h => h(App)
    }).$mount("#app");
};

export default {
    state: {
        initialized: false,
        app: null
    },
    getters: {
        isInitialized: (state) => state.initialized
    },
    actions: {
        initialize({commit, dispatch, getters}) {
            if (!getters.isInitialized) {
                dispatch('user/setAuthStateChange', () => commit('initialize', initApp()), {root: true});
            }
        }
    },
    mutations: {
        initialize: (state, app) => {
            state.initialized = true;
            state.app = app;
        }
    }
}