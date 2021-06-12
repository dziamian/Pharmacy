<template>
    <b-container class="mt-5">
        <b-row class="mb-5">   
            <b-col class="md-6 mb-5 mb-md-0">
                <h2 class="h3 mb-3 text-black">Billing Details</h2>
                <div class="p-3 p-lg-5 border">
                    <div class="form-group row">
                        <b-col class="md-6">
                            <label for="firstName" class="text-black">Name <span class="text-danger">*</span></label>
                            <input type="text" class="form-control" id="firstName" name="firstName">
                        </b-col>
                    </div>
            
                    <div class="form-group row">
                        <b-col class="md-12">
                            <label for="address" class="text-black">Address <span class="text-danger">*</span></label>
                            <input type="text" class="form-control" id="address" name="address" placeholder="Street address">
                        </b-col>
                    </div>
            
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Apartment, suite, unit etc.">
                    </div>

                    <div class="form-group row">
                        <b-col class="md-6">
                            <label for="city" class="text-black">City <span class="text-danger">*</span></label>
                            <input type="text" class="form-control" id="city" name="city">
                        </b-col>
                        <b-col class="md-6">
                                <label for="postcode" class="text-black">Postcode<span class="text-danger">*</span></label>
                                <input type="text" class="form-control" id="postcode" name="postcode">
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
                                        <label for="diffrentAddress" class="text-black">Address <span class="text-danger">*</span></label>
                                        <input type="text" class="form-control" id="diffrentAddress" name="diffrentAddress"
                                        placeholder="Street address">
                                    </b-col>
                                </div>
                
                                <div class="form-group">
                                    <input type="text" class="form-control" placeholder="Apartment, suite, unit etc.">
                                </div>
                
                                <div class="form-group row">
                                    <b-col class="md-6">
                                        <label for="diffrentCity" class="text-black">City<span
                                            class="text-danger">*</span></label>
                                        <input type="text" class="form-control" id="diffrentCity" name="diffrentCity">
                                    </b-col>
                                    <b-col class="md-6">
                                        <label for="diffrentPostalCode" class="text-black">Postcode<span
                                            class="text-danger">*</span></label>
                                        <input type="text" class="form-control" id="diffrentPostalCode" name="diffrentPostalCode">
                                    </b-col>
                                </div>
                            </div>
                        </b-collapse>
                    </div>

                    <div class="form-group">
                        <label for="orderNotes" class="text-black">Order Notes</label>
                        <textarea name="orderNotes" id="orderNotes" cols="30" rows="5" class="form-control"
                            placeholder="Write your notes here..."></textarea>
                    </div>
                </div>
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
            selectedPaymentOption: "paypal",
            loading: true
        }
    },
    created() {
        this.getItemsFromCart();
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
