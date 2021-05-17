<template>
  <div id="app">
    <div class="header py-2">
      <div class="container">
        <b-navbar class="navbar d-flex align-items-center justify-content-between">
          
          <b-navbar-brand class="logo" to="/">Pharmacy</b-navbar-brand>
          
            <b-navbar-nav>
              <b-nav-item to="/" v-bind:class="{ active: isActive('home') }">Home</b-nav-item>
              <b-nav-item to="/store" v-bind:class="{ active: isActive('store') }">Store</b-nav-item>
              <b-nav-item-dropdown text="Products">
                <b-dropdown-item to="/">Supplements</b-dropdown-item>
                <b-dropdown-item to="/">Vitamins</b-dropdown-item>
                <b-dropdown-item to="/">Diet &amp; Nutrition</b-dropdown-item>
                <b-dropdown-item to="/">Tea &amp; Coffee</b-dropdown-item>
              </b-nav-item-dropdown>
              <b-nav-item to="/about" v-bind:class="{ active: isActive('about') }">About</b-nav-item>
              <b-nav-item to="/contact" v-bind:class="{ active: isActive('contact') }">Contact</b-nav-item>
            </b-navbar-nav>

          <div class="buttons">
            <b-button class="mr-1" variant="outline-dark">
              <b-icon icon="search"></b-icon>&nbsp;Search
            </b-button>
            <b-button variant="outline-dark">
              <b-icon icon="handbag-fill"></b-icon>
              <span class="bag-number">2</span>
            </b-button>
          </div>
        </b-navbar>
      </div>
    </div>
    <router-view>
    </router-view>
  </div>
</template>

<script>
export default {
  name: 'app',
  data () {
    return {
      user: null,
      activeItem: 'home'
    }
  },
  methods: {
    login () {

    },
    async logout () {

    },
    isActive (menuItem) {
      return this.activeItem == menuItem;
    },
    setActive (menuItem) {
      this.activeItem = menuItem;
    },
    makeToast(title, description, variant, autoHideDelay = 5000) {
        this.$bvToast.toast(description, {
            title: title,
            variant: variant,
            autoHideDelay: autoHideDelay,
            appendToast: false,
            toaster: "b-toaster-bottom-right"
        });
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

#app {
  font-family: "Rubik";
}

.header {
  position: sticky;
  top: 0;
  background-color: #fff;
  z-index: 10000; /* TODO: set right value... */
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