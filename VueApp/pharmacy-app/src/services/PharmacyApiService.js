const axios = window.axios; 

export default {
    async execute(method, resource, data) {
        return axios({
            method,
            url: resource,
            data
        }).then(response => {
            return response.data;
        });
    },
    getAllProducts() {
        return this.execute('get', '/api/products');
    },
    getNewProducts() {
        return this.execute('get', '/api/products');
    },
    _getBaseURL() {
        return axios.defaults.baseURL;
    }
}