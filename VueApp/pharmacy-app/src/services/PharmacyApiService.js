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
    getSubstitutes(id) {
        return axios.execute('get', '/api/products/' + id + '/substitutes');
    },
    getCategories() {
        return axios.execute('get', '/api/categories');
    },
    getItemsFromCart() {
        return axios.execute('get', '/api/cart');
    },
    getCartAmount() {
        return axios.execute('get', '/api/cart/size');
    },
    addItemToCart(id, amount = 1) {
        return axios.execute('put', '/api/cart/', {productId: id, amount: amount});
    },
    removeItemFromCart(id) {
        return axios.execute('delete', '/api/cart/remove/' + id);
    },
    removeCart() {
        return axios.execute('delete', '/api/cart');
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