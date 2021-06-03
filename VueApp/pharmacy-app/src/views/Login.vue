<template>  
    <b-container class="mt-5">
        <b-form >
            <b-row class="justify-content-md-center">
                <b-col class=col-lg-3>
                    <b-form-group
                    label="Email"
                    label-for="email-input"
                    invalid-feedback="Email is required">
                        <b-form-input
                        id="email-input"
                        type="email"
                        v-model="userCredentials.email"
                        required
                        @keydown.native.enter="handleSubmit"/>
                    </b-form-group>
                </b-col>
            </b-row> 
            <b-row class="justify-content-md-center">
                <b-col class=col-lg-3>
                    <b-form-group
                    label="Password"
                    label-for="password-input"
                    invalid-feedback="Password is required">
                        <b-form-input
                        id="password-input"
                        type="password"
                        v-model="userCredentials.password"
                        required
                        @keydown.native.enter="handleSubmit"/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3">
                <b-col class="col-lg-3">
                    <b-button variant="primary" size="lg" @click="handleSubmit">Log in</b-button>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class=col-lg-3>
                    <hr>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3 mb-3">
                <b-col class="col-lg-3">
                    <b-button variant="outline-primary" size="lg" @click="signInWithGoogle">
                        <b-icon icon="google"/> Log in with Google</b-button>
                </b-col>
            </b-row>
        </b-form>
        <Footer/>
    </b-container>
</template>

<script>

import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'

export default {
    name: 'Login',
    components: { 
        Footer 
    },
    data() {
        return {
            userCredentials: {
                email: '',
                password: ''
            },
        }
    },
    methods: { 
        signIn() {
            this.$store.dispatch('user/signIn', this.userCredentials).then(() => {
                this.onSignedIn();
            }).catch((error) => {
                console.log(error.message);
                this.$parent.makeToast("Invalid login or password","Try again","danger");
            });
        },
        signInWithGoogle() {
            this.$store.dispatch('user/signInWithGoogle').then(() => {
                this.onSignedIn();
            }).catch((error) => {
                console.log(error.message);
            });
        },
        handleSubmit() {
            this.signIn();
        },
        onSignedIn() {
            console.log("SUCCESSFULLY SIGNED IN.");
            this.$emit('cart-size-change');
            this.$router.push({name: 'home'});
            api.test().then((data) => {
                console.log(data);
            }).catch((error) => console.log(error));
        }
        
    }
}
</script>

<style scoped>

</style>