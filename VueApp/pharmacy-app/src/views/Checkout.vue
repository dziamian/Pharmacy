<template>
    <b-container class="mt-5">
        <b-row class="mb-5">   
            <b-col class="md-6 mb-5 mb-md-0">
                <b-form>
                    <h2 class="h3 mb-3 text-black">Billing Details</h2>
                    <div class="p-3 p-lg-5 border">
                        <div class="form-group row">
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="name-input">
                                    <template v-slot:label>
                                        Name <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="name-input"
                                        type="text"
                                        v-model="userData.name"
                                        placeholder="Enter your name"
                                        :state="nameState"
                                        required/>
                                </b-form-group>
                            </b-col>
                        </div>
                
                        <div class="form-group row">
                            <b-col class="md-12">
                                <b-form-group
                                    label-for="streetAddress-input">
                                    <template v-slot:label>
                                        Address <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="streetAddress-input"
                                        type="text"
                                        v-model="userAddressInfo.streetAddress"
                                        placeholder="Street address"
                                        :state="streetAddressState"
                                        required/>
                                </b-form-group>
                            </b-col>
                        </div>
                
                        <div class="form-group row">
                            <b-col  class="md-12">
                                <b-form-group
                                    label-for="apartmentAddress-input">
                                    <b-form-input
                                        id="apartmentAddress-input"
                                        type="text"
                                        v-model="userAddressInfo.apartmentAddress"
                                        placeholder="Apartment, suite, unit etc."
                                        :state="apartmentAddressState"/>
                                </b-form-group>
                            </b-col>
                        </div>

                        <div class="form-group row">
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="city-input">
                                    <template v-slot:label>
                                        City <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="city-input"
                                        type="text"
                                        v-model="userAddressInfo.city"
                                        placeholder="City name"
                                        :state="cityState"
                                        required/>
                                </b-form-group>
                            </b-col>
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="postcode-input">
                                    <template v-slot:label>
                                        Postcode <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="postcode-input"
                                        type="text"
                                        v-model="userAddressInfo.postcode"
                                        placeholder="Postcode number"
                                        :state="postcodeState"
                                        required/>
                                    </b-form-group>
                            </b-col>
                        </div>
                        
                        <div class="form-group">
                            <label for="shipDiffrentAddress" class="text-black" data-toggle="collapse"
                                href="#shipDiffrentAddress" role="button" aria-expanded="false"
                                aria-controls="shipDiffrentAddress"><input v-b-toggle.shipDiffrentAddress type="checkbox" id="shipDiffrentAddress">
                                Ship To A Different Address?</label>
                            <b-collapse id="shipDiffrentAddress">
                                <div class="py-2">
                                    <div class="form-group row">
                                        <b-col class="md-12">
                                            <b-form-group
                                                label-for="alternativeStreetAddress-input">
                                                <template v-slot:label>
                                                    Address <span class="text-danger font-weight-bold h5">*</span>
                                                </template>
                                                <b-form-input
                                                    id="alternativeStreetAddress-input"
                                                    type="text"
                                                    v-model="userAddressInfo.alternativeStreetAddress"
                                                    placeholder="Street address"/>
                                            </b-form-group>
                                        </b-col>
                                    </div>
                    
                                    <div class="form-group row">
                                        <b-col  class="md-12">
                                            <b-form-group
                                                label-for="alternativeApartmentAddress-input">
                                                <b-form-input
                                                    id="alternativeApartmentAddress-input"
                                                    type="text"
                                                    v-model="userAddressInfo.alternativeApartmentAddress"
                                                    placeholder="Apartment, suite, unit etc."/>
                                            </b-form-group>
                                        </b-col>
                                    </div>
                    
                                    <div class="form-group row">
                                        <b-col class="md-6">
                                            <b-form-group
                                                label-for="alternativeCity-input">
                                                <template v-slot:label>
                                                    City <span class="text-danger font-weight-bold h5">*</span>
                                                </template>
                                                <b-form-input
                                                    id="alternativeCity-input"
                                                    type="text"
                                                    v-model="userAddressInfo.alternativeCity"
                                                    placeholder="City name"/>
                                            </b-form-group>
                                        </b-col>
                                        <b-col class="md-6">
                                            <b-form-group
                                                label-for="alternativePostcode-input">
                                                <template v-slot:label>
                                                    Postcode <span class="text-danger font-weight-bold h5">*</span>
                                                </template>
                                                <b-form-input
                                                    id="alternativePostcode-input"
                                                    type="text"
                                                    v-model="userAddressInfo.alternativePostcode"
                                                    placeholder="Postcode number"/>
                                                </b-form-group>
                                        </b-col>
                                    </div>
                                </div>
                            </b-collapse>
                        </div>

                        <div class="form-group">
                            <b-form-group
                                label-for="orderNotes-input">
                                <template v-slot:label>
                                    Order notes <span class="text-danger font-weight-bold h5">*</span>
                                </template>
                                <b-form-input
                                    id="orderNotes-input"
                                    type="text"
                                    v-model="userAddressInfo.orderNotes"
                                    placeholder="Write your notes here..."/>
                            </b-form-group>
                        </div>
                    </div>
                </b-form>
            </b-col>
            <b-col class="md-6">
                <b-row class="mb-5">
                    <b-col class="md-12">
                        <h2 class="h3 mb-3 text-black">Your Order</h2>
                        <div class="p-3 p-lg-5 border">
                            <table class="table site-block-order-table mb-5">
                                <thead>
                                    <th>Product</th>
                                    <th>Total</th>
                                </thead>
                                <tbody>
                                    <tr v-for="(element, index) in cart" :key="index">
                                        <td>{{element.product.name}}<strong class="mx-2">x</strong> {{element.amount}}</td>
                                        <td>{{getCost(element.product.cost*element.amount)}} {{BILLING.CURRENCY.ABB}}</td>
                                    </tr>
                                    <tr>
                                        <td class="text-black font-weight-bold"><strong>Order Total</strong></td>
                                        <td class="text-black font-weight-bold"><strong>{{getCost(getTotalCost())}} {{BILLING.CURRENCY.ABB}}</strong></td>
                                    </tr>
                                </tbody>
                            </table>
                            <div id="paypal-button"></div>
                        </div>
                    </b-col>
                </b-row>
            </b-col>
        </b-row>
        <Footer/>
    </b-container>
