<template>
    <Loading v-if="loading==true"/>
    <b-container v-else>
        <b-container class="mt-5" width="100%">
            <b-row class="mb-5 justify-content-md-center">
                <form class="col-md-15" method="post">
                    <table id="table" class="table table-bordered">
                        <thead class="text-center">
                            <tr>
                                <th>No.</th>
                                <th>Products</th>
                                <th>Quantity</th>
                                <th>Time of use</th>
                                <th>Notes</th>
                            </tr>
                        </thead>
                        <tbody class="text-center">
                            <tr v-for="(element, index) in quantities" :key="index">
                                <td>{{index + 1}}</td>
                                <td>
                                    <select class="mdb-select md-form">
                                        <option disabled selected>Choose your drug</option>
                                        <option v-for="(product, index) in products" :key="index" to="/">
                                            {{product.name}}
                                        </option>
                                    </select>
                                </td>
                                <td width="220px !imporant">
                                    <b-input-group class="ml-0">
                                        <b-input-group-prepend>
                                            <b-button variant="info" v-model.number="element.value" @click="setQuantity(-1, index)">-</b-button>
                                        </b-input-group-prepend>

                                        <b-form-input
                                            v-model.number="element.value"
                                            type="number"
                                            placeholder="1"
                                            :min="minQuantity"
                                            :formatter="formatter"
                                        />
                            
                                        <b-input-group-append>
                                            <b-button variant="info" v-model.number="element.value" @click="setQuantity(1, index)">+</b-button>
                                        </b-input-group-append>
                                    </b-input-group>
                                </td>
                                <td>
                                    <select class="form-control" id="exampleFormControlSelect1">
                                        <option disabled selected>Choose time</option>
                                        <option>Before breakfast</option>
                                        <option>After breakfast</option>
                                        <option>Morning</option>
                                        <option>Before dinner</option>
                                        <option>After dinner</option>
                                        <option>Noon</option>
                                        <option>Before supper</option>
                                        <option>After supper</option>
                                        <option>Evening</option>
                                    </select>
                                </td>
                                <td>
                                    <textarea class="form-control" id="NoteText" rows="3"></textarea>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <b-row>
                        <b-col class="md-12">
                            <b-button size="lg" @click="print_table">Print</b-button>
                        </b-col>
                        <b-col class="md-12 text-right">
                            <b-button size="lg" @click="addArrayElement">Add</b-button>
                        </b-col>
                    </b-row>
                </form>
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
    name: 'planner',
    components: {
        Footer,
        Loading
    },
    data() {
        let minQuantity = 1;
        return {
            loading: false,
            minQuantity: minQuantity,
            products: [],
            quantities: [{value: 1}]
        };
    },
    created() {
        this.getAllProducts();
    },
    methods: {
        getAllProducts() {
            this.loading = true;
            api.getAllProducts()
                .then((result) => {
                    this.products = result;
                    this.products.forEach(product => {
                        product.image = api._getBaseURL() + product.image;
                    });
                    this.products.forEach(product => {
                        product.supply = 1;
                    });
                }).catch((errors) => {
                    this.products = [];
                }).finally(() => {
                    this.loading = false;
                });
        },
        print_table() {
            window.print();
        },
        addArrayElement() {
            this.quantities.push({value: 1});
        },
        setQuantity(change, index) {
            this.quantities[index].value += change;
            if (this.quantities[index].value - 1 < this.minQuantity) {
                this.quantities[index].value = this.minQuantity;
            }
        },
        formatter(value) {
            if (!value || value < this.minQuantity) {
                return this.minQuantity;
            }
            return value;
        },
        
    }
}
</script>

<style scoped>


td {
    vertical-align: middle !important;
    text-align: center !important;
}

.input-group{
    max-width: 160px;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
}

input[type=number] {
  -moz-appearance: textfield;
}

</style>