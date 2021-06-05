import axios from '@/plugins/axios'

export default {
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
    //TODO: remove this test
    test() {
        return axios.execute('get', '/api/profile');
    },
    getContactInfo() {
        return axios.execute('get', '/api/contact');
    },
    _getBaseURL() {
        return axios.instance.defaults.baseURL;
    }
}