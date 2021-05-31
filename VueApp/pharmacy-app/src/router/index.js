import Vue from 'vue'
import VueRouter from 'vue-router'

import store from '@/store'

import Home from '@/views/Home.vue'
import Store from '@/views/Store.vue'
import About from '@/views/About.vue'
import Contact from '@/views/Contact.vue'
import SingleProduct from '@/views/SingleProduct.vue'
import Cart from '@/views/Cart.vue'
import PageNotFound from '@/views/PageNotFound.vue'

Vue.use(VueRouter);

const isAuthenticated = function() {
  return store.getters['user/isAuthenticated'];
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/store',
    name: 'Store',
    component: Store
  },
  {
    path: '/about',
    name: 'About',
    component: About
  },
  {
    path: '/contact',
    name: 'Contact',
    component: Contact
  },
  {
    path: '/store/product/:id',
    name: 'Product',
    component: SingleProduct,
    props: true
  },
  {
    path: '/cart',
    name: 'Cart',
    component: Cart,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '*',
    name: 'NotFound',
    component: PageNotFound
  }
]

const router = new VueRouter({
  routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (isAuthenticated()) {
      return next();
    }
    return next('/');
  }
  next();
});

export default router