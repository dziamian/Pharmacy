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
                <b-dropdown-item to="/">Painkillers</b-dropdown-item>
                <b-dropdown-item to="/">Antibiotic</b-dropdown-item>
                <b-dropdown-item to="/">Antiseptic</b-dropdown-item>
                <b-dropdown-item to="/">Bandage</b-dropdown-item>
                <b-dropdown-item to="/">Health</b-dropdown-item>
              </b-nav-item-dropdown>
              <b-nav-item to="/about" v-bind:class="{ active: isActive('about') }">About</b-nav-item>
              <b-nav-item to="/contact" v-bind:class="{ active: isActive('contact') }">Contact</b-nav-item>
            </b-navbar-nav>

          <div class="buttons">
            <b-button 
              v-if="!user" 
              v-b-modal.sign-in-modal 
              class="mr-1" 
              variant="outline-dark">
                <b-icon icon="person-circle"/>&nbsp;Log in
            </b-button>
            <b-button 
              v-else
              class="mr-1" 
              variant="outline-dark"
              @click="signOut">
                <b-icon icon="person-circle"/>&nbsp;Log out
          </b-button>
            <b-button v-if="user" variant="outline-dark" @click="navigateToCart">
              <b-icon icon="handbag-fill"/>
              <span class="bag-number">2</span>
            </b-button>
          </div>
        </b-navbar>
      </div>
    </div>
    <router-view/>
    <b-modal id="sign-in-modal" centered title="Log in" @ok="handleSignIn" ok-title="Log in">
        <form>
          <b-form-group
            label="Email"
            label-for="email-input"
            invalid-feedback="Email is required"
            :state="emailState">
              <b-form-input
                id="email-input"
                type="email"
                v-model="userCredentials.email"
                :state="emailState"
                required
              />
          </b-form-group>
          <b-form-group
            label="Password"
            label-for="password-input"
            invalid-feedback="Password is required"
            :state="passwordState">
              <b-form-input
                id="password-input"
                type="password"
                v-model="userCredentials.password"
                :state="passwordState"
                required
              />
        </b-form-group>
      </form>
    </b-modal>
  </div>
</template>

<script>
import api from '@/services/PharmacyApiService'

export default {
  name: 'app',
  data () {
    return {
      activeItem: 'home',
      userCredentials: {
        email: '',
        password: ''
      },
      emailState: null,
      passwordState: null
    }
  },
  computed: {
    user() {
      return this.$store.getters['user/isAuthenticated'];
    }
  },
  methods: {
    signUp() {
      this.$store.dispatch('user/signUp', this.userCredentials).then(() => {
        console.log("SUCCESSFULLY SIGNED UP.");
        api.test().then((data) => {
          console.log(data);
        }).catch(error => console.log(error));
      }).catch((error) => {
        console.log(error.message);
      });
    },
    signIn() {
      this.$store.dispatch('user/signIn', this.userCredentials).then(() => {
        console.log("SUCCESSFULLY SIGNED IN.");
        api.test().then((data) => {
          console.log(data);
        }).catch((error) => console.log(error));
      }).catch((error) => {
        console.log(error.message);
      });
    },
    signInWithGoogle() {
      this.$store.dispatch('user/signInWithGoogle').then(() => {
        console.log("SUCCESSFULLY SIGNED IN WITH GOOGLE.");
        api.test().then((data) => {
          console.log(data);
        }).catch((error) => console.log(error));
      }).catch((error) => {
        console.log(error.message);
      });
    },
    signOut() {
      this.$store.dispatch('user/signOut').then(() => {
        this.$router.push('');
      });
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
    },
    showSignInBox(){
      this.$bvModal.msgBoxOk('Data was submitted successfully', {
          title: 'Confirmation',
          size: 'sm',
          buttonSize: 'sm',
          okVariant: 'success',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        })
    },
    resetModal(){
      this.userCredentials.email = '';
      this.emailState = null;
      this.userCredentials.password = '';
      this.email = null;
    },
    handleSignIn(bvModalEvt) {
        bvModalEvt.preventDefault();
        this.handleSubmit();
    },
    handleSubmit() {
      this.signInWithGoogle();
      this.$nextTick(() => {
          this.$bvModal.hide('sign-in-modal');
      })
    },
    navigateToCart() {
      if (this.$route.name != 'Cart') {
        this.$router.push('cart');
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