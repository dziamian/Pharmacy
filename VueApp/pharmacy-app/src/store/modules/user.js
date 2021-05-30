import authService from '@/services/AccountService'

const setToken = async function (commit, commitName, user) {
    const token = await user.getIdToken(false);
    commit(commitName, token);
    localStorage.setItem('auth-token', token);
    console.log("Current auth token: " + token);
};

const removeToken = function (commit, commitName) {
    commit(commitName);
    localStorage.removeItem('auth-token');
    console.log("Current auth token: null");
}

export default {
    state: {
        authStatus: '',
        token: localStorage.getItem('auth-token') || null
    },
    getters: {
        isAuthenticated: (state) => !!state.token,
        authStatus: (state) => state.authStatus,
        authToken: (state) => state.token
    },
    actions: {
        setAuthStateChange({commit, dispatch}, initApp) {
            const authStateChangeHandler = async function (user) {
                (user) ? await setToken(commit, 'authUpdate', user) : removeToken(commit, 'authSignout');
            };
            const unsubscribe = authService.setAuthStateChange(async (user) => {
                await authStateChangeHandler(user);
                dispatch('app/initialize', initApp, {root: true});
                unsubscribe();
                authService.setAuthStateChange(authStateChangeHandler);
            });
        },
        signUp({commit, getters}, credentials) {
            return new Promise((resolve, reject) => {
                if (getters.authToken) {
                    reject(new Error("Please first sign out to sign up."));
                    return;
                }
                commit('authRequest');
                authService.signUpWithEmailAndPassword(credentials).then((result) => {
                    resolve(result);
                }).catch((error) => {
                    commit('authError', error);
                    reject(error);
                });
            });
        },
        signIn({commit, getters}, credentials) {
            return new Promise((resolve, reject) => {
                if (getters.authToken) {
                    reject(new Error("Please first sign out to sign in."));
                    return;
                }
                commit('authRequest');
                authService.signInWithEmailAndPassword(credentials).then((result) => {
                    resolve(result);
                }).catch((error) => {
                    commit('authError', error);
                    reject(error);
                });
            });
        },
        signInWithGoogle() {
            
        },
        signOut({commit}) {
            return new Promise((resolve) => {
                commit('authSignout');
                authService.signOut().then(() => {
                    localStorage.removeItem('auth-token');
                    resolve();
                });
            });
        }
    },
    mutations: {
        authRequest: (state) => {
            state.authStatus = 'Attempting authentication request.';
        },
        authSuccess: (state, token) => {
            state.authStatus = 'Authentication succeeded.';
            state.token = token;
        },
        authError: (state, error) => {
            state.authStatus = error;
            state.token = null;
        },
        authUpdate: (state, token) => {
            state.token = token;
        },
        authSignout: (state) => {
            state.authStatus = '';
            state.token = null;
        }
    }
}