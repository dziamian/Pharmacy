import Vue from 'vue'

Vue.mixin({
    data() {
        return {
            CONTACT: {
                ADDRESS: "Warszawska 21, 25-521 Kielce",
                COUNTRY: "Polska",
                PHONE: "48123456789",
                EMAIL: "pharmacy@gmail.com"
            }
        };
    },
    methods: {
        getFormattedPhoneNumber(phone) {
            return '+' + 
                phone.substring(0, 2) + ' ' + 
                phone.substring(2, 5) + ' ' +
                phone.substring(5, 8) + ' ' +
                phone.substring(8, 11);
        },
        getCost(cost) {
            return parseInt(cost / 100) + "," + ((cost % 100 < 10) ? "0" : "") + (cost % 100);
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