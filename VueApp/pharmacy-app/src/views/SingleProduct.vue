<template>
    <Loading v-if="loading==true"/>
    <div v-else-if="product != null" class="site-wrap">
        <b-container class="mt-5"> 
            <b-row class="justify-content-md-center">
                <b-col class="col-sm-4 col-lg-5 text-center item mb-3">
                    <img v-bind:src="product.image" class="img-fluid borderless ">
                    <b-row>
                        <b-col>
                            <div id="rete" v-if="rateRender">
                                <AwesomeVueStarRating
                                  :star="this.star"
                                  :disabled="this.disabled"
                                  :maxstars="this.maxstars"
                                  :starsize="this.starsize"
                                  :hasresults="this.hasresults"
                                  :hasdescription="this.hasdescription"
                                  :ratingdescription="this.ratingdescription"
                                />
                            </div>
                        </b-col>
                    </b-row>
                </b-col>
                <b-col class="col-sm-4 col-lg-6 text-center">
                    <h2 class="text-black">{{product.name}}</h2>
                    <p>{{product.description}}</p>
                
                    <p><strong class="text-primary h4">{{getCost(product.cost)}} {{BILLING.CURRENCY.ABB}}</strong></p>
                    
                    <b-row class="ml-0 justify-content-center" v-if="product.supply > 0">
                        <b-input-group class="ml-0">
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
                        <b-col v-if="user" class="col-sm-4 col-lg-12 mt-5 mb-3">
                            <b-button variant="primary" size="lg" @click="addItem">Add to cart</b-button>
                        </b-col>
                    </b-row>
                    <b-col v-else>
                        <h2 class="text-black">This product is unavailable.</h2>
                    </b-col>
                    <b-col class="mt-4 ml-3">
                        <b-button :pressed.sync="choosedActive" class="mr-3" variant="outline-primary" @click="changeActive" >Active substances</b-button>
                        <b-button :pressed.sync="choosedPassive" class="mr-3" variant="outline-primary"  @click="changePassive" >Passive substances</b-button>
                    </b-col>
                    <b-col class="mt-4" v-if="whichSubstances == true">
                        <b-table :items="product.activeSubstances" :fields="[
                            {key: 'index', label: 'No.'}, 
                            {key: 'name', sortable: true}, 
                            {key: 'dose', sortable: true}]"
                            >
                            <template v-slot:cell(index)="data">
                                {{data.index + 1}}
                            </template>
                        </b-table>
                    </b-col>
                    <b-col class="mt-4" v-else>
                        <b-table :items="product.passiveSubstances" :fields="[{key: 'index', label: 'No.'},{key: 'name', sortable: true}]">
                            <template v-slot:cell(index)="data">
                                {{data.index + 1}}
                            </template>
                        </b-table>
                    </b-col>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <hr>
                </b-col>
            </b-row>
            <b-row v-if="substitutes.length > 0">
                <b-col class="text-center">
                    <h2 class="text-black">Substitutes:</h2>
                </b-col>
                <products-gallery :products="substitutes" :priceLabel="BILLING.CURRENCY.ABB"/>
            </b-row>
        </b-container>
        <Footer/>
    </div>
    <div v-else>
        Product not found.
    </div>
</template>

<script>
import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'
import Loading from '@/components/Loading'
import ProductsGallery from '@/components/ProductsGallery'
import AwesomeVueStarRating from "awesome-vue-star-rating";

export default {
    components: {
        ProductsGallery,
        Footer,
        AwesomeVueStarRating,
        Loading
    },
    name: 'SingleProduct',
    data() {
        let minQuantity = 1;
        return {
            whichSubstances: true,
            loading: true,
            choosedActive: false,
            choosedPassive: false,
            product: Object,
            substitutes: [],
            minQuantity: minQuantity,
            quantity: 1,
            star: 0,
            rateRender: true,
            ratingdescription: [
            {
                text: "Poor",
                class: "star-poor",
            },
            {
                text: "Below Average",
                class: "star-belowAverage",
            },
            {
                text: "Average",
                class: "star-average",
            },
            {
                text: "Good",
                class: "star-good",
            },
            {
                text: "Excellent",
                class: "star-excellent",
            },
            ],
            hasresults: true,
            hasdescription: true,
            starsize: "lg",
            maxstars: 5,
            disabled: false,
        }
    },
    created() {
        this.getProduct();
        this.getSubstitutes();
        this.getRatings();
    },
    methods: {
        getProduct() {
            this.loading = true;
            
            api.getProduct(this.id)
                .then((result) => {
                    this.product = result;
                    this.product.image = api._getBaseURL() + this.product.image;
                }).catch((errors) => {
                    this.$router.push({name: 'store'});
                }).finally(() => {
                    this.loading = false;
                });
        },
        forceRerender() {
            this.rateRender = false;

            this.$nextTick(() => {
                this.rateRender = true;
            });
        },
        getRatings(){
            this.loading = true;
            api.getAverageRatingsForProduct(this.id)
                .then((result) => {
                    this.star = result;

                }).catch((errors) => {
                    this.makeToast("Couldn't download ratings from server.");
                }).finally(() => {
                    this.loading = false;
                });
        },
        getSubstitutes() {
            this.loading = true;
            
            api.getSubstitutes(this.id)
                .then((result) => {
                    this.substitutes = result;
                    this.substitutes.forEach(substitute => {
                        substitute.image = api._getBaseURL() + substitute.image;
                    });
                }).catch((errors) => {
                    this.star = 0;
                }).finally(() => {
                    this.loading = false;
                });
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
        },
        addItem() {
            api.addItemToCart(this.product.id, this.quantity)
                .then(result => {
                    this.$emit('cart-size-change');
                    this.makeToast('Adding item', result, 'success');
                }).catch(error => {
                    this.makeToast('Could not add item', error, 'danger');
                });
        },
        changeActive() {
            this.whichSubstances = true;
            if(this.choosedPassive == true){
                this.choosedPassive = false;
            }
        },
        changePassive() {
            this.whichSubstances = false;
            if(this.choosedActive == true){
                this.choosedActive = false;
            }
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
            this.getSubstitutes();
            this.getRatings();
            this.star;
        },
        star: (x,y) =>{this.forceRerender();}
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