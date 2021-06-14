import axios from '@/plugins/axios'

const isReachable = require('is-reachable');

export default {
    createAccount(accountInfo) {
        return axios.execute('post', '/api/accounts', accountInfo);
    },
    getAccountInfo() {
        return axios.execute('get', '/api/accounts');
    },
    updateUserInfo(accountInfo) {
        return axios.execute('patch', '/api/accounts', accountInfo);
    },
    getAllProducts() {
        return axios.execute('get', '/api/products');
    },
    getNewestProducts(amount) {
        return axios.execute('get', '/api/products/newest?count=' + amount);
    },
    getProduct(id) {
        return axios.execute('get', '/api/products/' + id);
    },
    getUserOrders() {
        return axios.execute('get', '/api/orders');
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
    createRating() {
        return axios.execute('post', '/api/ratings');
    },
    getAllRatingsForProduct(id) {
        return axios.execute('get', '/api/products/'+ id + '/ratings/all');
    },
    getAverageRatingsForProduct(id) {
        return axios.execute('get', '/api/products/'+ id + '/ratings/average');
    },
    getRating(id) {
        return axios.execute('get', '/api/ratings/'+ id);
    },
    getAllRating() {
        return axios.execute('get', '/api/ratings');
    },
    removeRating(id) {
        return axios.execute('delete', '/api/ratings/remove/' + id);
    },
    makeOrder(details) {
        return axios.execute('post', '/api/orders', details);
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