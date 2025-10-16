<template>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-4 g-3">
        <div class="col" v-for="val in values" :key="val.title">
            <ValueCard :model="val" />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropType } from "vue";
import ValueCard from "./ValueCard.vue";
import type { ValueCardModel } from "../models/aboutUs.type";

defineProps({
    values: {
        type: Array as PropType<ValueCardModel[]>,
        required: true,
    },
});
</script>
