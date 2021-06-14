<template>
    <b-container class="mt-5">
        <b-row class="mb-5">   
            <b-col class="md-6 mb-5 mb-md-0">
                <b-form @submit.stop.prevent="changeValidationState">
                    <h2 class="h3 mb-3 text-black">Shipping Details</h2>
                    <div class="p-3 p-lg-5 border">
                        <div class="form-group row">
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="name-input"
                                    invalid-feedback="Enter at least 5 characters.">
                                    <template v-slot:label>
                                        Name <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="name-input"
                                        type="text"
                                        v-model="userName"
                                        placeholder="Enter your name"
                                        :state="nameState"
                                        :disabled="validated"
                                        required/>
                                </b-form-group>
                            </b-col>
                        </div>
                
                        <div class="form-group row">
                            <b-col class="md-12">
                                <b-form-group
                                    label-for="streetAddress-input"
                                    invalid-feedback="Enter at least 2 characters.">
                                    <template v-slot:label>
                                        Address <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="streetAddress-input"
                                        type="text"
                                        v-model="userAddressInfo.streetAddress"
                                        placeholder="Street address"
                                        :state="streetAddressState"
                                        :disabled="validated"
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
                                        placeholder="Apartment, suite, unit etc. (optional)"
                                        :disabled="validated"/>
                                </b-form-group>
                            </b-col>
                        </div>

                        <div class="form-group row">
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="city-input"
                                    invalid-feedback="Enter at least 2 characters.">
                                    <template v-slot:label>
                                        City <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="city-input"
                                        type="text"
                                        v-model="userAddressInfo.city"
                                        placeholder="City name"
                                        :state="cityState"
                                        :disabled="validated"
                                        required/>
                                </b-form-group>
                            </b-col>
                            <b-col class="md-6">
                                <b-form-group
                                    label-for="postcode-input"
                                    invalid-feedback="Enter exactly 6 characters.">
                                    <template v-slot:label>
                                        Postcode <span class="text-danger font-weight-bold h5">*</span>
                                    </template>
                                    <b-form-input
                                        id="postcode-input"
                                        type="text"
                                        v-model="userAddressInfo.postcode"
                                        placeholder="Postcode number"
                                        :state="postcodeState"
                                        :disabled="validated"
                                        required/>
                                    </b-form-group>
                            </b-col>
                        </div>
                        
                        <div class="form-group">
                            <label for="billingDiffrentAddress" class="text-black" data-toggle="collapse"
                                href="#billingDiffrentAddress" role="button" aria-expanded="false"
                                aria-controls="billingDiffrentAddress">
                                <input v-b-toggle.billingDiffrentAddress @click="changeState" type="checkbox" id="billingDiffrentAddress"/>
                                Different Billing Details
                            </label>
                            <b-collapse id="billingDiffrentAddress">
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
                                                    placeholder="Street address"
                                                    :disabled="validated"
                                                    :state="alternativeStreetAddressState"/>
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
                                                    placeholder="Apartment, suite, unit etc. (optional)"
                                                    :disabled="validated"/>
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
                                                    placeholder="City name"
                                                    :disabled="validated"
                                                    :state="alternativeCityState"/>
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
                                                    placeholder="Postcode number"
                                                    :disabled="validated"
                                                    :state="alternativePostcodeState"/>
                                                </b-form-group>
                                        </b-col>
                                    </div>
                                </div>
                            </b-collapse>
                        </div>

                        <div class="form-group">
                            <b-form-group
                                label="Order notes"
                                label-for="orderNotes-input">
                                <b-form-textarea
                                    id="orderNotes-input"
                                    type="text"
                                    v-model="userAddressInfo.orderNotes"
                                    placeholder="Write your notes here..."
                                    rows="5"
                                    no-resize/>
                            </b-form-group>
                        </div>
                        <b-button type="submit">
                            Confirm Form
                        </b-button>
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
                            <div id="paypal-button" :hidden="!validated"></div>
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
            validated: false,
            userName: '',
            status: false,
            userAddressInfo: {
                streetAddress: '',
                apartmentAddress: '',
                city: '',
                postcode: '',
                orderNotes: '',
                alternativeStreetAddress: '',
                alternativeCity: '',
                alternativeApartmentAddress: '',
                alternativePostcode: ''
            },
        }
    },
    created() {
        this.getItemsFromCart();
        this.getUserName();
    },
    computed: {
        formState() {
            return ((this.nameState == null || this.nameState) && 
                (this.streetAddressState == null || this.streetAddressState) &&
                (this.cityState == null || this.cityState) &&
                (this.postcodeState == null || this.postcodeState)) 
                &&
                ((this.status && 
                this.alternativeStreetAddressState && this.alternativeCityState && this.alternativePostcodeState) || 
                (!this.status && 
                this.alternativeStreetAddressState == null && this.alternativeCityState == null && this.alternativePostcodeState == null));
        },
        nameState() {
            if (this.userName.length == 0) {
                return null;
            }
            return this.userName.length > 4;
        },
        streetAddressState() {
            if (this.userAddressInfo.streetAddress.length == 0) {
                return null;
            }
            return this.userAddressInfo.streetAddress.length > 1;
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
            return this.userAddressInfo.postcode.length == 6;
        },
        alternativeStreetAddressState() {
            if (this.userAddressInfo.alternativeStreetAddress.length == 0) {
                return null;
            }
            return this.userAddressInfo.alternativeStreetAddress.length > 1;
        },
        alternativeCityState() {
            if (this.userAddressInfo.alternativeCity.length == 0) {
                return null;
            }
            return this.userAddressInfo.alternativeCity.length > 1;
        },
        alternativePostcodeState() {
            if (this.userAddressInfo.alternativePostcode.length == 0) {
                return null;
            }
            return this.userAddressInfo.alternativePostcode.length == 6;
        }
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
                style: {
                    shape: 'rect',
                    color: 'blue',
                    layout: 'vertical',
                    label: 'pay'
                },
                fundingSource: window.paypal.FUNDING.PAYPAL,
                createOrder: (data, actions) => {
                    return actions.order.create({
                        purchase_units: [
                            {
                                amount: {
                                    value: this.getCost(this.getTotalCost(), '.')
                                }
                            }
                        ]
                    });
                },
                onApprove: async (data, actions, response) => {
                    const order = await actions.order.capture();
                    const orderObject = {
                        shippingAddress: {
                            streetAndBuildingNo: this.userAddressInfo.streetAddress,
                            city: this.userAddressInfo.city,
                            postalCode: this.userAddressInfo.postcode,
                            localNo: (this.userAddressInfo.apartmentAddress == '') ? null : this.userAddressInfo.apartmentAddress
                        },
                        billingAddress: this.status ? {
                            streetAndBuildingNo: this.userAddressInfo.alternativeStreetAddress,
                            city: this.userAddressInfo.alternativeCity,
                            postalCode: this.userAddressInfo.alternativePostcode,
                            localNo: (this.userAddressInfo.alternativeApartmentAddress == '') ? null : this.userAddressInfo.alternativeApartmentAddress
                        } : null,
                        recipientName: this.userName,
                        transactionId: order.id,
                        notes: (this.userAddressInfo.orderNotes == '') ? null : this.userAddressInfo.orderNotes
                    };
                    console.log(orderObject);
                    api.makeOrder(orderObject)
                        .then(() => {
                            this.$emit('cart-size-change');
                            this.$router.push({name: 'successfulOrder'});
                        })
                        .catch(() => {
                            this.makeToast("Order Error", "Payment was not made due to an error with order data.", "danger");
                        });
                },
                onError: (error) => {
                    this.makeToast("Payment Error", "Payment was not made due to an error with payment method.", "danger");
                }
            }).render('#paypal-button');
        },
        getUserName() {
            this.loading = true;

            api.getAccountInfo()
                .then((result) => {
                    this.userName = result.name;
                }).catch((errors) => {
                    this.makeToast("Couldn't load user data from the server");
                }).finally(() => {
                    this.loading = false;
                });
        },
        getAllCost() {
            var sum = 0;
            this.cart.forEach((element) => {
                sum += element.product.cost * element.amount;
            });
            return sum;
        },
        changeValidationState() {
            if(this.validated == false && this.formState) {
                this.validated = true;
            }
            else {
                this.validated = false;
            }
        },
        changeState() {
            this.status = !this.status;
        }
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
