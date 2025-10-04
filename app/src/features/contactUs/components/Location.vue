<template>
    <h6>{{ name }}</h6>
    <p class="mb-2">{{ address }}<br />Phone: {{ phone }}</p>
    <img :alt="`Map or storefront for ${name}`" class="location-img mx-auto d-block shadow" :src="image" loading="lazy" />
</template>

<script setup lang="ts">
defineProps({
    name: { type: String, required: true },
    address: { type: String, required: true },
    phone: { type: String, required: true },
    image: { type: String, required: true },
});
</script>
<style scoped>
.location-img {
    background-color: #dee2e6;
    width: 100%;
    height: 200px;
    object-fit: cover;
    border-radius: 0.5rem;
    border: 1px solid #dee2e6;
}
</style>
