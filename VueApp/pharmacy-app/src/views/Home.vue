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
              <b-button class="btn-shop px-5 py-3" variant="primary">
                Check new products
              </b-button>
            </div>
          </div>
        </div>
        <products-gallery :products="newProducts" priceLabel="zł">
        </products-gallery>
        <Footer></Footer>
      </div>
    </div>
  </main>
</template>

<script>
import ProductsGallery from '@/components/ProductsGallery'
import Footer from '@/components/Footer'

import api from '@/services/PharmacyApiService'

export default {
  components: {
    ProductsGallery,
    Footer
  },
  data() {
	  return {
      loading: true,
      newProducts: []
    }
  },
  async created() {
    this.getNewProducts();
  },
  methods: {
    async getNewProducts() {
      this.loading = true;

      try {
        this.newProducts = await api.getNewProducts();
        this.newProducts.forEach(product => {
          product.image = api._getBaseURL() + product.image;
        });
      } catch(e) {
        this.$parent.makeToast('Connection failed', 'No server response.', 'danger');
      } finally {
        this.loading = false;
      }
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
</style>