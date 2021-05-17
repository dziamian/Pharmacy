<template>
    <div class="store-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by price</h3>
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
                <div class="col-lg-3">
                    <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by Reference</h3>
                    <b-dropdown class="refrencesButton" text="REFERENCE">
                        <b-dropdown-item>Name, A to Z</b-dropdown-item>
                        <b-dropdown-item>Name, Z to A</b-dropdown-item>
                        <b-dropdown-item>Price, Low to High</b-dropdown-item>
                        <b-dropdown-item>Price, High to Low</b-dropdown-item>
                    </b-dropdown>
                </div>
                <div class="col-lg-4">
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'

import api from '@/services/PharmacyApiService'

export default {
    components: {
        VueSlider
    },
    data() {
        return {
            loading: true,
            minValue: 0,
            maxValue: 500,
            value: [75, 300],
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

</style>