<template>
    <form @submit.prevent="onSubmit" novalidate>
        <!-- Alert -->
        <transition name="fade">
            <div v-if="alertText" class="alert mt-2" :class="alertClass" role="alert" aria-live="polite">
                {{ alertText }}
            </div>
        </transition>
        <div class="row g-3 mb-3">
            <!-- Username -->
            <div class="text-start">
                <TextInput name="username" label="Username" required auto-complete="username" />
            </div>

            <!-- Password -->
            <div class="text-start">
                <TextInput name="password" label="Password" required type="password" auto-complete="current-password" />
            </div>

            <!-- Remember Me -->
            <div class="d-flex justify-content-between align-items-center">
                <CheckBox name="rememberMe" label="Remember Me" />
                <RouterLink class="text-decoration-underline" to="#">Forgot password?</RouterLink>
            </div>
        </div>

        <!-- Submission button -->
        <button class="btn btn-primary w-100" type="submit" :disabled="isSubmitting">Login</button>
    </form>
</template>

<script setup lang="ts">
import { type PropType } from "vue";
import type { LoginModel } from "../models/auth.type";
import { useAlert } from "../../../shared/composables/useAlert";
import { loginInitialValues, loginSchema, type LoginSchema } from "../models/authSchema";
import { toTypedSchema } from "@vee-validate/zod";
import { useForm } from "vee-validate";
import TextInput from "../../../shared/components/TextInput.vue";
import CheckBox from "../../../shared/components/CheckBox.vue";

const props = defineProps({
    model: { type: Object as PropType<LoginModel>, required: false },
});
const emit = defineEmits<{
    (e: "formSubmit", payload: LoginModel): void;
}>();

const { alertText, alertClass, showAlert } = useAlert();
const { handleSubmit, isSubmitting, resetForm } = useForm<LoginSchema>({
    validationSchema: toTypedSchema(loginSchema),
    initialValues: props.model || loginInitialValues,
});

/** Handles form submission. */
const onSubmit = handleSubmit(async (values: LoginModel) => {
    try {
        emit("formSubmit", values);
        showAlert("Login success.", "success");
        resetForm({ values });
    } catch (e) {
        showAlert("Login failed.", "danger");
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
