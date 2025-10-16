<template>
    <div id="contain" class="d-flex justify-content-center align-items-center">
        <section class="signup-container mx-auto shadow">
            <div class="logo-placeholder d-flex align-items-center justify-content-center">
                <img alt="Retail eCommerce logo" src="/images/logo.png" width="60" height="52" onerror="this.style.visibility='hidden'" />
            </div>
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h1 class="h4 mb-0">Signup</h1>
                <RouterLink class="text-decoration-underline fw-bold" to="/login">Login</RouterLink>
            </div>
            <SignupForm @form-submit="handleSubmit" />
        </section>
    </div>
</template>

<script setup lang="ts">
import SignupForm from "../features/account/components/SignupForm.vue";
import type { SignupModel } from "../features/account/models/auth.type";

/** Handles form submission. */
function handleSubmit(data: SignupModel) {
    console.log(data);
}
</script>

<style scoped>
#contain {
    min-height: calc(100vh - 196px);
}

.signup-container {
    width: 100%;
    max-width: 420px;
    border: 1px solid #dee2e6;
    border-radius: 1rem;
    padding: 2rem;
    background: #fff;
}

.logo-placeholder {
    width: 100px;
    height: 100px;
    background-color: #f1f3f5;
    border: 1px solid #dee2e6;
    margin: 0 auto 1rem;
    border-radius: 12%;
}
</style>
