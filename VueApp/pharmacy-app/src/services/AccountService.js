import firebase from 'firebase/app'
import 'firebase/firebase-auth'

var googleProvider = new firebase.auth.GoogleAuthProvider();
googleProvider
    .addScope('https://www.googleapis.com/auth/userinfo.profile')
    .addScope('https://www.googleapis.com/auth/userinfo.email');
googleProvider.setCustomParameters({
    'prompt': 'consent'
});

export default {
    signUpWithEmailAndPassword(credentials) {
        return firebase.auth()
            .createUserWithEmailAndPassword(credentials.email, credentials.password);
    },
    signInWithEmailAndPassword(credentials) {
        return firebase.auth()
            .signInWithEmailAndPassword(credentials.email, credentials.password);
    },
    signInWithGoogle() {
        return firebase.auth()
            .signInWithPopup(googleProvider);
    },
    signOut() {
        return firebase.auth().signOut();
    },
    getCurrentUser() {
        return firebase.auth().currentUser;
    },
    setAuthStateChange(method) {
        //firebase.auth().onAuthStateChanged(method);
        firebase.auth().onIdTokenChanged(method);
    }
}