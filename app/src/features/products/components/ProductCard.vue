<template>
    <div class="col">
        <div class="card h-100 shadow text-center">
            <img :src="product.image" :alt="product.title" class="card-img-top" loading="lazy" style="height: 200px; object-fit: contain" />
            <div class="card-body d-flex flex-column">
                <h6 class="card-title mb-1 text-truncate">{{ product.title }}</h6>
                <div class="text-muted small mb-2 text-truncate">{{ product.brand }} · {{ product.category }}</div>
                <div class="fw-semibold mb-3">{{ formatCurrency(product.price) }}</div>
                <div class="mt-auto btn-group">
                    <RouterLink :to="product.href || '/products'" class="btn btn-outline-secondary btn-sm"> View </RouterLink>
                    <button class="btn btn-primary btn-sm pe-3" @click="addToCart">Add</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropType } from "vue";
import { formatCurrency } from "../../../shared/lib/helpers";
import type { ProductCardModel } from "../models/product.type";

/** Functional prrops/ */
const props = defineProps({
    product: { type: Object as PropType<ProductCardModel>, required: true },
});

// Define callbacks.
const emit = defineEmits(["addItem"]);

/** Add items to cart and keep info in local storage. */
const addToCart = () => emit("addItem", props.product);
</script>
