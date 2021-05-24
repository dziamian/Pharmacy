import Vue from 'vue'
import VueRouter from 'vue-router'

import Home from '@/views/Home.vue'
import Store from '@/views/Store.vue'
import About from '@/views/About.vue'
import Contact from '@/views/Contact.vue'
import SingleProduct from '@/views/SingleProduct.vue'

Vue.use(VueRouter);

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
  }
]

const router = new VueRouter({
  routes
});

export default router