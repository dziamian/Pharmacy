<template>
    <gmap-map
        ref="map"
        :center="coordinates"
        :zoom="15"
        style="width:640px; height:360px;"
    >
        <gmap-marker :position="coordinates" :clickable="false"/>
    </gmap-map>  
</template>

<script>

export default {
    data() {
        return {
            coordinates: {
                lat: 50.87539942130235,
                lng: 20.634027133381906
            }
        };
    },
}
</script>
