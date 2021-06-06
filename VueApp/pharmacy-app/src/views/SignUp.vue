<template>
    <b-container class="mt-5">
        <b-form @submit.stop.prevent="signUp">
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="First name"
                    label-for="firstName-input"
                    invalid-feedback="Enter at least 3 characters.">
                        <b-form-input
                            id="firstName-input"
                            type="text"
                            v-model="userCredentials.firstName"
                            :state="firstNameState"
                            required/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Last name"
                    label-for="lastName-input"
                    invalid-feedback="Enter at least 3 characters.">
                        <b-form-input
                            id="lastName-input"
                            type="text"
                            v-model="userCredentials.lastName"
                            :state="lastNameState"
                            required/>
                    </b-form-group>
                </b-col>
            </b-row>  
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Email"
                    label-for="email-input"
                    invalid-feedback="Invalid email address.">
                        <b-form-input
                            id="email-input"
                            type="email"
                            v-model="userCredentials.email"
                            :state="emailState"
                            required/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Phone number"
                    label-for="phone-input"
                    invalid-feedback="Invalid phone number.">
                        <b-form-input
                            id="phone-input"
                            type="tel"
                            v-model="userCredentials.phone"
                            :state="phoneState"/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3 mt-3">
                    <b-form-group
                        label="Date of birth"
                        label-for="age-input"
                        invalid-feedback="Date of birth is required.">
                        <b-form-input
                            id="age-input"
                            type="date"
                            v-model="userCredentials.dateOfBirth"
                            :state="dateOfBirthState"/>
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
                    invalid-feedback="Password must have at least 4 characters.">
                        <b-form-input
                            id="password-input"
                            type="password"
                            v-model="userCredentials.password"
                            :state="passwordState"
                            required/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label="Password confirm"
                    label-for="passwordConf-input"
                    invalid-feedback="Passwords do not match.">
                        <b-form-input
                            id="passwordConf-input"
                            type="password"
                            v-model="userCredentials.confirmPassword"
                            :state="passwordConfirmState"
                            required/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center mt-3">
                <b-col class="col-lg-3 text-center">
                    <b-button type="submit" variant="primary" size="lg">Sign up</b-button>
                </b-col>
            </b-row>
        </b-form>
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
            formError: false,
            userCredentials: {
                firstName: '',
                lastName: '',
                email: '',
                phone: '',
                dateOfBirth: '',
                gender: 'male',
                password: '',
                confirmPassword: ''
            },
        }
    },
    computed: {
        formState() {
            return (this.firstNameState == null || this.firstNameState) && 
                (this.lastNameState == null || this.lastNameState) && 
                (this.emailState == null || this.emailState) && 
                (this.phoneState == null || this.phoneState) && 
                (this.dateOfBirthState == null || this.dateOfBirthState) && 
                (this.passwordState == null || this.passwordState) &&
                (this.passwordConfirmState == null || this.passwordConfirmState);
        },
        firstNameState() {
            return this.userCredentials.firstName.length > 2;
        },
        lastNameState() {
            return this.userCredentials.lastName.length > 2;
        },
        emailState() {
            return this.userCredentials.email.indexOf('@') > -1;
        },
        phoneState() {
            return null;
        },
        dateOfBirthState() {
            return this.userCredentials.dateOfBirth != '';
        },
        passwordState() {
            return this.userCredentials.password.length > 3;
        },
        passwordConfirmState() {
            if (this.passwordState) {
                if (this.userCredentials.password == this.userCredentials.confirmPassword) {
                    return true;
                }
                return false;
            }
            return null;
        }
    },
    methods: { 
        signUp() {
            if (!this.formState) {
                this.makeToast("Invalid form state", "Please fill out data correctly.", "danger");
                return;
            }
            this.$store.dispatch('user/signUp', this.userCredentials).then(() => {
                this.onSignedIn();
            }).catch((error) => {
                console.log(error.message);
                this.makeToast("Invalid data or account exist", "Try again", "danger");
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