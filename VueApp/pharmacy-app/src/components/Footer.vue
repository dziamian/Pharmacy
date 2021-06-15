<template>
<b-container class="mt-5">
    <b-row>
        <b-col class="col-md-6 col-lg-3 mb-4 mb-lg-0">
            <div class="block-7">
                <h5 class="footer-heading mb-4">About Us</h5>
                <p>We are a company with many years of experience in the sale of pharmaceutical products, 
                    for which the customer's comfort is the most important.</p>
            </div>
        </b-col>

        <b-col class="col-lg-3 mx-auto mb-5 mb-lg-0 text-center">
            <h5 class="footer-heading mb-4">Quick Links</h5>
            <ul class="list-unstyled">
                <li><router-link to="/store">Painkillers</router-link></li>
                <li><router-link to="/store">Antibiotic</router-link></li>
                <li><router-link to="/store">Antiseptic</router-link></li>
                <li><router-link to="/store">Bandage</router-link></li>
                <li><router-link to="/store">Health</router-link></li>
            </ul>
        </b-col>

        <b-col class="col-md-6 col-lg-3 text-center">
            <div class="block-5 mb-5">
                <h5 class="footer-heading mb-4">Contact Info</h5>
                <ul class="list-unstyled">
                    <li class="address"> <b-icon icon="map-fill" color="#51eaea"></b-icon>  {{CONTACT.ADDRESS}}, {{CONTACT.COUNTRY}}</li>
                    <li class="phone"> <b-icon icon="telephone-fill" color="#51eaea"></b-icon> <a :href="'tel://' + CONTACT.PHONE">  {{getFormattedPhoneNumber(CONTACT.PHONE)}}</a></li>
                    <li class="email"> <b-icon icon="envelope-fill" color="#51eaea"></b-icon> <a :href="'mailto:' + CONTACT.EMAIL">  {{CONTACT.EMAIL}}</a></li>
                </ul>
            </div>
        </b-col>
    </b-row>
    <b-row class="pt-5 mt-5 text-center">
        <b-col class="col-md-12 mt-2 mb-5">
            <p>Copyright © 2021 | All rights reserved</p>
        </b-col>
    </b-row>
</b-container>
</template>

<script>

export default {

}
</script>

<style scoped>

a {
    color: #8c92a0;
    text-decoration: none;
    transition: .3s all ease;
}

a:hover{
    color: #51eaea;
}
</style>
