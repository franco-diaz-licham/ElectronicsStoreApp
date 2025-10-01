<template>
    <div class="d-flex flex-column min-vh-100">
        <!-- Navbar -->
        <AppNavbar :cart-count="count" @search="goSearch" />

        <!-- Main content -->
        <main class="flex-grow-1">
            <div class="container-xxl">
                <router-view />
            </div>
        </main>

        <!-- Footer -->
        <AppFooter />

        <!-- Toast -->
        <AppToast v-model:show="toast.state.show" :message="toast.state.message" :type="toast.state.type" :duration="toast.state.duration" :position="toast.state.position" />
    </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import AppNavbar from "../shared/layout/AppNavbar.vue";
import AppFooter from "../shared/layout/AppFooter.vue";
import AppToast from "../shared/layout/AppToast.vue";
import { useToast } from "../shared/composables/useToast";
import { useCartStore } from "../stores/cartStore";
import { storeToRefs } from "pinia";

const router = useRouter();
/** Query portion of search. */
const q = ref("");
const toast = { state: useToast().state };
const cart = useCartStore();
const { count } = storeToRefs(cart);

/** Search forwarded from navbar to products page. */
function goSearch(query: string) {
    q.value = (query || "").trim();
    const value = q.value ? { q: q.value } : {};
    router.push({ path: "/products", query: value });
}
</script>

<style>
.navbar-nav .router-link-active {
    font-weight: 600;
    text-decoration: underline;
    text-underline-offset: 0.2rem;
}
</style>
