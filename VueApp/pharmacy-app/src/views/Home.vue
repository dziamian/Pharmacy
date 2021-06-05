<template>
  <main class="pb-3">
    <div class="home-cover">
      <div class="container">
        <div class="row">
          <div class="col-lg-7 mx-auto order-lg-2 align-self-center">
            <div class="home-cover-content text-center">
              <h2>
                Take care of your health every day!
              </h2>
              <h1>
                Welcome to our Pharmacy
              </h1>
              <b-button class="btn-shop px-5 py-3" variant="primary" v-scroll-to="'#newProducts'">
                Check new products
              </b-button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="home-section new-products">
      <div id="newProducts" class="container pt-5">
        <div class="row">
          <div class="title-section text-center col-12">
            <h2 class="text-uppercase">
              New products
            </h2>
          </div>
        </div>
        <div class="row">
          <products-gallery v-if="newProducts.length > 0" :products="newProducts" :priceLabel="BILLING.CURRENCY.ABB"/>
        </div>
      </div>
    </div>
    <Footer></Footer>
  </main>
</template>

<script>
import api from '@/services/PharmacyApiService'

import ProductsGallery from '@/components/ProductsGallery'
import Footer from '@/components/Footer'

export default {
  components: {
    ProductsGallery,
    Footer
  },
  data() {
	  return {
      loading: true,
      authRedirected: this.$route.params.authRedirect,
      newProducts: []
    }
  },
  created() {
    this.getNewProducts();
  },
  methods: {
    getNewProducts() {
      this.loading = true;
      
      api.getNewProducts()
        .then((result) => {
          this.newProducts = result;
          this.newProducts.forEach(product => {
            product.image = api._getBaseURL() + product.image;
          });
        }).catch((errors) => {
          this.newProducts = [];
          this.makeToast('Connection failed', 'No server response. Please refresh the page.', 'danger');
        }).finally(() => {
          this.loading = false;
        });
    },
    isAuthRedirected() {
      if (this.authRedirected) {
        this.$parent.showSignIn();
      }
    }
  },
  mounted () {
    this.$parent.setActive('home');
    this.isAuthRedirected();
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