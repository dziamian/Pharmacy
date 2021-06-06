import Vue from 'vue'

import api from '@/services/PharmacyApiService'

export default {
    async getContactInfo() {
        const contact = await api.getContactInfo();
        Object.keys(contact).forEach((key) => {
            contact[key.toUpperCase()] = contact[key];
            delete contact[key];
        });
        return contact;
    },
    initMixin(contact = {ADDRESS: "", COUNTRY: "", PHONE: "", EMAIL: ""}) {
        Vue.mixin({
            data() {
                return {
                    CONTACT: contact,
                    BILLING: {
                        CURRENCY: {
                            NAME: 'złoty',
                            SYMBOL: 'zł',
                            ABB: 'PLN'
                        }
                    }
                };
            },
            computed: {
                user() {
                    return this.$store.getters['user/isAuthenticated'];
                }
            },
            methods: {
                getFormattedDate(date) {
                    return date.getUTCFullYear() + '-'
                        + ("0" + (date.getUTCMonth() + 1)).slice(-2) + '-'
                        + ("0" + date.getUTCDate()).slice(-2);
                },
                getFormattedPhoneNumber(phone) {
                    return '+' + 
                        phone.substring(0, 2) + ' ' + 
                        phone.substring(2, 5) + ' ' +
                        phone.substring(5, 8) + ' ' +
                        phone.substring(8, 11);
                },
                getCost(cost, delimeter = ',') {
                    return parseInt(cost / 100) + delimeter + ((cost % 100 < 10) ? "0" : "") + (cost % 100);
                },
                makeToast(title, description, variant, autoHideDelay = 5000) {
                    this.$bvToast.toast(description, {
                        title: title,
                        variant: variant,
                        autoHideDelay: autoHideDelay,
                        appendToast: false,
                        toaster: "b-toaster-bottom-right"
                    });
                },
            }
        });
    }
}