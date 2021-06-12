<template>
    <Loading v-if="loading==true"/>
    <b-container v-else>
        <b-container class="mt-5" width="100%">
            <b-row class="mb-5 justify-content-md-center">
                <form class="col-md-9" method="post">
                    <table class="table table-bordered">
                        <thead class="text-center">
                            <tr>
                                <th>No.</th>
                                <th>Products</th>
                                <th>Quantity</th>
                                <th>time of use</th>
                            </tr>
                        </thead>
                        <tbody class="text-center">
                            <tr>
                                <td>1</td>
                                <td>Apap<br>
                                </td>
                                <td>1</td>
                                <td>after dinner</td>
                            </tr>
                        </tbody>
                    </table>
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
            loading: false
        };
    }
}
</script>

<style scoped>

td{
    vertical-align: middle !important;
    text-align: center !important;
}

</style>