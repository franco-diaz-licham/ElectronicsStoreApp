<template>
    <section class="my-2">
        <img :src="src" :alt="alt" class="d-block w-100 rounded-3" loading="eager" />
    </section>
</template>

<script setup lang="ts">
defineProps({
    src: { type: String, required: true },
    alt: { type: String, default: "Promotional banner" },
});
</script>
