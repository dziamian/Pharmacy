<template>
    <b-container class="mt-5">
        <b-row>
            <b-col class="md-12 text-center">
                <b-icon class="display-3" icon="check-circle-fill" variant="success"/>
                <h2 class="display-3 text-black">
                    Thank you!
                </h2>
                <p class="lead mb-5">
                    Your order was successfully completed.
                </p>
                <b-button class="btn-shop px-5 py-3" variant="primary" @click="manageClick">
                    Go back to store
                </b-button>
            </b-col>
        </b-row>
        <Footer/>
    </b-container>
</template>

<script>
import Footer from '@/components/Footer'

export default {
    components:{
        Footer
    },
    methods: {
        manageClick(){
            this.$router.push({name: 'store'});
        }
    }
}
</script>

<style scoped>

.lead {
    color: #8c92a0;
}

</style>