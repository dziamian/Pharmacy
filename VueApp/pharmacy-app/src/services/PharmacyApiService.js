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
    test(access_token) {
        return axios.instance.get('http://localhost:5000/api/profile/home', { headers: {
            'Authorization': `Bearer ${access_token}`
        }})
        //return axios.execute('get', '/api/profile/home');
    },
    _getBaseURL() {
        return axios.instance.defaults.baseURL;
    }
}