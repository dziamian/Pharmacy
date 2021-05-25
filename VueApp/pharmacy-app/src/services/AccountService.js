import axios from '@/plugins/axios'

export default {
    login(credentials) {
        return axios.execute('post', '/api/auth/login', credentials);
    }
}