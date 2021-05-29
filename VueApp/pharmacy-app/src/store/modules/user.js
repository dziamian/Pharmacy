import authService from '@/services/AccountService'

const setToken = async function (commit, commitName, user) {
    const token = await user.getIdToken(false);
    commit(commitName, token);
    localStorage.setItem('auth-token', token);
    console.log("Current auth token: " + token);
};

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
        setAuthStateChange({commit, dispatch}) {
            authService.setAuthStateChange(async (user) => {
                if (user) {
                    setToken(commit, 'authUpdate', user);
                }
            });
        },
        signUp({commit, dispatch}, credentials) {
            return new Promise((resolve, reject) => {
                commit('authRequest');
                authService.signUpWithEmailAndPassword(credentials).then((result) => {
                    const user = authService.getCurrentUser();
                    setToken(commit, 'authSuccess', user);
                    resolve(user);
                }).catch((error) => {
                    commit('authError', error);
                    localStorage.removeItem('auth-token');
                    reject(error);
                });
            });
        },
        signIn() {
            
        },
        signInWithGoogle() {

        },
        signOut({commit, dispatch}) {
            return new Promise((resolve, reject) => {
                commit('authSignout');
                localStorage.removeItem('auth-token');
                resolve();
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
            state.token = null;
        }
    }
}