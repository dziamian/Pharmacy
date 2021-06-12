<template> <!-- TODO: Dodać dane pobrane z servera do placeholderów-->
    <b-container class="mt-5">
        <b-container>
            <b-form v-model="editing">
                <b-row class="justify-content-md-center">
                    <b-col class="col-lg-6">
                        <b-form-group
                            label="Name"
                            label-for="Name-input"
                            invalid-feedback="Enter at least 5 characters.">
                            <b-form-input
                                :disabled="editing == false"
                                id="Name-input"
                                type="text"
                                v-model="userCredentials.name"
                                :placeholder="userCredentials.name"
                                :state="(editing) ? nameState : null"/>
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
                                disabled
                                id="email-input"
                                type="email"
                                v-model="userCredentials.email"
                                :placeholder="userCredentials.email"
                                :state="(editing) ? emailState : null"/>
                        </b-form-group>
                    </b-col>
                    <b-col class="col-lg-3">
                        <b-form-group
                            label="Phone number"
                            label-for="phone-input"
                            invalid-feedback="Invalid phone number.">
                            <b-form-input
                                :disabled="editing == false"
                                id="phone-input"
                                type="tel"
                                v-model="userCredentials.phone"
                                :placeholder="userCredentials.phone"
                                :state="(editing) ? phoneState : null"/>
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
                                :disabled="editing == false"
                                id="age-input"
                                type="date"
                                :max="currentDate"
                                v-model="userCredentials.dateOfBirth"
                                :placeholder="userCredentials.dateOfBirth"
                                :state="(editing) ? dateOfBirthState : null"/>
                        </b-form-group>
                    </b-col>
                    <b-col class="col-lg-3">
                        <b-form-group v-slot="{ gender }" label="Gender" :disabled="editing == false">
                            <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Male" value="m">Male</b-form-radio>
                            <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Female" value="f">Female</b-form-radio>
                            <b-form-radio v-model="userCredentials.gender" :aria-describedby="gender" name="Other" value="o">Other</b-form-radio>
                        </b-form-group>
                    </b-col>
                </b-row> 
            </b-form>
            <b-row class="mt-5" v-if="editing==false">
                <b-col class="text-right">
                    <b-button class="mr-3" variant="primary" :pressed.sync="editing">Edit user data</b-button>
                    <b-button class="mr-5"  v-b-toggle.ordersTable variant="primary">Show orders</b-button>   
                </b-col>
            </b-row>
            <b-row class="mt-5" v-else>
                <b-col class="text-right">
                    <b-button class="mr-3" variant="primary" :pressed.sync="editing">Cancel changes</b-button>
                    <b-button class="mr-3" variant="primary" :pressed.sync="editing">Save changes</b-button>  
                </b-col>
            </b-row>
        </b-container>
        <b-collapse class="mt-5 mb-5" id="ordersTable">
            <b-container>
                <b-row class="justify-content-md-center">
                    <table class="table table-bordered">
                        <thead class="text-center">
                            <tr>
                                <th>No.</th>
                                <th>Products</th>
                                <th>Total cost</th>
                                <th>Date of order</th>
                            </tr>
                        </thead>
                        <tbody class="text-center">
                            <tr>
                                <td>1</td>
                                <td>Przykład <br>
                                    1. Apap x 2 <br>
                                    2. Paracetamol x 5 <br>
                                </td>
                                <td>150 zł</td>
                                <td>10.06.2021</td>
                            </tr>
                        </tbody>
                    </table>
                </b-row>
            </b-container>
        </b-collapse>
        <Footer/>
    </b-container>
</template>

<script>

import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'

export default {
    components: {
        Footer,
    },
    data() {
        return {
            editing: false,
            currentDate: this.getFormattedDate(new Date()),
            userCredentials: {
                name: '',
                email: '',
                phone: '',
                dateOfBirth: '',
                gender: 'm'
            },
        }
    },
    computed: {
        nameState() {
            if (!this.userCredentials.name || this.userCredentials.name.length == 0) {
                return null;
            }
            return this.userCredentials.name.length > 4;
        },
        emailState() {
            if (!this.userCredentials.email || this.userCredentials.email.length == 0) {
                return null;
            }
            return (this.userCredentials.email.indexOf('@') > -1) && 
                (this.userCredentials.email.length > 5) && 
                (this.userCredentials.email.indexOf('.') > -1);
        },
        phoneState() {
            if (!this.userCredentials.phone || this.userCredentials.phone.length == 0) {
                return null;
            }
            return this.userCredentials.phone.length == 9; 
        },
        dateOfBirthState() {
            if (!this.userCredentials.dateOfBirth || this.userCredentials.dateOfBirth.length == 0) {
                return null;
            }
            return Date.parse(this.userCredentials.dateOfBirth) <= new Date();
        },
    },
    created() {
        this.getUserData();
    },
    methods: {
        getUserData() {
            api.getAccountInfo()
                .then((result) => {
                    this.userCredentials = result;
                }).catch((errors) => {
                    this.makeToast("Couldn't load user data from the server");
                });
        },
    },
    mounted() {
        this.$parent.setActive('');
    }
}

</script>

<style scoped>

td{
    vertical-align: middle !important;
    text-align: center !important;
}

</style>