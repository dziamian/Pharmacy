import authService from '@/services/AccountService'

const setToken = async function (commit, commitName, user) {
    const token = await user.getIdToken(false);
    commit(commitName, token);
    localStorage.setItem('auth-token', token);
    console.log("Current auth token: " + token);
    return token;
};

const removeToken = function (commit, commitName) {
    commit(commitName);
    localStorage.removeItem('auth-token');
    console.log("Current auth token: null");
    return null;
}

export default {
    state: {
        token: localStorage.getItem('auth-token') || null
    },
    getters: {
        isAuthenticated: (state) => !!state.token
    },
    actions: {
        onFirstAuthStateChange({commit}, method) {
            const unsubscribe = authService.setAuthStateChange(async (user) => {
                method();
                unsubscribe();
            });
        },
        async getAuthToken({commit}) {
            const user = authService.getCurrentUser();
            if (user == null) {
                return removeToken(commit, 'authFailure');
            }
            return await setToken(commit, 'authSuccess', user);
        },
        signUp({dispatch, getters}, credentials) {
            return new Promise((resolve, reject) => {
                if (getters.isAuthenticated) {
                    reject(new Error("You are currently logged in. Please log out first."));
                    return;
                }
                authService.signUpWithEmailAndPassword(credentials).then(async (result) => {
                    await dispatch('getAuthToken');
                    resolve(result);
                }).catch((error) => {
                    reject(error);
                });
            });
        },
        signIn({dispatch, getters}, credentials) {
            return new Promise((resolve, reject) => {
                if (getters.isAuthenticated) {
                    reject(new Error("You are currently logged in. Please log out first."));
                    return;
                }
                authService.signInWithEmailAndPassword(credentials).then(async (result) => {
                    await dispatch('getAuthToken');
                    resolve(result);
                }).catch((error) => {
                    reject(error);
                });
            });
        },
        signInWithGoogle({dispatch, getters}) {
            return new Promise((resolve, reject) => {
                if (getters.isAuthenticated) {
                    reject(new Error("You are currently logged in. Please log out first."));
                    return;
                }
                authService.signInWithGoogle().then(async (result) => {
                    await dispatch('getAuthToken');
                    resolve(result);
                }).catch((error) => {
                    reject(error);
                });
            });
        },
        signOut({commit, getters}) {
            return new Promise((resolve, reject) => {
                if (!getters.isAuthenticated) {
                    reject(new Error("You are currently logged out."));
                    return;
                }
                authService.signOut().then(() => {
                    removeToken(commit, 'authSignout');
                    resolve();
                }).catch((error) => {
                    reject(error);
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
        },
        authFailure: (state) => {
            state.token = null;
        }
    }
}