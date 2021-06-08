<template>
    <b-container class="mt-5">
        <b-form @submit.stop.prevent="signUp">
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-6">
                    <b-form-group
                    label-for="Name-input"
                    invalid-feedback="Enter at least 5 characters.">
                        <template v-slot:label>
                            Name <span class="text-danger font-weight-bold h5">*</span>
                        </template>
                        <b-form-input
                            id="Name-input"
                            type="text"
                            v-model="userCredentials.name"
                            placeholder="Enter your name"
                            :state="nameState"
                            required/>
                    </b-form-group>
                </b-col>
            </b-row>  
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label-for="email-input"
                    invalid-feedback="Invalid email address.">
                        <template v-slot:label>
                            Email <span class="text-danger font-weight-bold h5">*</span>
                        </template>
                        <b-form-input
                            id="email-input"
                            type="email"
                            v-model="userCredentials.email"
                            placeholder="Enter your e-mail"
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
                            placeholder="Enter your phone number"
                            :state="phoneState"/>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3 mt-3">
                    <b-form-group
                        label="Date of birth"
                        label-for="age-input"
                        invalid-feedback="Date of birth must be in the past.">
                        <b-form-input
                            id="age-input"
                            type="date"
                            :max="currentDate"
                            v-model="userCredentials.dateOfBirth"
                            :state="dateOfBirthState"/>
                    </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group v-slot="{ gender }" label="Gender">
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Male" value="m">Male</b-form-radio>
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Female" value="f">Female</b-form-radio>
                        <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Other" value="o">Other</b-form-radio>
                    </b-form-group>
                </b-col>
            </b-row> 
            <b-row class="justify-content-md-center">
                <b-col class="col-lg-3">
                    <b-form-group
                    label-for="password-input"
                    invalid-feedback="Password must have at least 4 characters.">
                    <template v-slot:label>
                        Password <span class="text-danger font-weight-bold h5">*</span>
                    </template>
                        <b-form-input
                            id="password-input"
                            type="password"
                            v-model="userCredentials.password"
                            placeholder="Enter your password"
                            :state="passwordState"
                            required/>
                        </b-form-group>
                </b-col>
                <b-col class="col-lg-3">
                    <b-form-group
                    label-for="passwordConf-input"
                    invalid-feedback="Passwords do not match.">
                        <template v-slot:label>
                            Password confirm <span class="text-danger font-weight-bold h5">*</span>
                        </template>
                        <b-form-input
                            id="passwordConf-input"
                            type="password"
                            v-model="userCredentials.confirmPassword"
                            placeholder="Repeat your password"
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
        <GoogleButton :onSignedIn="onSignedIn"/>
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

import GoogleButton from '@/components/GoogleButton'
import Footer from '@/components/Footer'

export default {
    name: 'signUp',
    components: { 
        GoogleButton,
        Footer 
    },
    data() {
        return {
            currentDate: this.getFormattedDate(new Date()),
            userCredentials: {
                name: '',
                email: '',
                phone: '',
                dateOfBirth: '',
                gender: 'm',
                password: '',
                confirmPassword: ''
            },
        }
    },
    computed: {
        formState() {
            return (this.nameState == null || this.nameState) && 
                (this.emailState == null || this.emailState) && 
                (this.phoneState == null || this.phoneState) && 
                (this.dateOfBirthState == null || this.dateOfBirthState) && 
                (this.passwordState == null || this.passwordState) &&
                (this.passwordConfirmState == null || this.passwordConfirmState);
        },
        nameState() {
            if (this.userCredentials.name.length == 0) {
                return null;
            }
            return this.userCredentials.name.length > 4;
        },
        emailState() {
            if (this.userCredentials.email.length == 0) {
                return null;
            }
            return (this.userCredentials.email.indexOf('@') > -1) && 
                (this.userCredentials.email.length > 5) && 
                (this.userCredentials.email.indexOf('.') > -1);
        },
        phoneState() {
            if (this.userCredentials.phone.length == 0) {
                return null;
            }
            return this.userCredentials.phone.length == 9; 
        },
        dateOfBirthState() {
            if (this.userCredentials.dateOfBirth.length == 0) {
                return null;
            }
            return Date.parse(this.userCredentials.dateOfBirth) <= new Date();
        },
        passwordState() {
            if (this.userCredentials.password.length == 0) {
                return null;
            }
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
                api.createAccount({
                    name: this.userCredentials.name,
                    email: this.userCredentials.email,
                    phone: this.userCredentials.phone == '' ? undefined : this.userCredentials.phone,
                    dateOfBirth: this.userCredentials.dateOfBirth == '' ? undefined : this.userCredentials.dateOfBirth,
                    gender: this.userCredentials.gender
                }).then(() => {
                    this.onSignedIn();
                });
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

</style>