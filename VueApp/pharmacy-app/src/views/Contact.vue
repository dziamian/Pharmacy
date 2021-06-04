<template>
    <b-container>
        <b-row class="mb-5">
            <b-col  class="ml-5 mt-5">
                <h2 class="h3 mb-3 text-black">Address:</h2>
                <h2 class="h5 mb-4">{{CONTACT.ADDRESS}}</h2>

                <h2 class="h3 mb-3 text-black">Office:</h2>
                <h2 class="h5 mb-2">Phone number: <a :href="'tel://' + CONTACT.PHONE">{{getFormattedPhoneNumber(CONTACT.PHONE)}}</a></h2>
                <h2 class="h5 mb-4">E-mail:  <a :href="'mailto:' + CONTACT.EMAIL">{{CONTACT.EMAIL}}</a></h2>

                <h2 class="h3 mb-3 text-black">Opening hours:</h2>
                <h2 class="h5 mb-2 text-black">From 9:00 a.m. to 20:00 p.m.</h2>
                <h2 class="h5 mb-4">From Monday to Saturday</h2>

            </b-col>
            <b-col class="col-sm-4 col-lg-6 mt-5">
                <Map></Map>
            </b-col>
        </b-row>
        <Footer/>
    </b-container>
</template>

<script>
import Map from "@/components/Map"
import Footer from "@/components/Footer"

export default {
    components:{
        Map,
        Footer  
    },
    mounted () {
        this.$parent.setActive('contact');
    }
}
</script>

<style scoped>

</style>