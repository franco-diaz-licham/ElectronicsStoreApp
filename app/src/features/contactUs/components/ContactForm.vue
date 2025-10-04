<template>
    <form class="shadow p-3 rounded-3 needs-validation" novalidate @submit.prevent="onSubmit">
        <div class="row g-3">
            <!-- Firstname -->
            <div class="col-md-6">
                <TextInput name="firstName" id="firstName" label="First Name" required />
            </div>
            <!-- Surname -->
            <div class="col-md-6">
                <TextInput name="surname" label="Surname" required />
            </div>
            <!-- Phone -->
            <div class="col-md-6">
                <TextInput name="phone" label="Phone" type="tel" required placeholder="e.g., 0412 345 678" />
            </div>
            <!-- Email -->
            <div class="col-md-6">
                <TextInput name="email" label="Email" type="email" required />
            </div>
            <!-- Subject -->
            <div class="col-12">
                <TextInput name="subject" label="Subject" required />
            </div>
            <!-- Message -->
            <div class="col-12">
                <TextInput name="message" label="Message" as="textarea" :rows="4" required help="Min 10 characters." />
            </div>
            <!-- Button -->
            <div class="col-12 text-end">
                <button type="submit" class="btn btn-secondary shadow" :disabled="isSubmitting"><span v-if="isSubmitting">Sending…</span><span v-else>Send Message</span></button>
            </div>
        </div>
    </form>
</template>

<script setup lang="ts">
import { useForm } from "vee-validate";
import { toTypedSchema } from "@vee-validate/zod";
import { contactInitialValues, contactSchema, type ContactSchema } from "../models/contactSchema";
import type { ContactFormModel } from "../models/contact.type";
import TextInput from "../../../shared/components/TextInput.vue";

const emit = defineEmits<{
    (e: "formSubmit", payload: ContactFormModel): void;
}>();

const { handleSubmit, isSubmitting, resetForm } = useForm<ContactSchema>({
    validationSchema: toTypedSchema(contactSchema),
    initialValues: contactInitialValues,
});

/** Handles submit. */
const onSubmit = handleSubmit(async (values: ContactFormModel) => {
    emit("formSubmit", values);
    resetForm();
});
</script>
