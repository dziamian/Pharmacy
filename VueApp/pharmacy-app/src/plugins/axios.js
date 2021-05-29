"use strict";

import Vue from 'vue';
import axios from "axios";
import store from '@/store'

let config = {
  baseURL: "http://localhost:5000",
  json: true
};

const _axios = axios.create(config);

_axios.interceptors.request.use(
  async function(config) {
    const authToken = await store.getters['user/authToken'];
    console.log(authToken);
    if (authToken) {
      config.headers.Authorization = `Bearer ${authToken}`;
    }
    return config;
  },
  function(error) {
    return Promise.reject(error);
  }
);

_axios.interceptors.response.use(
  function(response) {
    return response;
  },
  function(error) {
    return Promise.reject(error);
  }
);

Plugin.install = function(Vue, options) {
  Vue.axios = _axios;
  window.axios = _axios;
  Object.defineProperties(Vue.prototype, {
    axios: {
      get() {
        return _axios;
      }
    },
    $axios: {
      get() {
        return _axios;
      }
    },
  });
};

Vue.use(Plugin);

export default {
  instance: _axios,
  execute: async function(method, resource, data) {
    return _axios({
      method,
      url: resource,
      data
    }).then(response => {
      return response.data;
    });
  }
};