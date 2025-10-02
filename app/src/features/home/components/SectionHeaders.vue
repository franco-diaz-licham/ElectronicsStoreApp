<template>
    <div class="d-flex justify-content-between align-items-end">
        <h2 class="mb-2">{{ title }}</h2>
        <RouterLink :to="to" class="btn btn-secondary btn-sm shadow">
            {{ cta }}
        </RouterLink>
    </div>
</template>

<script setup lang="ts">
defineProps({
    title: { type: String, required: true },
    to: { type: [String, Object], default: "/products" },
    cta: { type: String, default: "View all" },
});
</script>
