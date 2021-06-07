import axios from 'axios'
import firebase from 'firebase/app'
import 'firebase/firebase-auth'

firebase.auth().languageCode = 'en';
var googleProvider = new firebase.auth.GoogleAuthProvider();
googleProvider
    .addScope('https://www.googleapis.com/auth/userinfo.profile')
    .addScope('https://www.googleapis.com/auth/userinfo.email')
    .addScope('https://www.googleapis.com/auth/user.birthday.read')
    .addScope('https://www.googleapis.com/auth/user.gender.read')
    .addScope('https://www.googleapis.com/auth/user.phonenumbers.read');
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
    getGoogleApiData(id, accessToken) {
        return axios({
            method: 'get',
            url: `https://people.googleapis.com/v1/people/${id}?personFields=birthdays,genders,phoneNumbers&access_token=${accessToken}`
        }).then((response) => {
            return response.data;
        }).catch((error) => {
            throw error.response.data;
        });
    },
    signOut() {
        return firebase.auth().signOut();
    },
    getCurrentUser() {
        return firebase.auth().currentUser;
    },
    setAuthStateChange(method, catchMethod) {
        return firebase.auth().onIdTokenChanged(method, catchMethod);
    }
}