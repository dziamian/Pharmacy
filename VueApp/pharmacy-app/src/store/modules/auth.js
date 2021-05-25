import authService from '@/services/AccountService'

export default {
    state: {
        token: localStorage.getItem('auth-token') || '',
        status: ''
    },
    getters: {
        isAuthenticated: (state) => !!state.token,
        authStatus: (state) => state.status,
        authToken: (state) => state.token
    },
    actions: {
        async authRequest({commit, dispatch}, credentials) {
            return new Promise((resolve, reject) => {
                commit('authRequest');
                authService.login(credentials).then(result => {
                    localStorage.setItem('auth-token', result.auth_token);
                    commit('authSuccess', result.auth_token);
                    resolve(result.auth_token);
                }).catch((err) => {
                    commit('authError', err);
                    localStorage.removeItem('auth-token');
                    reject(err);
                });
            });
            /*commit('authRequest');
            const result = (await authService.login(credentials, (errors) => {

            })).auth_token;
            localStorage.setItem('auth-token', result);
            commit('authSuccess', result);
            return result;*/
            /*return new Promise((resolve, reject) => {
                commit('authRequest');
                const result = await authService.login(credentials);
                localStorage.setItem('auth-token', result);
                commit('authSuccess', result);
                resolve(result);
            });*/
        }
    },
    mutations: {
        authRequest: (state) => {
            state.status = 'Attempting authentication request.';
        },
        authSuccess: (state, authToken) => {
            state.status = 'Authentication succeeded.';
            state.token = authToken;
        },
        authError: (state) => {
            state.status = 'Error';
        }
    }
}