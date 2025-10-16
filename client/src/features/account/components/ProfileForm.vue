<template>
    <form class="w-100 shadow p-3 rounded-3" style="max-width: 640px" @submit.prevent="onSubmit" novalidate>
        <!-- Alert -->
        <transition name="fade">
            <div v-if="alertText" class="alert" :class="alertClass" role="alert" aria-live="polite">
                {{ alertText }}
            </div>
        </transition>
        <div class="row g-3">
            <!-- Firstname -->
            <div class="col-md-12">
                <TextInput name="firstName" label="First Name" required />
            </div>
            <!-- Surname -->
            <div class="col-md-12">
                <TextInput name="surname" label="Surname" required />
            </div>
            <!-- Street -->
            <div class="col-md-12">
                <TextInput name="street" label="Street" required />
            </div>
            <!-- Suburb -->
            <div class="col-md-12">
                <TextInput name="suburb" label="Suburb" required />
            </div>
            <!-- State -->
            <div class="col-md-6">
                <SelectInput name="state" label="State" :options="options" placeholder="Select a state" :required="true" />
            </div>
            <!-- Postcode -->
            <div class="col-md-6">
                <TextInput name="postcode" label="Postcode" required />
            </div>
            <!-- Submit button -->
            <div class="col-12 text-end">
                <button type="submit" class="btn btn-secondary shadow" :disabled="isSubmitting">
                    {{ isSubmitting ? "Saving..." : "Save Changes" }}
                </button>
            </div>
        </div>
    </form>
</template>

<script setup lang="ts">
import { toTypedSchema } from "@vee-validate/zod";
import type { ProfileModel } from "../models/profile.type";
import { profileInitialValues, profileSchema, type ProfileSchema } from "../models/profileSchema";
import { useForm } from "vee-validate";
import TextInput from "../../../shared/components/TextInput.vue";
import { useAlert } from "../../../shared/composables/useAlert";
import SelectInput from "../../../shared/components/SelectInput.vue";
import type { PropType } from "vue";

const props = defineProps({
    modelValue: { type: Object as PropType<ProfileModel>, required: false },
});

const { alertText, alertClass, showAlert } = useAlert();
const options = [
    { value: "NSW", label: "NSW" },
    { value: "VIC", label: "VIC" },
    { value: "QLD", label: "QLD" },
    { value: "SA", label: "SA" },
    { value: "WA", label: "WA" },
    { value: "TAS", label: "TAS" },
    { value: "NT", label: "NT" },
    { value: "ACT", label: "ACT" },
];
const emit = defineEmits<{
    (e: "updated:modelValue", payload: ProfileModel): void;
}>();

const { handleSubmit, isSubmitting, resetForm } = useForm<ProfileSchema>({
    validationSchema: toTypedSchema(profileSchema),
    initialValues: props.modelValue || profileInitialValues,
});

/** Handles submit. */
const onSubmit = handleSubmit(async (values: ProfileModel) => {
    try {
        emit("updated:modelValue", values);
        showAlert("Profile saved.", "success");
        resetForm({ values });
    } catch (e) {
        showAlert("Could not save changes.", "danger");
    }
});
</script>