</template>

<script>
import Footer from '@/components/Footer'
import Loading from '@/components/Loading'

import api from '@/services/PharmacyApiService'

export default {
    components:{
        Footer,
        Loading
    },
    data() {
        return {
            paypal: null,
            cart: [],
            userOrders: [],
            selectedPaymentOption: "paypal",
            loading: true,
            userData: {
                name: '',
            },
            userAddressInfo: {
                streetAddress: '',
                apartmentAddress: '',
                city: '',
                postcode: '',
                orderNotes: '',
                alternativeAddress: '',
                alternativeCity: '',
                alternativeapartmentAddress: '',
                alternativePostCode: ''
            },
        }
    },
    created() {
        this.getItemsFromCart();
        this.getUserData();
    },
    methods: {
        getItemsFromCart() {
            this.loading = true;
            api.getItemsFromCart()
                .then((result) => {
                    this.cart = result;
                    this.cart.forEach((element) => {
                        element.product.image = api._getBaseURL() + element.product.image;
                    });
                }).catch((errors) => {
                    this.cart = [];
                }).finally(() => {
                    this.loading = false;
                });
        },
        getTotalCost() {
            var sum = 0;
            this.cart.forEach((element) =>{
                sum += element.product.cost * element.amount;
            });
            return sum;
        },
        setLoaded() {
            this.loading = false;
            this.paypal = window.paypal.Buttons({
                fundingSource: window.paypal.FUNDING.PAYPAL,
                createOrder: (data, actions) => {
                    return actions.order.create({
                        purchase_units: [
                            {
                                description: "XDDesc",
                                amount: {
                                    value: "50.55"
                                }
                            }
                        ]
                    });
                },
                onApprove: async (data, actions, response) => {
                    const order = await actions.order.capture();
                    console.log(order);
                },
                onError: (error) => {
                    
                }
            }).render('#paypal-button');
        },
        getUserData() {
            this.loading = true;

            api.getAccountInfo()
                .then((result) => {
                    this.userData = result;
                }).catch((errors) => {
                    this.makeToast("Couldn't load user data from the server");
                }).finally(() => {
                    this.loading = false;
                });
        },
    },
    computed: {
        formState() {
            return (this.nameState == null || this.nameState) && 
                (this.streetAddressState == null || this.streetAddressState) && 
                (this.apartmentAddressState == null || this.apartmentAddressState) &&
                (this.cityState == null || this.cityState) &&
                (this.postcodeState == null || this.postcodeState);
        },
        nameState() {
            if (this.userData.name.length == 0) {
                return null;
            }
            return this.userData.name.length > 4;
        },
        streetAddressState() {
            if (this.userAddressInfo.streetAddress.length == 0) {
                return null;
            }
            return this.userAddressInfo.streetAddress.length > 1;
        },
        apartmentAddressState() {
            if (this.userAddressInfo.apartmentAddress.length == 0) {
                return null;
            }
            return this.userAddressInfo.apartmentAddress.length > 1;
        },
        cityState() {
            if (this.userAddressInfo.city.length == 0) {
                return null;
            }
            return this.userAddressInfo.city.length > 1;
        },
        postcodeState() {
            if (this.userAddressInfo.postcode.length == 0) {
                return null;
            }
            return this.userAddressInfo.postcode.length > 1;
        },

    },
    mounted() {
        this.$parent.setActive('store');

        const script = document.createElement("script");
        script.src = "https://www.paypal.com/sdk/js?client-id=AYprUXv1Y6tPemMNwfUeJ9IMUPVMxwgPm4deenB6Z55fX5Esd400KcglzIgibM_xUllG6UQIzjZ9E16z&currency=PLN";
        document.body.appendChild(script);
        script.addEventListener("load", this.setLoaded);
    }
}

</script>

<style scoped>

</style>
