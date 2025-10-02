<template>
    <div class="card h-100 text-center shadow-sm">
        <div class="card-body">
            <i aria-hidden="true" class="bi fs-1" :class="model.icon"></i>
            <h3 class="h6 mt-2">{{ model.title }}</h3>
            <p class="small text-muted mb-0">{{ model.text }}</p>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropType } from "vue";
import type { ValueCardModel } from "../models/aboutUs.type";

defineProps({
    model: { type: Object as PropType<ValueCardModel>, required: true },
});
</script>
