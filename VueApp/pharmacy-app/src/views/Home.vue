<template>
  <Loading v-if="loading==true"/>
  <main v-else class="pb-3">
    <div class="home-cover">
      <b-container>
        <b-row>
          <b-col class="col-lg-7 mx-auto order-lg-2 align-self-center">
            <div class="home-cover-content text-center">
              <h2>
                Take care of your health every day!
              </h2>
              <h1>
                Welcome to our Pharmacy
              </h1>
              <b-button 
                class="btn-shop px-5 py-3" 
                variant="primary" v-scroll-to="{el: '#newProducts', offset: appHeight}">
                Check newest products
              </b-button>
            </div>
          </b-col>
        </b-row>
      </b-container>
    </div>
    <div id="newProducts" class="home-section new-products">
      <b-container class="pt-5">
        <b-row>
          <b-col class="title-section text-center col-12">
            <h2 class="text-uppercase">
              Newest products
            </h2>
          </b-col>
        </b-row>
        <b-row>
          <products-gallery v-if="newestProducts.length > 0" :products="newestProducts" :priceLabel="BILLING.CURRENCY.ABB"/>
        </b-row>
      </b-container>
    </div>
    <Footer/>
  </main>
</template>

<script>
import api from '@/services/PharmacyApiService'

import ProductsGallery from '@/components/ProductsGallery'
import Footer from '@/components/Footer'
import Loading from '@/components/Loading'

export default {
  components: {
    ProductsGallery,
    Footer,
    Loading
  },
  data() {
	  return {
      loading: true,
      authRedirected: this.$route.params.authRedirect,
      newestProducts: [],
      amountOfNewestProducts: 5
    }
  },
  created() {
    this.getNewProducts();
  },
  computed: {
    appHeight() {
      return -this.$parent.height;
    }
  },
  methods: {
    getNewProducts() {
      this.loading = true;
      
      api.getNewestProducts(this.amountOfNewestProducts)
        .then((result) => {
          this.newestProducts = result;
          this.newestProducts.forEach(product => {
            product.image = api._getBaseURL() + product.image;
          });
        }).catch((errors) => {
          this.newestProducts = [];
          this.makeToast('Connection failed', 'No server response. Please refresh the page.', 'danger');
        }).finally(() => {
          this.loading = false;
        });
    }
  },
  mounted () {
    this.$parent.setActive('home');
  }
}
</script>

<style scoped>
.home-cover, .home-cover > .container > .row {
  min-height: 700px;
  height: calc(100vh);
}

.home-cover {
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
  background-image: url('~@/assets/images/home.jpg');
}

.home-cover h2 {
  font-size: 14px;
  font-weight: normal;
  color: #fff;
  letter-spacing: .2em;
  text-transform: uppercase;
  text-shadow: 2px 2px 5px #000;
}

.home-cover h1 {
  font-size: 70px;
  font-weight: 900;
  color: #fff;
  margin-bottom: 30px;
  text-transform: uppercase;
  text-shadow: 2px 2px 5px #000;
}

.btn-shop {
  transition: .3s all ease-in-out;
  text-transform: uppercase;
  background-color: #51eaea;
  border-color: #51eaea;
  border-width: 2px;
  color: #000;
}

.btn-shop:hover {
  background: transparent;
  color: #51eaea;
}

.new-products {
  background-color: #f8f9fa;
}
</style>