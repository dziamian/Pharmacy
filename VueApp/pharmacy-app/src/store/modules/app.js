import Vue from 'vue'
import App from '@/App'

import '@/plugins/bootstrap-vue'
import router from '@/router'
import store from '@/store'
import helpers from '@/helpers'
import '@/plugins/scrollto'

import '@/plugins/firebase'
import '@/plugins/gmap'

Vue.config.productionTip = false;

const initApp = function () {
    var app;
    helpers.getContactInfo()
    .then((contact) => {
        helpers.initMixin(contact);
    }).catch((error) => {
        helpers.initMixin();
    }).finally(() => {
        app = new Vue({
            router,
            store,
            render: h => h(App)
        }).$mount("#app");
    });
    return app;
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
                dispatch('user/onFirstAuthStateChange', () => commit('initialize', initApp()), {root: true});
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