<template>
    <main>
        <b-row>
            <b-col class="col-sm-4 col-lg-6 ml-5 mt-5">
                <Map></Map>
            </b-col>
            <b-col class="col-sm-4 col-lg-6 ml-5 mt-5">

            </b-col>
        </b-row>
        <Footer/>
    </main>
</template>

<script>
import Map from "@/components/Map"
import Footer from "@/components/Footer"

export default {
    components:{
        Map,
        Footer  
    },
    data () {
        return {

        }
    },
    mounted () {
        this.$parent.setActive('contact');
    }
}
</script>

<style scoped>

</style>