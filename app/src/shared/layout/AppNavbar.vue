<template>
    <header class="sticky-top">
        <nav class="navbar navbar-expand-lg navbar-dark shadow-sm">
            <div class="container-xl">
                <!-- Brand -->
                <RouterLink class="navbar-brand d-flex align-items-center gap-2" to="/">
                    <img :src="logo" alt="Company logo" class="rounded" style="height: 65px; width: 70px; object-fit: contain" />
                </RouterLink>

                <!-- Toggler -->
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNavbar" aria-controls="mainNavbar" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="mainNavbar">
                    <!-- Search -->
                    <form class="input-group mx-lg-3 my-2 my-lg-0 flex-grow-1" role="search" @submit.prevent="emitSearch">
                        <span class="input-group-text"><i class="bi bi-search"></i></span>
                        <input v-model.trim="query" class="form-control form-control-small" type="search" placeholder="Search products..." aria-label="Search products" />
                    </form>

                    <!-- Right nav links -->
                    <ul class="navbar-nav ms-lg-auto align-items-lg-center fw-bold">
                        <li v-for="link in mainLinks" :key="link.path" class="nav-item">
                            <RouterLink class="nav-link px-3 text-white" :to="link.path">{{ link.label }}</RouterLink>
                        </li>

                        <!-- Cart with badge -->
                        <li class="nav-item ms-lg-2">
                            <RouterLink class="nav-link position-relative px-2" to="/cart" aria-label="Cart">
                                <i class="bi bi-cart4 fs-5 text-white"></i>
                                <span v-if="cartCount" class="position-absolute top-0 start-75 badge rounded-pill bg-warning text-dark">
                                    {{ cartCount }}
                                </span>
                            </RouterLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
</template>

<script setup lang="ts">
import { ref } from "vue";
import logo from "/images/logo.png";

const props = defineProps({
    cartCount: { type: Number, default: 0 },
});
const emit = defineEmits(["search"]);
const query = ref("");

const mainLinks = [
    { path: "/", label: "Home" },
    { path: "/products", label: "Products" },
    { path: "/about", label: "About" },
    { path: "/contact", label: "Contact" },
    { path: "/account", label: "Account" },
    { path: "/login", label: "Login" },
];

function emitSearch() {
    emit("search", query.value);
}
</script>

<style scoped>
header {
    background: #008cff;
}
</style>
