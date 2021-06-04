import Vue from 'vue'

Vue.mixin({
    methods: {
        getCost(cost) {
            return parseInt(cost / 100) + "," + ((cost % 100 < 10) ? "0" : "") + (cost % 100);
        }
    }
});