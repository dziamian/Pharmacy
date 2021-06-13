<template>
    <Loading v-if="loading==true"/>
    <div v-else class="store-section">
        <b-container class="mt-5">
            <b-row class="ml-3 justify-content-md-center">
                <b-col class="col-sm-4 col-lg-5">
                    <b-input-group class="ml-0">
                        <b-input-group-prepend is-text>
                            <b-icon icon="search"/>
                        </b-input-group-prepend>
                        <vue-bootstrap-typeahead
                            :data="filter.products"
                            v-model="filter.settings.query"
                            :serializer="p => p.name"
                            placeholder="Search for product..."
                        />
                        <b-input-group-append>
                            <b-button v-b-toggle.collapse-2>FILTERS</b-button>
                        </b-input-group-append>
                    </b-input-group>
                </b-col>
            </b-row>
        </b-container>
        <b-collapse id="collapse-2" class="filters mt-2">
            <b-card>
                <div class="row">
                    <div class="col-lg-6">
                        <h3 class="label mb-3 h6 text-uppercase text-black d-block">Filter by Price</h3>
                        <vue-slider 
                            v-model="filter.settings.priceRange"
                            :min="init.minPrice" 
                            :max="init.maxPrice" 
                            :enable-cross="false" 
                            :tooltip="'none'" 
                            :tooltip-placement="'bottom'"
                        />
                        <div class="p-2">
                            <b-input-group class="price-input-group" :append="BILLING.CURRENCY.ABB" style="float: left;">
                                <b-form-input 
                                    v-model.number="filter.settings.priceRange[0]"
                                    type="number"  
                                    placeholder="Min" 
                                    :min="init.minPrice" 
                                    :max="init.maxPrice" 
                                    :formatter="formatter"
                                />
                            </b-input-group>
                            <b-input-group class="price-input-group" :append="BILLING.CURRENCY.ABB" style="float: right;">
                                <b-form-input 
                                    v-model.number="filter.settings.priceRange[1]"
                                    type="number" 
                                    placeholder="Max" 
                                    :min="init.minPrice" 
                                    :max="init.maxPrice" 
                                    :formatter="formatter"
                                />
                            </b-input-group>
                        </div>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by Reference</h3>
                        <b-dropdown text="REFERENCE">
                            <template v-for="(reference, index) in params.references">
                                <b-dropdown-item :active="index == filter.settings.reference" :key="index" @click="filter.settings.reference = index">
                                    {{reference.name}}
                                </b-dropdown-item>
                                <b-dropdown-divider v-if="reference.divider" :key="'0'+index" />
                            </template>
                        </b-dropdown>
                    </div>
                    <div class="col-lg-3" style="text-align: center;">
                        <h3 class="categories-title h6 text-uppercase text-black d-block">Filter by Categories</h3>
                        <b-form-group>
                            <b-form-tags id="tags" v-model="filter.settings.tags" style="text-align: left;">
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
        <b-pagination
            class='mt-5'
            v-model="pagination.settings.currentPage"
            @change="onPageChanged"
            :total-rows="productRows"
            :per-page="pagination.settings.perPage"
            align="center">
        </b-pagination>
        <div class="container mt-5">
            <b-row v-if="pagination.products.length > 0">
                <b-col class="col-sm-4 col-lg-4 text-center item mb-4" v-for="(product, index) in pagination.products"  :key="index">
                    <product :product="product" :priceLabel="BILLING.CURRENCY.ABB" />
                </b-col>
            </b-row>
            <b-row v-else class="justify-content-md-center">
                <p class="h3 mb-2">
                    <b-icon icon="exclamation-circle-fill" font-scale="1" variant="info"/>
                    We have found no items.
                </p>
            </b-row>
            <b-pagination
                v-model="pagination.settings.currentPage"
                @change="onPageChanged"
                :total-rows="productRows"
                :per-page="pagination.settings.perPage"
                align="center">
            </b-pagination>
        </div>
        <Footer/>
    </div>
