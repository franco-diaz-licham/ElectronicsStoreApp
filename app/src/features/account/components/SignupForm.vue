<template>
    <form @submit.prevent="onSubmit" novalidate>
        <!-- Alert -->
        <transition name="fade">
            <div v-if="alertText" class="alert mt-2" :class="alertClass" role="alert" aria-live="polite">
                {{ alertText }}
            </div>
        </transition>
        <div class="row g-3 mb-3">
            <!-- Firstname -->
            <div class="col-md-12">
                <TextInput name="firstName" label="First Name" required />
            </div>
            <!-- Surname -->
            <div class="col-md-12">
                <TextInput name="surname" label="Surname" required />
            </div>
            <!-- Email -->
            <div class="col-md-12">
                <TextInput name="email" label="Email" type="email" required />
            </div>
            <!-- Username -->
            <div class="text-start">
                <TextInput name="username" label="Username" required auto-complete="username" />
            </div>
            <!-- Password -->
            <div class="text-start">
                <TextInput name="password" label="Password" required type="password" auto-complete="current-password" />
            </div>
        </div>

        <!-- Submission button -->
        <button class="btn btn-primary w-100" type="submit" :disabled="isSubmitting">Signup</button>
    </form>
</template>

<script setup lang="ts">
import { type PropType } from "vue";
import type { SignupModel } from "../models/auth.type";
import { useAlert } from "../../../shared/composables/useAlert";
import { signupInitialValues, signupSchema, type SignupSchema } from "../models/authSchema";
import { toTypedSchema } from "@vee-validate/zod";
import { useForm } from "vee-validate";
import TextInput from "../../../shared/components/TextInput.vue";

const props = defineProps({
    model: { type: Object as PropType<SignupModel>, required: false },
});
const emit = defineEmits<{
    (e: "formSubmit", payload: SignupModel): void;
}>();

const { alertText, alertClass, showAlert } = useAlert();
const { handleSubmit, isSubmitting, resetForm } = useForm<SignupSchema>({
    validationSchema: toTypedSchema(signupSchema),
    initialValues: props.model || signupInitialValues,
});

/** Handles form submission. */
const onSubmit = handleSubmit(async (values: SignupModel) => {
    try {
        emit("formSubmit", values);
        showAlert("Signup success.", "success");
        resetForm({ values });
    } catch (e) {
        showAlert("Signup failed.", "danger");
    }
});
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s;
}
.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
