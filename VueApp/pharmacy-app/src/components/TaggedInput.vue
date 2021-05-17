<template>
    <div>
        <b-form-group>
            <b-form-tags id="tags" v-model="values" style="text-align: left;">
                <template v-slot="{ tags, disabled, addTag, removeTag }">
                    <ul v-if="tags.length > 0" class="list-inline d-inline-block mb-2">
                        <li v-for="tag in tags" :key="tag" class="list-inline-item">
                            <b-form-tag 
                                @remove="removeTag(tag)" 
                                :title="tag" 
                                :disabled="disabled" 
                                variant="info">
                                    {{tag}}
                            </b-form-tag>
                        </li>
                    </ul>

                    <b-dropdown size="sm" block menu-class="w-100">
                        <template #button-content>
                            <b-icon icon="tag-fill"></b-icon>
                            {{buttonTitle}}
                        </template>
                        <b-dropdown-item-button v-for="option in availableOptions" :key="option" @click="onOptionClick({option, addTag})">
                            {{option}}
                        </b-dropdown-item-button>
                        <b-dropdown-text v-if="availableOptions.length === 0">
                            No more categories...
                        </b-dropdown-text>
                    </b-dropdown>
                </template>
            </b-form-tags>
        </b-form-group>
    </div>
</template>

<script>
export default {
    name: 'TaggedInput',
    data () {
        return {
            values: []
        }
    },
    computed: {
        availableOptions() {
            return this.options.filter(opt => this.values.indexOf(opt) === -1);
        }
    },
    methods: {
        onOptionClick({option, addTag}) {
            addTag(option);
            this.search = '';
        }
    },
    props: {
        label: String,
        buttonTitle: String,
        options: Array
    }
}
</script>

<style scoped>

ul {
    list-style-type: none;
}

li {
    margin: 0 0 0 0;
}

.list-inline-item:not(:last-child) {
    margin-right: 0px;
}

</style>