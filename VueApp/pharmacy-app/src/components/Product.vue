<template>
    <div style="margin:5px; padding:5px">
        <span v-if="product.supply <= 0" class="tag">UNAVAILABLE</span>
        <router-link :to="'/store/product/' + product.id">
            <img v-bind:src="product.image"/>
            <h3 class="text-dark">{{product.name}}</h3>
            <p class="price">{{getCost(product.cost)}} {{priceLabel}}</p>
        </router-link>
    </div>
</template>

<script>
export default {
    name: 'Product',
    props: {
        product: Object,
        priceLabel: String
    }
}
</script>



<style scoped>
.tag {
    position: absolute;
    background: #f50202;
    color: #fff;
    letter-spacing: .2em;
    padding: 7px 20px;
    font-size: .8rem;
    font-weight: 900;
    top: -80px;
    left: -20px;
    text-transform: uppercase;
    z-index: 2;
    -webkit-transform: rotate(-90deg) translateX(-100%) translateY(-100%);
    -ms-transform: rotate(-90deg) translateX(-100%) translateY(-100%);
    transform: rotate(-90deg) translateX(-100%) translateY(-100%);
}

img {
    width: 280px;
    height: 240px;
}

a {
    color: #000;
    text-decoration: none;
}

</style>