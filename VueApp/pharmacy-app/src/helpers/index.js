import Vue from 'vue'

Vue.mixin({
    methods: {
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