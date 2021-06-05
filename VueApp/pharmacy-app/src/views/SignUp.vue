<template>
    <b-container class="mt-5">
        <b-form >
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="First name"
                    label-for="firstName-input"
                    invalid-feedback="First name is required">
                        <b-form-input
                            id="firstName-input"
                            type="text"
                            v-model="userCredentials.firstName"
                            required
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Last name"
                    label-for="lastName-input"
                    invalid-feedback="Last name is required">
                        <b-form-input
                            id="lastName-input"
                            type="text"
                            v-model="userCredentials.lastName"
                            required
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
            </b-row>  
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Email"
                    label-for="email-input"
                    invalid-feedback="Email is required">
                        <b-form-input
                            id="email-input"
                            type="email"
                            v-model="userCredentials.email"
                            required
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Phone number"
                    label-for="phone-input"
                    invalid-feedback="Phone is required">
                        <b-form-input
                            id="phone-input"
                            type="tel"
                            v-model="userCredentials.phone"
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3 mt-3">
                    <b-form-group
                        label="Date of birth"
                        label-for="age-input"
                        invalid-feedback="Age is required">
                        <b-form-input
                            id="age-input"
                            type="date"
                            v-model="userCredentials.age"
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group label="Gender" v-slot="{ gender }">
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Male" value="male">Male</b-form-radio>
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Female" value="female">Female</b-form-radio>
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Other" value="other">Other </b-form-radio>
                    </b-form-group>
                </b-col>
            </b-row> 
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Password"
                    label-for="password-input"
                    invalid-feedback="Password is required">
                        <b-form-input
                            id="password-input"
                            type="password"
                            v-model="userCredentials.password"
                            required
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Password confirm"
                    label-for="passwordConf-input"
                    invalid-feedback="Password is incorrect">
                        <b-form-input
                            id="passwordConf-input"
                            type="password"
                            required
                            @keydown.native.enter="signUp"/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3">
                <b-col class="col-lg-3 text-center">
                    <b-button variant="primary" size="lg" @click="signUp">Sign up</b-button>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3">
                <b-col class="col-lg-6">
                    <hr class="or">
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3 mb-3">
                <b-col class="col-lg-3 text-center">
                    <b-button variant="outline-primary" size="lg" @click="signInWithGoogle">
                        <b-icon icon="google"/> Log in with Google</b-button>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-6">
                    <hr>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3 text-center">
                    <p class="signUpRef">If you have an account: <b-link to="SignIn">Log in</b-link></p>
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
    name: 'SignUp',
    components: { 
        Footer 
    },
    data() {
        return {
            userCredentials: {
                firstName: '',
                lastName: '',
                email: '',
                phone: '',
                age: '',
                gender: '',
                password: ''
            },
        }
    },
    methods: { 
        signUp() {
            this.$store.dispatch('user/signUp', this.userCredentials).then(() => {
                this.onSignedIn();
            }).catch((error) => {
                console.log(error.message);
                this.makeToast("Invalid data or account exist","Try again","danger");
            });
        },
        signInWithGoogle() {
            this.$store.dispatch('user/signInWithGoogle').then(() => {
                this.onSignedIn();
            }).catch((error) => {
                console.log(error.message);
            });
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

</style>