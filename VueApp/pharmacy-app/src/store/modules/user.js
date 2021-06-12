import api from '@/services/PharmacyApiService'
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
        onFirstAuthStateChange({commit, dispatch}, method) {
            const unsubscribe = authService.setAuthStateChange(async () => {
                const isReachable = await api._isReachable();
                if (isReachable) {
                    await dispatch('getAuthToken');
                }
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
                api._isReachable().then((isReachable) => {
                    if (!isReachable) {
                        reject(new Error("Server connection error. Please try again later."));
                        return;
                    }
                    if (getters.isAuthenticated) {
                        reject(new Error("You are currently logged in. Please log out first."));
                        return;
                    }

                    authService.signUpWithEmailAndPassword(credentials).then(async (user) => {
                        await dispatch('getAuthToken');
                        resolve(user);
                    }).catch(() => {
                        reject(new Error("Account with this e-mail exists. Log in or use a different email."));
                    });
                }).catch(() => {
                    reject(new Error("Server connection error. Please try again later."));
                });
            });
        },
        signIn({dispatch, getters}, credentials) {
            return new Promise((resolve, reject) => {
                api._isReachable().then((isReachable) => {
                    if (!isReachable) {
                        reject(new Error("Server connection error. Please try again later."));
                        return;
                    }
                    if (getters.isAuthenticated) {
                        reject(new Error("You are currently logged in. Please log out first."));
                        return;
                    }

                    authService.signInWithEmailAndPassword(credentials).then(async (user) => {
                        await dispatch('getAuthToken');
                        resolve(user);
                    }).catch(() => {
                        reject(new Error("Invalid login or password. Try again."));
                    });
                }).catch(() => {
                    reject(new Error("Server connection error. Please try again later."));
                });
            });
        },
        signInWithGoogle({dispatch, getters}) {
            return new Promise((resolve, reject) => {
                api._isReachable().then((isReachable) => {
                    if (!isReachable) {
                        reject(new Error("Server connection error. Please try again later."));
                        return;
                    }
                    if (getters.isAuthenticated) {
                        reject(new Error("You are currently logged in. Please log out first."));
                        return;
                    }

                    authService.signInWithGoogle().then(async (user) => {
                        await dispatch('getAuthToken');
                        resolve(user);
                    }).catch(() => {
                        reject(new Error("An error occurred. Couldn't sign in with Google."));
                    });
                }).catch(() => {
                    reject(new Error("Server connection error. Please try again later."));
                });
            });
        },
        getGoogleApiData({commit}, credentials) {
            return new Promise((resolve, reject) => {
                authService.getGoogleApiData(credentials.id, credentials.accessToken).then((response) => {
                    resolve(response);
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