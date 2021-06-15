<template>
  <div id="app">
    <div class="header py-2" ref="header">
      <div class="container">
        <b-navbar class="navbar d-flex align-items-center justify-content-between">
          
          <b-navbar-brand class="logo" to="/">Pharmacy</b-navbar-brand>
          
            <b-navbar-nav>
              <b-nav-item to="/" v-bind:class="{ active: isActive('home') }">Home</b-nav-item>
              <b-nav-item to="/store" v-bind:class="{ active: isActive('store') }">Store</b-nav-item>
              <b-nav-item to="/contact" v-bind:class="{ active: isActive('contact') }">Contact</b-nav-item>
            </b-navbar-nav>

          <div class="buttons">
            <b-button
              v-if="!user" 
              class="mr-2" 
              variant="outline-dark"
              @click="navigateToSignInPage">
                <b-icon icon="person-circle"/>&nbsp;Log in
            </b-button>
            <b-button
              v-if="!user" 
              class="mr-1" 
              variant="outline-dark"
              @click="navigateToSignUpPage">
                <b-icon icon="person-plus-fill"/>&nbsp;Sign Up
            </b-button>
            <b-button 
              v-else
              class="mr-1" 
              variant="outline-dark"
              @click="signOut">
                <b-icon icon="door-open"/>&nbsp;Log out
          </b-button>
          <b-button 
              v-if="user"
              class="mr-1" 
              variant="outline-dark"
              @click="navigateToProfilePage">
                <b-icon icon="person-square"/>&nbsp;Profile
          </b-button>
          <b-button 
              v-if="user"
              class="mr-1" 
              variant="outline-dark"
              @click="navigateToPlannerPage">
                <b-icon icon="journal-bookmark-fill"/>&nbsp;Planner
          </b-button>
            <b-button v-if="user" variant="outline-dark" @click="navigateToCart">
              <b-icon icon="basket2"/>
              <span class="bag-number">{{cartSize}}</span>
            </b-button>
          </div>
        </b-navbar>
      </div>
    </div>
    <router-view @cart-size-change="setCartAmount"/>
  </div>
</template>

<script>
import api from '@/services/PharmacyApiService'

export default {
  name: 'app',
  data () {
    return {
      activeItem: 'home',
      categories: [],
      cartSize: 0
    }
  },
  created() {
    this.setCartAmount();
  },
  computed: {
    height() {
      return this.$refs.header.clientHeight;
    }
  },
  methods: {
    signOut() {
      this.$store.dispatch('user/signOut').then(() => {
        if (this.$route.name != 'home') {
          this.$router.push({name: 'home'});
        }
      });
    },
    setCartAmount() {
      if (!this.user) {
        return;
      }
      api.getCartAmount()
        .then((result) => {
          this.cartSize = result;
        });
    },
    isActive (menuItem) {
      return this.activeItem == menuItem;
    },
    setActive (menuItem) {
      this.activeItem = menuItem;
    },
    navigateToCart() {
      if (this.$route.name != 'cart') {
        this.$router.push({name: 'cart'});
      }
    },
    navigateToSignInPage() {
      if (this.$route.name != 'signIn') {
        this.$router.push({name: 'signIn'});
      }
    },
    navigateToSignUpPage() {
      if (this.$route.name != 'signUp') {
        this.$router.push({name: 'signUp'});
      }
    },
    navigateToProfilePage() {
      if (this.$route.name != 'profile') {
        this.$router.push({name: 'profile'});
      }
    },
    navigateToPlannerPage() {
      if (this.$route.name != 'planner') {
        this.$router.push({name: 'planner'});
      }
    }
  }
}


</script>

<style>
html {
  overflow-y: scroll;
}

body {
  margin: 0;
  line-height: 1.7 !important;
}

body.modal-open {
  height: 100vh;
  overflow: hidden;
  padding-right: 0px !important;
}

#app {
  font-family: "Rubik";
}

.header {
  position: sticky;
  top: 0;
  background-color: #fff;
  z-index: 100;
}

.navbar {
  padding-left: 0 !important;
  padding-right: 0 !important;
  background-color: #fff;
}

.navbar-light .navbar-nav .nav-link {
    color: rgba(0, 0, 0, 1) !important;
}

.navbar-expand .navbar-nav .nav-link {
    padding: 0.7rem 1rem !important;
}

.navbar-light .navbar-nav .nav-link:hover {
    color: rgba(0, 0, 0, 0.35) !important;
}

li > a {
  text-transform: uppercase;
  letter-spacing: .05em;
}

ul.dropdown-menu {
  border-top: 2px solid #007f80;
}

ul.dropdown-menu {
  margin-top: 0.4rem;
}

li > a.dropdown-item {
  text-transform: none;
  letter-spacing: normal;
}

li.active > a {
  color: #000;
  position: relative;
}

li.active > a::before {
  content: "";
  position: absolute;
  left: 16px;
  right: 16px;
  height: 2px;
  background: #000;
  bottom: 0; 
}

.logo {
  text-transform: uppercase;
  letter-spacing: .2em;
  font-size: 22px !important;
  color: #000;
  font-weight: 900;
}

.bag-number {
  position: absolute;
  top: 5px;
  right: -8px;
  width: 20px;
  height: 20px;
  border: 1px solid black;
  border-radius: 50%;
  line-height: 20px;
  color: #000;
  font-size: 12px;
  background: #51eaea;
  text-align: center;
}

</style>