<template>
    <section id="hero" class="w-100 my-2 rounded-3 shadow bg-primary-subtle">
        <div class="p-5 text-center">
            <h1 class="mb-1">{{ title }}</h1>
            <p class="lead mb-0">{{ subtitle }}</p>
        </div>
    </section>
</template>
<script setup lang="ts">
defineProps({
    title: { type: String, required: true },
    subtitle: { type: String, required: true },
});
</script>
