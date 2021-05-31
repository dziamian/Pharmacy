<template>
<b-container class="mt-5" width="100%">
    <b-row class="mb-5 justify-content-md-center">
        <form class="col-md-9" method="post">
            <table class="table table-bordered">
                <thead class="text-center">
                    <tr>
                        <th>Image</th>
                        <th>Product</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Total</th>
                        <th>Remove</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(element, index) in cart" :key="index">
                        <td >
                            <img v-bind:src="element.product.image" class="img-fluid">
                        </td>
                        <td>
                            <h2 class="h5 text-black">{{element.product.name}}</h2>
                        </td>
                        <td width="100px !important"><h2 class="h5 text-black">{{getCost(element.product.cost)}} zł</h2></td>
                        <td width="170px !important">
                            <b-input-group>
                                <b-input-group-prepend>
                                    <b-button variant="info" v-model.number="element.amount" @click="setAmount(-1)">-</b-button>
                                </b-input-group-prepend>

                                <b-form-input
                                    v-model.number="element.amount"
                                    type="number"
                                    placeholder="element.amount"
                                    :min="minQuantity"
                                    :max="element.product.supply"
                                    :formatter="formatter"
                                />
                                
                                <b-input-group-append>
                                    <b-button variant="info" v-model.number="element.amount" @click="setAmount(1)">+</b-button>
                                </b-input-group-append>
                            </b-input-group>
                        </td>
                        <td width="100px !important"><h2 class="h5 text-black">{{getCost(element.product.cost*element.amount)}} zł</h2></td>
                        <td><b-button>X</b-button></td>
                    </tr>
                </tbody>
            </table>
        </form>
    
        <div class="row">
            <div class="col-md-6">
                <div class="row mb-5">
                    <div class="col-md-6 mb-3 mb-md-0">
                        <button class="btn btn-primary btn-md btn-block">Update Cart</button>
                    </div>
                    <div class="col-md-6">
                        <button class="btn btn-outline-primary btn-md btn-block">Continue Shopping</button>
                    </div>
                </div>
            </div>
            <div class="col-md-6 pl-5">
                <div class="row justify-content-end">
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-12 text-right border-bottom mb-5">
                                <h3 class="text-black h4 text-uppercase">Cart Totals</h3>
                            </div>
                        </div>
                        <div class="row mb-5">
                            <div class="col-md-6">
                                <span class="text-black">Total</span>
                            </div>
                                <div class="col-md-6 text-right">
                                    <strong class="text-black">{{getCost(getTotalCost())}}zł</strong>
                                </div>
                
                            <div class="row">
                                <div class="col-md-12">
                                    <button class="btn btn-primary btn-lg btn-block" onclick="window.location='checkout.html'">Proceed To
                                    Checkout</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </b-row>
</b-container>
</template>

<script>
import api from '@/services/PharmacyApiService'

export default {
    data() {
        let minQuantity = 1;
        return {
            cart: [],
            minQuantity: minQuantity
        };
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
                    })
                }).catch((errors) => {
                    this.cart = null;
                }).finally(() => {
                    this.loading = false;
                });
        },
        getTotalCost(){
            var sum = 0;
            this.cart.forEach((element) =>{
                sum += element.product.cost * element.amount;
            });
            return sum;
        }/*
        setAmount(change){
            this.cart.amount += change;
            if (this.cart.amount - 1 < this.minQuantity) {
                this.cart.amount = this.minQuantity;
            }
            else if (this.cart.amount + 1 > this.element.product.supply) {
                this.cart.amount = this.element.product.supply;
            }
        },
        formatter(value) {
            if (!value || value < this.minQuantity) {
                return this.minQuantity;
            }
            if (value > this.cart.product.supply) {
                return this.cart.product.supply;
            }
            return value;
        }*/
    },
    mounted () {
        this.$parent.setActive('store');
    }
}
</script>

<style scoped>

td{
    vertical-align: middle !important;
    text-align: center !important;
}
</style>