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
    _getBaseURL() {
        return axios.instance.defaults.baseURL;
    }
}