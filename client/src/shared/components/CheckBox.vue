<template>
    <Field :name="name" v-slot="{ field, errorMessage, meta }">
        <div class="form-check m-0">
            <component
                :is="as"
                v-bind="field"
                :id="id"
                type="checkbox"
                :placeholder="placeholder"
                :autocomplete="autoComplete"
                :name="name"
                class="form-check-input"
                :class="{
                    'is-invalid': meta.touched && !!errorMessage,
                    'is-valid': meta.dirty && !errorMessage,
                }"
                :aria-invalid="!!errorMessage"
                :aria-describedby="`${id}-feedback`"
            />
            <label :for="id" class="form-check-label"> {{ label }}<span v-if="required" class="text-danger">*</span> </label>
        </div>
        <div v-if="help" class="form-text">{{ help }}</div>
        <div :id="`${id}-feedback`" class="invalid-feedback">{{ errorMessage }}</div>
    </Field>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { Field } from "vee-validate";

const props = withDefaults(
    defineProps<{
        name: string;
        label: string;
        as?: "input" | "textarea";
        placeholder?: string;
        help?: string;
        rows?: number;
        autoComplete?: string;
        required?: boolean;
        id?: string;
    }>(),
    { as: "input", required: false }
);

/** Auto generate ids for better maintainability. */
const id = computed(() => props.id ?? `field-${props.name.replace(/\./g, "-")}`);
</script>
