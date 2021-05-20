<template>
    <div class="col-sm-4 col-lg-4 text-center item mb-4" style="margin:5px; padding:5px">
        <span v-if="product.supply > 0" class="tag">UNAVAILABLE</span>
        <img v-bind:src="product.image"/> <!-- TODO: link-->
        <h3 class="text-dark">{{product.name}}</h3>
        <p class="price">{{getCost(product.cost)}} {{priceLabel}}</p>
    </div>
</template>

<script>
export default {
    name: 'Product',
    props: {
        product: Object,
        priceLabel: String
    },
    methods: {
        getCost(cost) {
            return parseInt(cost / 100) + "," + ((cost % 100 < 10) ? "0" : "") + (cost % 100);
        }
    }
}
</script>



<style scoped>
.tag {
    position: absolute;
    background: #e86b00;
    color: #fff;
    letter-spacing: .2em;
    padding: 7px 20px;
    font-size: .8rem;
    font-weight: 900;
    top: 0;
    left: 0;
    text-transform: uppercase;
    z-index: 2;
    -webkit-transform: rotate(-90deg) translateX(-100%) translateY(-70%);
    -ms-transform: rotate(-90deg) translateX(-100%) translateY(-70%);
    transform: rotate(-90deg) translateX(-100%) translateY(-70%);
}

img {
    width: 280px;
    height: 240px;
}

</style>