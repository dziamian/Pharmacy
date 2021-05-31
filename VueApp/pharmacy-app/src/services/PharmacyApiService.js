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
    addItemToCart(id, amount = '') {
        return axios.execute('get', '/api/cart/' + id + '/' + amount);
    },
    //TODO: remove this test
    test() {
        return axios.execute('get', '/api/profile/home');
    },
    _getBaseURL() {
        return axios.instance.defaults.baseURL;
    }
}