<template>  
    <b-container class="mt-5">
        <b-form @submit.stop.prevent="signIn">
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Email"
                    label-for="email-input">
                        <b-form-input
                        id="email-input"
                        type="email"
                        v-model="userCredentials.email"
                        placeholder="Enter your e-mail"
                        :state="emailState"
                        required/>
                    </b-form-group>
                </b-col>
            </b-row> 
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Password"
                    label-for="password-input">
                        <b-form-input
                        id="password-input"
                        type="password"
                        v-model="userCredentials.password"
                        placeholder="Enter your password"
                        :state="passwordState"
                        required/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3">
                <b-col class="col-lg-3 text-center">
                    <b-button type="submit" variant="primary" size="lg">Log in with Email</b-button>
                </b-col>
            </b-row>
        </b-form>
        <b-row class="justify-content-md-center mt-3">
            <b-col class="col-lg-3">
                <hr class="or">
            </b-col>
        </b-row>
        <GoogleButton :onSignedIn="onSignedIn"/>
        <b-row class="justify-content-md-center">
            <b-col class="col-lg-3">
                <hr>
            </b-col>
        </b-row>
        <b-row class="justify-content-md-center">
            <b-col class="col-lg-3 text-center">
                <p class="signUpRef">If you don't have an account: <b-link to="SignUp">click here</b-link></p>
            </b-col>
        </b-row>
        <Footer/>
    </b-container>
</template>

<script>

import api from '@/services/PharmacyApiService'

import GoogleButton from '@/components/GoogleButton'
import Footer from '@/components/Footer'

export default {
    name: 'signIn',
    components: { 
        GoogleButton,
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
        computed: {
        formState() {
            return (this.emailState == null || this.emailState) && 
                (this.passwordState == null || this.passwordState);
        },
        emailState() {
            if (this.userCredentials.email.length == 0) {
                return null;
            }
            return (this.userCredentials.email.indexOf('@') > -1) && 
                (this.userCredentials.email.length > 5) && 
                (this.userCredentials.email.indexOf('.') > -1);
        },
        passwordState() {
            if (this.userCredentials.password.length == 0) {
                return null;
            }
            return this.userCredentials.password.length > 5;
        },
    },
    methods: { 
        signIn() {
            if (!this.formState) {
                this.makeToast("Invalid form state", "Please fill out data correctly.", "danger");
                return;
            }
            this.$store.dispatch('user/signIn', this.userCredentials).then(() => {
                this.onSignedIn();
            }).catch((error) => {
                const message = error.message.split('.');
                this.makeToast(message[0], message[1] + '.', "danger");
            });
        },
        onSignedIn() {
            this.$emit('cart-size-change');
            this.$router.push({name: 'home'});
        }
    },
    mounted() {
        this.$parent.setActive('');
    }
}
</script>

<style scoped>

.or {
    color: rgb(145, 145, 145);
    overflow: visible;
    text-align: center;
    height: 5px;
}

.or:after {
    background: #fff;
    content: 'Or';
    padding: 0 4px;
    position: relative;
    top: -15px;
}

.signUpRef {
    font-size: small;
}

</style>