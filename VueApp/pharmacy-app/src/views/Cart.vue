<template>
    <Loading v-if="loading==true"/>
    <b-container v-else>
        <b-container v-if="cart.length != 0" class="mt-5" width="100%">
            <b-row class="mb-5 justify-content-md-center">
                <form class="col-md-9" method="post">
                    <table class="table table-bordered">
                        <thead class="text-center">
                            <tr>
                                <th>Image</th>
                                <th>Product</th>
                                <th>Price</th>
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
                                <td width="130px !important"><h2 class="h5 text-black">{{getCost(element.product.cost)}} {{BILLING.CURRENCY.ABB}}</h2></td>
                                <td width="130px !important"><h2 class="h5 text-black">{{getCost(element.product.cost*element.amount)}} {{BILLING.CURRENCY.ABB}}</h2></td>
                                <td><b-button @click="removeItem(index)">X</b-button></td>
                            </tr>
                        </tbody>
                    </table>
                </form>
            
                <b-row class="mt-5">
                    <b-col class="col-lg-5">
                        <button class="btn btn-outline-primary btn-md btn-block"><router-link to="/store">Continue Shopping</router-link></button>
                    </b-col>
                    <b-col>
                        <b-row class="justify-content-end">
                            <b-col class="col-md-7">
                                <b-row>
                                    <b-col class="col-md-12 text-center border-bottom mb-2">
                                        <h3 class="text-black h4 text-uppercase">Total</h3>
                                    </b-col>
                                </b-row>
                                <b-row class="mb-2">
                                    <b-col class="md-6 mb-4 text-center">
                                        <strong class="text-black">{{getCost(getTotalCost())}} {{BILLING.CURRENCY.ABB}}</strong>
                                    </b-col>
                        
                                    <b-row>
                                        <b-col class="md-12 mb-5">
                                            <button class="btn btn-primary btn-lg btn-block" @click="manageSubmit">Proceed To
                                            Checkout</button>
                                            </b-col>
                                    </b-row>
                                </b-row>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-row>
            <Footer/>
        </b-container>
        <b-container class="mt-5" v-else>
            <b-row class="justify-content-md-center mb-5">
                <p class="h3 mb-2">
                    <b-icon icon="exclamation-circle-fill" font-scale="1" variant="info"/>
                    You have no items in cart.
                </p>
            </b-row>
            <Footer/>
        </b-container>
    </b-container>
</template>

<script>
import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'
import Loading from '@/components/Loading'

export default {
    components:{
        Footer,
        Loading
    },
    data() {
        let minQuantity = 1;
        return {
            cart: [],
            products: [],
            minQuantity: minQuantity,
            loading: true
        };
    },
    created() {
        this.getAllProducts();
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
        getAllProducts() {
            this.loading = true;

            api.getAllProducts()
                .then((result) => {
                    this.products = result;
                }).catch((errors) => {
                    this.products = [];
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
        removeItem(index) {
            const productId = this.cart[index].product.id;
            api.removeItemFromCart(productId)
                .then((result) => {
                    this.$emit('cart-size-change');
                    this.cart.splice(index, 1);
                }).catch(error => {
                    this.makeToast('Could not remove item', error, 'error');
                });
        },
        manageSubmit(){
            this.$router.push({name: 'checkout'});
        }
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

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}

input[type=number] {
    -moz-appearance: textfield;
}

a {
    text-decoration: none;
    transition: .3s all ease;
}

</style>