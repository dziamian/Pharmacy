<!--TODO:-->
<template>
    <div>
        <h3 class="label mb-3 h6 text-uppercase text-black d-block">{{label}}</h3>
        <vue-slider 
            :value="value"
            @change="$emit('input', $event)"
            :min="min" 
            :max="max" 
            :enable-cross="false" 
            :tooltip="'none'" 
            :tooltip-placement="'bottom'"
        />
        <div class="p-2">
            <b-input-group v-bind:append="buttonsLabel" style="float: left;">
                <b-form-input 
                    :value="value[0]"
                    @input="$event = filter($event); emitter($event, [$event, value[1]])"
                    class="price-input" 
                    type="number"  
                    placeholder="Min" 
                    :min="min" 
                    :max="max" 
                    :formatter="formatter"
                />
            </b-input-group>
            <b-input-group v-bind:append="buttonsLabel" style="float: right;">
                <b-form-input 
                    :value="value[1]"
                    @input="$event = filter($event); emitter($event, [value[0], $event])"
                    class="price-input" 
                    type="number" 
                    placeholder="Max" 
                    :min="min" 
                    :max="max" 
                    :formatter="formatter"
                />
            </b-input-group>
        </div>
    </div>
</template>

<script>
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/default.css'

export default {
    name: 'RangeFilter',
    components: {
        VueSlider
    },
    methods: {
        filter(event) {
            return this.parser(event);
        },
        emitter(event, value) {
            if (event == null) {
                return;
            }
            this.$emit('input', value);
        },
        parser(value) {
            var newValue = parseInt(value);
            if (isNaN(newValue)) {
                return null;
            }
            return newValue;
        },
        formatter(value) {
            if (value < this.min) {
                return this.min;
            }
            if (value > this.max) {
                return this.max;
            }
            return value;
        }
    },
    props: {
        min: Number,
        max: Number,
        value: Array,
        label: String,
        buttonsLabel: String,
    }
}
</script>

<style scoped>
.label {
    text-align: center;
}

.input-group {
    width: 22%;
}
</style>