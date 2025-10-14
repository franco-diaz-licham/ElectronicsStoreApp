<template>
    <div class="my-4">
        <section class="row">
            <!-- User profile -->
            <div class="my-3 col-lg-6">
                <h2>User Profile</h2>
                <p class="text-center text-muted small" aria-live="polite">
                    {{ greeting }}
                </p>
                <ProfileForm :model-value="profile" @updated:model="handleSubmit" />
            </div>

            <!-- Order History -->
            <div class="my-3 col-lg-6">
                <div class="d-flex justify-content-between align-items-end">
                    <h2>Orders</h2>
                    <p class="small text-muted mb-0" aria-live="polite">
                        {{ orders.length ? `${orders.length} order${orders.length > 1 ? "s" : ""}` : "No recent orders" }}
                    </p>
                </div>
                <OrderTable :orders="orders" />
            </div>
        </section>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import ProfileForm from "../features/account/components/ProfileForm.vue";
import type { ProfileModel } from "../features/account/models/profile.type";
import { useProfileStore } from "../stores/profileStore";
import { storeToRefs } from "pinia";
import OrderTable from "../features/orders/components/OrderTable.vue";
import type { OrderModel } from "../features/orders/models/order.type";

const profileStore = useProfileStore();
const { profile } = storeToRefs(profileStore);
const { updateProfile } = profileStore;
const orders = ref<OrderModel[]>([]);

/** Form greeting. */
const greeting = computed(() => {
    return profile.value.firstName ? `Hi ${profile.value.firstName} — keep your details up to date.` : "Keep your details up to date.";
});

onMounted(async () => {
    getData();
});

/** Loads sample data. */
const getData = () => {
    orders.value = [
        { id: 10031, total: 2199, date: "2025-07-04", status: "Delivered" },
        { id: 10032, total: 129.95, date: "2025-08-15", status: "Processing" },
    ];
};

/** Handles form submission. */
function handleSubmit(data: ProfileModel) {
    console.log(data);
    updateProfile(data);
}
</script>

<style scoped>
/* Minimal: fade transition for alert */
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s;
}
.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
