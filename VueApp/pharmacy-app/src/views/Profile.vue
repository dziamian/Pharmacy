<template>
    <b-container class="mt-5">
        <Footer/>
    </b-container>
</template>

<script>

import api from '@/services/PharmacyApiService'
import Footer from '@/components/Footer'

export default {
    components: {
        Footer 
    },
    data() {
        return {
            
        }
    },
    methods: { 

    },
    mounted() {
        this.$parent.setActive('');
    }
}

</script>

<style scoped>

</style>