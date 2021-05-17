<template>
    <div class="store-section">
        <div style="text-align: center;">
            <b-button v-b-toggle.collapse-2>FILTERS</b-button>
        </div>
        <b-collapse id="collapse-2" class="mt-2" style="width: 75%; margin: auto;">
            <b-card>
                <div class="row">
                    <div class="col-lg-6">
                        <h3 class="mb-3 h6 text-uppercase text-black d-block" style="text-align: center;">Filter by Price</h3>
                        <vue-slider v-model="value" :min="0" :max="500" :enable-cross="false" :tooltip="'none'" :tooltip-placement="'bottom'"></vue-slider>
                        <div class="p-2">
                            <b-input-group append="zł" style="float: left;">
                                <b-form-input v-model="value[0]" class="price-input" type="number" placeholder="Min" v-bind:min="minValue" v-bind:max="maxValue" :formatter="formatter"></b-form-input>
                            </b-input-group>
                            <b-input-group append="zł" style="float: right;">
                                <b-form-input v-model="value[1]" class="price-input" type="number" placeholder="Max" v-bind:min="minValue" v-bind:max="maxValue" :formatter="formatter"></b-form-input>
                            </b-input-group>
                        </div>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by Reference</h3>
                        <b-dropdown text="REFERENCE">
                            <b-dropdown-item>Name, A to Z</b-dropdown-item>
                            <b-dropdown-item>Name, Z to A</b-dropdown-item>
                            <b-dropdown-item>Price, Low to High</b-dropdown-item>
                            <b-dropdown-item>Price, High to Low</b-dropdown-item>
                        </b-dropdown>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="categories-title h6 text-uppercase text-black d-block">Filter by Categories</h3>
                        <tagged-input buttonTitle="CATEGORIES" v-bind:options="tags">
                        </tagged-input>
                    </div>
                </div>
            </b-card>
        </b-collapse>
        <div class="container">
            <div class="row" margin="15px">
                <div class="col-sm-6 col-lg-4 text-center item mb-4">
                    <img src="@/assets/images/example.png"/>
                    <h3 class="text-dark">Dices</h3>
                    <p class="price">$55.00</p>
                </div>
                <div class="col-sm-6 col-lg-4 text-center item mb-4">
                    <img src="@/assets/images/example.png"/>
                    <h3 class="text-dark">Dices</h3>
                    <p class="price">$55.00</p>
                </div>
                <div class="col-sm-6 col-lg-4 text-center item mb-4">
                    <img src="@/assets/images/example.png"/>
                    <h3 class="text-dark">Dices</h3>
                    <p class="price">$55.00</p>
                </div>
            </div>
            <div class="row justify-content-center">
                <nav aria-label="page selector">
                    <ul class="pagination">
                        <li class="page-item disabled">
                            <a class="page-link" href="#" tabindex="-1" aria-disabled="true">
                            <span aria-hidden="true">&laquo;</span>
                            </a>
                        </li>
                        <li class="page-item active" aria-current="page"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">3</a></li>
                        <li class="page-item">
                            <a class="page-link" href="#" aria-label="Next">
                                <span aria-hidden="true">&raquo;</span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>
</template>

<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'

import TaggedInput from '@/components/TaggedInput'

import api from '@/services/PharmacyApiService'

export default {
    components: {
        VueSlider,
        TaggedInput
    },
    data() {
        return {
            loading: true,
            minValue: 0,
            maxValue: 500,
            value: [75, 300],
            tags: ['Test1', 'Test2', 'Test3', 'Test4', 'Test5'],
            products: []
        };
    },
    async created() {
        this.getAllProducts();   
    },
    methods: {
        async getAllProducts() {
            this.loading = true;

            try {
                this.products = await api.getAllProducts();
                this.products.forEach(product => {
                    product.image = api._getBaseURL() + product.image;
                });
            } finally {
                this.loading = false;
            }
        },
        formatter(value) {
            if (value < this.minValue) {
                return this.minValue;
            } else if (value > this.maxValue) {
                return this.maxValue;
            } else {
                return value;
            }
        }
    },
    mounted () {
        this.$parent.setActive('store');
    }
}
</script>

<style scoped>

.input-group {
    width: 22%;
}

.categories-title {
    margin-bottom: 0.8rem !important;
}

img{
    width: 200px;
    height: 200px;
}
</style>