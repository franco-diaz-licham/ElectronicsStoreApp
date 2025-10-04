<template>
    <div class="table-responsive shadow rounded-3 p-3">
        <table class="table table-bordered table-striped align-middle text-center">
            <caption class="text-start">
                Your recent orders
            </caption>
            <thead class="table-light">
                <tr>
                    <th scope="col">Order #</th>
                    <th scope="col">Total</th>
                    <th scope="col">Date</th>
                    <th scope="col">Status</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="o in orders" :key="o.id">
                    <td>#{{ o.id }}</td>
                    <td>{{ formatCurrency(o.total) }}</td>
                    <td>{{ formatDate(o.date) }}</td>
                    <td>
                        <span class="badge" :class="statusClass(o.status)">{{ o.status }}</span>
                    </td>
                </tr>
                <tr v-if="!orders.length">
                    <td colspan="4" class="text-muted">No orders to show.</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script setup lang="ts">
import type { PropType } from "vue";
import { formatCurrency, formatDate } from "../../../shared/lib/helpers";
import type { OrderModel } from "../models/order.type";

defineProps({
    orders: { type: Array as PropType<OrderModel[]>, required: true },
});

/** Get class for order status. */
function statusClass(status: string) {
    const s = String(status || "").toLowerCase();
    if (s.includes("delivered") || s.includes("completed")) return "text-bg-success";
    if (s.includes("processing") || s.includes("pending")) return "text-bg-warning";
    if (s.includes("cancel")) return "text-bg-danger";
    return "text-bg-secondary";
}
</script>
