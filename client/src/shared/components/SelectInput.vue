<template>
    <Field :name="name" v-slot="{ field, errorMessage, meta }">
        <label :for="id" class="form-label"> {{ label }}<span v-if="required" class="text-danger">*</span> </label>
        <select
            v-bind="field"
            :id="id"
            class="form-control"
            :class="{
                'is-invalid': meta.touched && !!errorMessage,
                'is-valid': meta.dirty && !errorMessage,
            }"
            :aria-invalid="!!errorMessage"
            :aria-describedby="`${id}-feedback`"
            :multiple="multiple"
            :disabled="disabled"
        >
            <!-- Optional placeholder -->
            <option v-if="placeholder && !multiple" disabled value="">
                {{ placeholder }}
            </option>

            <!-- Render options -->
            <option v-for="opt in normalizedOptions" :key="opt.value" :value="opt.value" :disabled="!!opt.disabled">
                {{ opt.label }}
            </option>
        </select>

        <div v-if="help" class="form-text">{{ help }}</div>
        <div :id="`${id}-feedback`" class="invalid-feedback">{{ errorMessage }}</div>
    </Field>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { Field } from "vee-validate";

type Primitive = string | number;

export interface SelectOption {
    value: Primitive;
    label: string;
    disabled?: boolean;
}

const props = withDefaults(
    defineProps<{
        name: string;
        label: string;
        /** Displayed above the select */
        help?: string;
        /** Placeholder shows as the first disabled option (single-select only) */
        placeholder?: string;
        /** Allow multiple selection */
        multiple?: boolean;
        /** Disable the control */
        disabled?: boolean;
        /** Required flag only affects the label’s asterisk (validation is handled by your schema/rules) */
        required?: boolean;
        /** Optional explicit id; otherwise auto-generated from name */
        id?: string;
        /** Options can be an array of { value, label } or a record like { NSW: 'NSW', VIC: 'VIC' } */
        options: SelectOption[] | Record<string, Primitive>;
    }>(),
    { multiple: false, disabled: false, required: false }
);

/** Auto generate ids for better maintainability. */
const id = computed(() => props.id ?? `field-${props.name.replace(/\./g, "-")}`);

/** Normalize options to a consistent array of { value, label, disabled? } */
const normalizedOptions = computed<SelectOption[]>(() => {
    const { options } = props;
    if (Array.isArray(options)) {
        return options.map((o) => ({
            value: o.value,
            label: o.label ?? String(o.value),
            disabled: o.disabled ?? false,
        }));
    }
    // Record form: { key: labelOrValue }
    return Object.entries(options).map(([key, val]) => ({
        value: val,
        label: String(val ?? key),
    }));
});
</script>
