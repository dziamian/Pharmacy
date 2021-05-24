<template>
<div v-if="loading == true"> <!-- Dodaj animacje do ładowania-->

</div>
<div v-else-if="product != ''" class="site-wrap">
    <div class="site-section">
        <div class="container mt-5"> 
            <b-row class="justify-content-md-center">
                <b-col class="col-sm-4 col-lg-5 text-center item mb-4">
                    <div class="border text-center">
                        <img v-bind:src="product.image" class="img-fluid p-5">
                    </div>
                </b-col>
                <b-col class="col-sm-4 col-lg-5 text-center">
                    <h2 class="text-black">{{product.name}}</h2>
                    <p>{{product.description}}</p>
                
                    <p><strong class="text-primary h4">{{getCost(product.cost)}} zł</strong></p>
                    
                    <div v-if="product.supply > 0">
                        <b-input-group class="mt-3">
                            <b-input-group-prepend>
                                <b-button variant="info" v-model.number="quantity" @click="setQuantity(-1)">-</b-button>
                            </b-input-group-prepend>

                            <b-form-input
                                v-model.number="quantity"
                                type="number"
                                placeholder="1"
                                :min="minQuantity"
                                :max="product.supply"
                                :formatter="formatter"
                            />
                            
                            <b-input-group-append>
                                <b-button variant="info" v-model.number="quantity" @click="setQuantity(1)">+</b-button>
                            </b-input-group-append>
                        </b-input-group>
                        <b-col class="col-sm-4 col-lg-12 mt-4">
                            <b-button variant="primary" size="lg">Add to cart</b-button>
                        </b-col>
                    </div>
                    <div v-else>
                        Unavailable <!-- TODO: style -->
                    </div>
                </b-col>
            </b-row>
        </div>
        <Footer></Footer>
    </div>
</div>
</template>

<script>
import api from '@/services/PharmacyApiService'

import Footer from '@/components/Footer'

export default {
    components:{
        Footer
    },
    name: 'SingleProduct',
    data() {
        let minQuantity = 1;
        return {
            loading: true,
            product: Object,
            minQuantity: minQuantity,
            quantity: 1
        }
    },
    async created() {
        this.getProduct();
    },
    methods: {
        async getProduct() {
            this.loading = true;

            try {
                this.product = await api.getProduct(this.id);
                this.product.image = api._getBaseURL() + this.product.image;
            } finally {
                this.loading = false;
            }
        },
        getCost(cost) {
            return parseInt(cost / 100) + "," + ((cost % 100 < 10) ? "0" : "") + (cost % 100);
        },
        setQuantity(change){
            this.quantity += change;
            if (this.quantity - 1 < this.minQuantity) {
                this.quantity = this.minQuantity;
            }
            else if (this.quantity + 1 > this.product.supply) {
                this.quantity = this.product.supply;
            }
        },
        formatter(value) {
            if (!value || value < this.minQuantity) {
                return this.minQuantity;
            }
            if (value > this.product.supply) {
                return this.product.supply;
            }
            return value;
        }
    },
    mounted () {
        this.$parent.setActive('store');
    },
    props: {
        id: {
            type: String,
            required: true
        }
    },
    watch: {
        id() {
            this.getProduct();
        }
    }
}
</script>

<style scoped>

.input-group{
    max-width: 130px;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
}

input[type=number] {
  -moz-appearance: textfield;
}


</style>