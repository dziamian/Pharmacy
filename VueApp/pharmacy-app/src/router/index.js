import Vue from 'vue'
import VueRouter from 'vue-router'

import store from '@/store'

import Home from '@/views/Home.vue'
import Store from '@/views/Store.vue'
import Contact from '@/views/Contact.vue'
import SingleProduct from '@/views/SingleProduct.vue'
import Cart from '@/views/Cart.vue'
import PageNotFound from '@/views/PageNotFound.vue'
import Checkout from '@/views/Checkout.vue'
import SuccessfulOrder from '@/views/SuccessfulOrder.vue'
import SignIn from '@/views/SignIn.vue'
import SignUp from '@/views/SignUp.vue'
import Profile from '@/views/Profile.vue'


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
    path: '/checkout',
    name: 'checkout',
    component: Checkout,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/successfulOrder',
    name: 'successfulOrder',
    component: SuccessfulOrder,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/signIn',
    name: 'signIn',
    component: SignIn,
    meta: {
      requiresNonAuth: true
    }
  },
  {
    path: '/signUp',
    name: 'signUp',
    component: SignUp,
    meta: {
      requiresNonAuth: true
    }
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile,
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
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition;
    }
    return {
      x: 0, 
      y: 0
    };
  }
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (isAuthenticated()) {
      return next();
    }
    return next({
      name: 'signIn', 
    });
  }
  if (to.matched.some((record) => record.meta.requiresNonAuth)) {
    if (!isAuthenticated()) {
      return next();
    }
    return next(false);
  }
  next();
});

export default router