</template>

<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'
import VueBootstrapTypeahead from 'vue-bootstrap-typeahead'

import Loading from '@/components/Loading'

import Product from '@/components/Product'

import Footer from '@/components/Footer'

import api from '@/services/PharmacyApiService'

export default {
    components: {
        VueSlider,
        VueBootstrapTypeahead,
        Product,
        Footer,
        Loading
    },
    data() {
        let minPrice = 0;
        let maxPrice = 500;
        return {
            loading: true,
            init: {
                minPrice: minPrice,
                maxPrice: maxPrice
            },
            params: {
                products: [],
                tags: [],
                references: [{
                    name: 'Name, A to Z',
                    condition: (p1, p2) => (p1.name > p2.name) ? 1 : -1
                }, { 
                    name: 'Name, Z to A',
                    condition: (p1, p2) => (p1.name < p2.name) ? 1 : -1,
                    divider: true
                }, {
                    name: 'Price, Low to High',
                    condition: (p1, p2) => (p1.cost > p2.cost) ? 1 : -1
                }, {
                    name: 'Price, High to Low',
                    condition: (p1, p2) => (p1.cost < p2.cost) ? 1 : -1
                }]
            },
            filter: {
                products: [],
                settings: {
                    priceRange: [0, maxPrice],
                    tags: [],
                    reference: 0,
                    query: ''
                }
            },
            pagination: {
                products: [],
                settings: {
                    perPage: 9,
                    currentPage: 1
                }
            }
        };
    },
    created() {
        this.getCategories();
        this.getAllProducts();
    },
    computed: {
        availableOptions() {
            return this.params.tags.filter(opt => this.filter.settings.tags.indexOf(opt) === -1);
        },
        productRows() {
            return this.filter.products.length;
        }
    },
    methods: {
        getAllProducts() {
            this.loading = true;

            api.getAllProducts()
                .then((result) => {
                    this.params.products = result;
                    this.params.products.forEach(product => {
                        product.image = api._getBaseURL() + product.image;
                    });
                }).catch((errors) => {
                    this.params.products = [];
                }).finally(() => {
                    this.initPagination();
                    this.loading = false;
                });
        },
        getCategories() {
            this.$parent.categories.forEach((category) => {
                this.params.tags.push(category.name);
            });
        },
        formatter(value) {
            if (!value || value < this.init.minPrice) {
                return this.init.minPrice;
            }
            if (value > this.init.maxPrice) {
                return this.init.maxPrice;
            }
            return value;
        },
        initPagination(){
            this.filter.products = this.params.products;
            this.paginate(this.pagination.settings.perPage, 0);
        },
        onOptionClick({option, addTag}) {
            addTag(option);
        },
        paginate(pageSize, pageNumber) {
            let productsToParse = this.filter.products;
            this.pagination.products = productsToParse.slice(
                pageNumber * pageSize,
                (pageNumber + 1) * pageSize
            );
        },
        onPageChanged(page) {
            this.paginate(this.pagination.settings.perPage, page - 1);
        },
        filterProducts(settings) {
            this.filter.products = this.params.products.filter((product) => {
                return settings.priceRange[0] * 100 <= product.cost 
                    && settings.priceRange[1] * 100 >= product.cost
                    && product.name.toLowerCase().includes(settings.query.toLowerCase());
            });

            this.filter.products.sort(this.params.references[settings.reference].condition);

            let currentPage = 1;
            this.pagination.settings.currentPage = currentPage;
            this.paginate(this.pagination.settings.perPage, currentPage - 1);
        }
    },
    mounted () {
        this.$parent.setActive('store');
    },
    watch: {
        'filter.settings': {
            handler(settings) {
                this.filterProducts(settings);
            },
            deep: true
        }
    }
}
</script>

<style scoped>


.filters {
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

.price-input-group {
    width: 25%;
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

.price-input-group {
    min-width: 112px;
}
</style>