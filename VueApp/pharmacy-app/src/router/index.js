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
    name: 'home',
    component: Home,
    props: true
  },
  {
    path: '/store',
    name: 'store',
    component: Store
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/contact',
    name: 'contact',
    component: Contact
  },
  {
    path: '/store/product/:id',
    name: 'product',
    component: SingleProduct,
    props: true
  },
  {
    path: '/cart',
    name: 'cart',
    component: Cart,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '*',
    name: 'notFound',
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
    return next({
      name: 'home', 
      params: {
        authRedirect: true
      }
    });
  }
  next();
});

export default router