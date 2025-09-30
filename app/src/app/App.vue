<template>
    <div class="d-flex flex-column min-vh-100">
        <AppNavbar :cart-count="2" />
        <RouterView />
    </div>
</template>
<script setup lang="ts">
import AppNavbar from '../shared/layout/AppNavbar.vue';

</script>
<style scoped></style>
