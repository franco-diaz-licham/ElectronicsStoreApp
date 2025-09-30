<template>
    <footer class="bg-dark text-light mt-auto">
        <div class="container-xxl py-3 text-center small">
            <div class="mb-2">
                <RouterLink class="text-decoration-none text-light me-3" to="/">Home</RouterLink>
                <RouterLink class="text-decoration-none text-light me-3" to="/about">About Us</RouterLink>
                <RouterLink class="text-decoration-none text-light" to="/contact">Contact</RouterLink>
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
defineProps({
    lastUpdated: { type: String, required: true },
});
</script>
