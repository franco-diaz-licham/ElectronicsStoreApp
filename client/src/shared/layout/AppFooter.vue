<template>
    <footer class="bg-dark text-light mt-auto">
        <div class="container-xxl py-3 text-center small">
            <div class="mb-2">
                <RouterLink class="text-decoration-none text-light me-3" to="/">Home</RouterLink>
                <RouterLink class="text-decoration-none text-light me-3" to="/about-us">About Us</RouterLink>
                <RouterLink class="text-decoration-none text-light" to="/contact-us">Contact</RouterLink>
            </div>
            <div>
                © 2025 Retail eCommerce ·
                <a href="mailto:hello@retailecommerce.example" class="text-decoration-none link-light"> hello@retailecommerce.example </a>
            </div>
            <div class="text-secondary">Last updated: {{ lastUpdated }}</div>
        </div>
    </footer>
</template>

<script setup lang="ts">
import { computed } from 'vue';

/** footer timestamp  */
const lastUpdated = computed(() => {
    const d = new Date();
    return d.toLocaleString(undefined, {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
    });
});
</script>
