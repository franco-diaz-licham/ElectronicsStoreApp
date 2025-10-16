<template>
    <div class="d-grid gap-2">
        <div class="form-check" v-for="opt in radioOptions" :key="opt.value">
            <input class="form-check-input" type="radio" :name="name" :id="`${name}-${String(opt.value ?? 'any')}`" :value="opt.value" v-model="model" />
            <label class="form-check-label" :for="`${name}-${String(opt.value ?? 'any')}`">
                {{ opt.label }}
            </label>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropType } from "vue";

/** Options used for radio component. */
export interface RadioOption {
    value: string | number;
    label: string;
}

/** Component props. */
const props = defineProps({
    radioOptions: { type: Array as PropType<RadioOption[]>, required: true },
    name: { type: String, default: "price" },
});

const model = defineModel<string | number | "">({ default: "" });
</script>
