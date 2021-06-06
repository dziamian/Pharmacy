<template>
    <b-container class="mt-5">
        <b-row>
            <b-col class="text-center">
                <h2 class="h3 mb-3 text-black">We're sorry!</h2>
            </b-col>
        </b-row>
        <b-row class="text-center">
            <b-col>
                <h2 class="h3 mb-3 text-black">Page you have requested does not exist.</h2>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
export default {
    data() {
        return {

        };
    },
    mounted() {
        this.$parent.setActive('');
    }
}
</script>

<style scoped>

</style>