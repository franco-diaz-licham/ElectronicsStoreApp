<template>
    <div class="d-none d-lg-block">
        <a v-if="href" :href="href" class="d-block">
            <img :src="src" :alt="alt" class="d-block w-100 rounded-2" loading="lazy" />
        </a>
        <img v-else :src="src" :alt="alt" class="d-block w-100 rounded-2" loading="lazy" />
    </div>
</template>

<script setup lang="ts">
defineProps({
    src: { type: String, required: true },
    alt: { type: String, default: "Advertisement" },
    href: { type: String, default: "" },
});
</script>
