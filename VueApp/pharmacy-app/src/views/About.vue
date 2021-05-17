<template>
    <main>
        This is about page.
    </main>
</template>

<script>

export default {
    data () {
        return {

        }
    },
    mounted () {
        this.$parent.setActive('about');
    }
}
</script>

<style scoped>

</style>