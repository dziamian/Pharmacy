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
        token: localStorage.getItem('auth-token') || null
    },
    getters: {
        isAuthenticated: (state) => !!state.token,
        authStatus: (state) => state.authStatus,
        authToken: (state) => state.token
    },
    actions: {
        setAuthStateChange({commit}, onFirstAuthStateChange) {
            const authStateChangeHandler = async function (user) {
                (user) ? await setToken(commit, 'authSuccess', user) : removeToken(commit, 'authSignout');
            };
            const unsubscribe = authService.setAuthStateChange(async (user) => {
                await authStateChangeHandler(user);
                onFirstAuthStateChange();
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
                authService.signUpWithEmailAndPassword(credentials).then((result) => {
                    resolve(result);
                }).catch((error) => {
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
                authService.signInWithEmailAndPassword(credentials).then((result) => {
                    resolve(result);
                }).catch((error) => {
                    reject(error);
                });
            });
        },
        signInWithGoogle({commit, getters}) {
            return new Promise((resolve, reject) => {
                if (getters.authToken) {
                    reject(new Error("Please first sign out to sign in."));
                    return;
                }
                authService.signInWithGoogle().then((result) => {
                    resolve(result);
                }).catch((error) => {
                    reject(error);
                });
            });
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
        authSuccess: (state, token) => {
            state.token = token;
        },
        authSignout: (state) => {
            state.token = null;
        }
    }
}