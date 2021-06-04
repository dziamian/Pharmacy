<template>
<b-container class="mt-5">
    <b-row class="mb-5">   
        <b-col class="md-6 mb-5 mb-md-0">
            <h2 class="h3 mb-3 text-black">Billing Details</h2>
            <div class="p-3 p-lg-5 border">

                <div class="form-group">
                    <label for="country" class="text-black">Country <span class="text-danger">*</span></label>
                    <select id="country" class="form-control">
                        <option value="1">Select a country</option>
                        <option value="2">Poland</option>
                        <option value="3">Germany</option>
                        <option value="4">France</option>
                        <option value="5">Spain</option>
                        <option value="6">Russia</option>
                        <option value="7">The Czech Republic</option>
                        <option value="8">Slovakia</option>
                    </select>
                </div>

                <div class="form-group row">
                    <b-col class="md-6">
                        <label for="firstName" class="text-black">First Name <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="firstName" name="firstName">
                    </b-col>
                    <b-col class="md-6">
                        <label for="lastName" class="text-black">Last Name <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="lastName" name="lastName">
                    </b-col>
                </div>
        
                <div class="form-group row">
                    <b-col class="md-12">
                        <label for="companyName" class="text-black">Company Name </label>
                        <input type="text" class="form-control" id="companyName" name="companyName">
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
                        <label for="provinceCountry" class="text-black">Province / Country <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="provinceCountry" name="provinceCountry">
                    </b-col>
                    <b-col class="md-6">
                            <label for="postcode" class="text-black">Postcode<span class="text-danger">*</span></label>
                            <input type="text" class="form-control" id="postcode" name="postcode">
                    </b-col>
                </div>

                <div class="form-group row mb-5">
                    <b-col class="md-6">
                        <label for="emailAdress" class="text-black">Email Address <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="emailAdress" name="emailAdress">
                    </b-col>
                    <b-col class="md-6">
                        <label for="phoneNumber" class="text-black">Phone <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="phoneNumber" name="phoneNumber" placeholder="Phone Number">
                    </b-col>
                </div>

                <div class="form-group">
                    <label for="shipDiffrentAddress" class="text-black" data-toggle="collapse"
                        href="#shipDiffrentAddress" role="button" aria-expanded="false"
                        aria-controls="shipDiffrentAddress"><input v-b-toggle.shipDiffrentAddress type="checkbox" id="shipDiffrentAddress">
                        Ship To A Different Address?</label>
                    <b-collapse id="shipDiffrentAddress">
                        <div class="py-2">
                            <div class="form-group">
                                <label for="diffrentCountry" class="text-black">Country <span class="text-danger">*</span></label>
                                <select id="diffrentCountry" class="form-control">
                                    <option value="1">Select a country</option>
                                    <option value="2">Poland</option>
                                    <option value="3">Germany</option>
                                    <option value="4">France</option>
                                    <option value="5">Spain</option>
                                    <option value="6">Russia</option>
                                    <option value="7">The Czech Republic</option>
                                    <option value="8">Slovakia</option>
                                </select>
                            </div>
            
            
                            <div class="form-group row">
                                <b-col class="md-6">
                                    <label for="diffrentFirstName" class="text-black">First Name <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentFirstName" name="diffrentFirstName">
                                </b-col>
                                <b-col class="md-6">
                                    <label for="diffrentLastName" class="text-black">Last Name <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentLastName" name="diffrentLastName">
                                </b-col>
                            </div>
            
                            <div class="form-group row">
                                <b-col class="md-12">
                                    <label for="diffrentCompanyName" class="text-black">Company Name </label>
                                    <input type="text" class="form-control" id="diffrentCompanyName" name="diffrentCompanyName">
                                </b-col>
                            </div>
            
                            <div class="form-group row">
                                <b-col class="md-12">
                                    <label for="diffrentAddress" class="text-black">Address <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentAddress" name="diffrentAddress"
                                    placeholder="Street address">
                                </b-col>
                            </div>
            
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="Apartment, suite, unit etc. (optional)">
                            </div>
            
                            <div class="form-group row">
                                <b-col class="md-6">
                                    <label for="diffrentProvinceCountry" class="text-black">Province / Country<span
                                        class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentProvinceCountry" name="diffrentProvinceCountry">
                                </b-col>
                                <b-col class="md-6">
                                    <label for="diffrentPostalCode" class="text-black">Posta / Zip <span
                                        class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentPostalCode" name="diffrentPostalCode">
                                </b-col>
                            </div>
            
                            <div class="form-group row mb-5">
                                <b-col class="md-6">
                                    <label for="diffrentEmailAddress" class="text-black">Email Address <span
                                        class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentEmailAddress" name="diffrentEmailAddress">
                                </b-col>
                                <b-col class="md-6">
                                    <label for="diffrentPhoneNumber" class="text-black">Phone <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="diffrentPhoneNumber" name="diffrentPhoneNumber"
                                    placeholder="Phone Number">
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
                                    <td>{{getCost(element.product.cost*element.amount)}} zł</td>
                                </tr>
                                <tr>
                                    <td class="text-black font-weight-bold"><strong>Order Total</strong></td>
                                    <td class="text-black font-weight-bold"><strong>{{getCost(getTotalCost())}} zł</strong></td>
                                </tr>
                            </tbody>
                        </table>
                        <b-form-group label="Payment options" v-slot="{ paymentOption }">
                            <b-form-radio v-model="selectedPaymentOption" :aria-describedby="paymentOption" name="payPal" value="payPal">Paypal</b-form-radio>
                        </b-form-group>

                        <div class="form-group mt-5">
                            <button class="btn btn-primary btn-lg btn-block">Make order</button>
                        </div>
                    </div>
                </b-col>
            </b-row>
        </b-col>
    </b-row>
    <Footer/>
</b-container>
</template>

<script>
import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'

export default {
    components:{
        Footer,
    },
    data() {
        return {
            cart: [],
            selectedPaymentOption: ""
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
    }
}

</script>

<style scoped>

</style>
