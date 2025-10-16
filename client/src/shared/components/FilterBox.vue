<template>
    <section class="border rounded p-3 bg-light mb-3 shadow">
        <h5 class="h6 mb-3">{{ title }}</h5>
        <slot />
    </section>
</template>

<script setup lang="ts">
defineProps({ title: { type: String, required: true } });
</script>
