<template>
    <div class="store-section">
        <div style="text-align: center;">
            <b-button v-b-toggle.collapse-2>FILTERS</b-button>
        </div>
        <b-collapse id="collapse-2" class="mt-2">
            <b-card>
                <div class="row">
                    <div class="col-lg-6">
                        <h3 class="label mb-3 h6 text-uppercase text-black d-block">Filter by Price</h3>
                        <vue-slider 
                            v-model="priceRange"
                            :min="0" 
                            :max="maxPrice" 
                            :enable-cross="false" 
                            :tooltip="'none'" 
                            :tooltip-placement="'bottom'"
                        />
                        <div class="p-2">
                            <b-input-group :append="priceLabel" style="float: left;">
                                <b-form-input 
                                    v-model="priceRange[0]"
                                    class="price-input"
                                    type="number"  
                                    placeholder="Min" 
                                    :min="0" 
                                    :max="maxPrice" 
                                    :formatter="formatter"
                                />
                            </b-input-group>
                            <b-input-group :append="priceLabel" style="float: right;">
                                <b-form-input 
                                    v-model="priceRange[1]"
                                    class="price-input"
                                    type="number" 
                                    placeholder="Max" 
                                    :min="0" 
                                    :max="maxPrice" 
                                    :formatter="formatter"
                                />
                            </b-input-group>
                        </div>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by Reference</h3>
                        <b-dropdown text="REFERENCE">
                            <b-dropdown-item v-for="reference in references" :key="reference.name" @click="reference.action">
                                {{reference.name}}
                            </b-dropdown-item>
                        </b-dropdown>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="categories-title h6 text-uppercase text-black d-block">Filter by Categories</h3>
                        <b-form-group>
                            <b-form-tags id="tags" v-model="activeTags" style="text-align: left;">
                                <template v-slot="{ tags, disabled, addTag, removeTag }">
                                    <ul v-if="tags.length > 0" class="list-inline d-inline-block mb-2">
                                        <li v-for="tag in tags" :key="tag" class="list-inline-item">
                                            <b-form-tag 
                                                @remove="removeTag(tag)" 
                                                :title="tag" 
                                                :disabled="disabled" 
                                                variant="info">
                                                    {{tag}}
                                            </b-form-tag>
                                        </li>
                                    </ul>

                                    <b-dropdown size="sm" block menu-class="w-100">
                                        <template #button-content>
                                            <b-icon icon="tag-fill"></b-icon>
                                            CATEGORIES
                                        </template>
                                        <b-dropdown-item-button v-for="option in availableOptions" :key="option" @click="onOptionClick({option, addTag})">
                                            {{option}}
                                        </b-dropdown-item-button>
                                        <b-dropdown-text v-if="availableOptions.length === 0">
                                            No more categories...
                                        </b-dropdown-text>
                                    </b-dropdown>
                                </template>
                            </b-form-tags>
                        </b-form-group>
                    </div>
                </div>
            </b-card>
        </b-collapse>
        <div class="container">
            <b-row>
                <b-col v-for="(product, index) in paginatedProducts" :key="index">
                    <product :product="product" :priceLabel="priceLabel" />
                </b-col>
            </b-row>
            <b-pagination
                v-model="currentPage"
                @change="onPageChanged"
                :total-rows="productRows"
                :per-page="perPage"
                align="center">
            </b-pagination>
        </div>
    </div>
</template>

<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'

import Product from '@/components/Product'

import api from '@/services/PharmacyApiService'

export default {
    components: {
        VueSlider,
        Product
    },
    data() {
        let maxPrice = 500;
        return {
            loading: true,
            maxPrice: maxPrice,
            priceRange: [0, maxPrice],
            priceLabel: 'zł',
            references: [{
                name: 'Name, A to Z',
                action: this.referenceFilter
            }, {
                name: 'Name, Z to A',
                action: this.referenceFilter
            }, {
                name: 'Price, Low to High',
                action: this.referenceFilter
            }, {
                name: 'Price, High to Low',
                action: this.referenceFilter
            }],
            activeTags: [],
            tags: ['Test1', 'Test2', 'Test3', 'Test4', 'Test5'],
            paginatedProducts: [],
            filteredProducts: [],
            products: [],
            perPage: 3,
            currentPage: 1,
            index: 0
        };
    },
    async created() {
        this.getAllProducts();
    },
    computed: {
        availableOptions() {
            return this.tags.filter(opt => this.activeTags.indexOf(opt) === -1);
        },
        productRows() {
            return this.products.length;
        }
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
                this.initPagination();
                this.loading = false;
            }
        },
        formatter(value) {
            if (value < this.min) {
                return this.min;
            }
            if (value > this.max) {
                return this.max;
            }
            return value;
        },
        initPagination(){
            this.filteredProducts = [...this.products];
            this.paginate(this.perPage, 0);
        },
        onOptionClick({option, addTag}) {
            addTag(option);
        },
        paginate(pageSize, pageNumber) {
            let productsToParse = this.filteredProducts;
            this.paginatedProducts = productsToParse.slice(
                pageNumber * pageSize,
                (pageNumber + 1) * pageSize
            );
        },
        onPageChanged(page) {
            this.paginate(this.perPage, page - 1);
        },
        referenceFilter() {

        }
    },
    mounted () {
        this.$parent.setActive('store');
    }
}
</script>

<style scoped>

#collapse-2 {
    width: 75%;
    margin: auto;
}

.categories-title {
    margin-bottom: 0.8rem !important;
}

img {
    width: 360px;
    height: 280px;
}

.label {
    text-align: center;
}

.input-group {
    width: 22%;
}

ul.list-inline {
    list-style-type: none;
}

li.list-inline-item {
    margin: 0 0 0 0;
}

.list-inline-item:not(:last-child) {
    margin-right: 0px;
}
</style>