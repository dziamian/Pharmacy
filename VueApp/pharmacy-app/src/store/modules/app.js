export default {
    state: {
        initialized: false
    },
    getters: {
        isInitialized: (state) => state.initialized
    },
    actions: {
        initialize({commit, getters}, initApp) {
            if (!getters.isInitialized) {
                initApp();
                commit('initialize');
            }
        }
    },
    mutations: {
        initialize: (state) => {
            state.initialized = true;
        }
    }
}