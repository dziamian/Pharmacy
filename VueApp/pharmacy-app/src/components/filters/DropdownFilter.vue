<!--TODO:-->
<template>
    <div>
        <h3 class="mb-3 h6 text-uppercase text-black d-block">Filter by Reference</h3>
        <b-dropdown text="REFERENCE">
            <b-dropdown-item>Name, A to Z</b-dropdown-item>
            <b-dropdown-item>Name, Z to A</b-dropdown-item>
            <b-dropdown-item>Price, Low to High</b-dropdown-item>
            <b-dropdown-item>Price, High to Low</b-dropdown-item>
        </b-dropdown>
    </div>
</template>

<script>
export default {
    name: 'DropdownFilter'
}
</script>
