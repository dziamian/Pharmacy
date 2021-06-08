import axios from '@/plugins/axios'

const isReachable = require('is-reachable');

export default {
    createAccount(accountInfo) {
        return axios.execute('post', '/api/accounts', accountInfo);
    },
    getAccountInfo() {
        return axios.execute('get', '/api/accounts');
    },
    getAllProducts() {
        return axios.execute('get', '/api/products');
    },
    getNewProducts() {
        return axios.execute('get', '/api/products');
    },
    getProduct(id) {
        return axios.execute('get', '/api/products/' + id);
    },
    getItemsFromCart() {
        return axios.execute('get', '/api/cart');
    },
    getCartAmount() {
        return axios.execute('get', '/api/cart/size');
    },
    addItemToCart(id, amount = '') {
        return axios.execute('get', '/api/cart/' + id + '/' + amount);
    },
    removeItemFromCart(id) {
        return axios.execute('get', '/api/cart/remove/' + id);
    },
    validateCart() {
        return axios.execute('get', '/api/cart/validate');
    },
    getContactInfo() {
        return axios.execute('get', '/api/contact');
    },
    _getBaseURL() {
        return axios.instance.defaults.baseURL;
    },
    _isReachable() {
        return isReachable(this._getBaseURL(), {timeout: 1000});
    }
}