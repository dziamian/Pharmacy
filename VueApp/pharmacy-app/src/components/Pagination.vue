<!--TODO:-->
<template>
    <div>
        <b-pagination
        v-model="currentPageCopy"
        @input="$emit('currentPageChange', $event)"
        :total-rows="rows"
        :per-page="perPage"
        aria-controls="productsList">
        </b-pagination>
    </div>
</template>

<script>

export default {
    name: "Pagination",
    data() {
        return {
            currentPageCopy: this.value
        }
    },
    props: {
        rows: Number,
        perPage: Number,
        value: Number
    },
    watch: {
        value(v) {
            this.currentPageCopy = v;
        }
    }
}

</script>

