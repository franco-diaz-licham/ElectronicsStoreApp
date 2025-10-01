<template>
    <div :class="gridClass">
        <ProductCard v-for="p in products" :key="p.id" :product="p" @add-item="addToCart" />
    </div>
</template>

<script setup lang="ts">
import { computed, type PropType } from "vue";
import ProductCard from "./ProductCard.vue";
import type { ProductCardModel } from "../models/product.type";

/** Component props. */
const props = defineProps({
    products: { type: Array as PropType<ProductCardModel[]>, required: true },
    colsSm: { type: Number, default: 2 },
    colsMd: { type: Number, default: 3 },
    colsLg: { type: Number, default: 4 },
});

// Define callbacks
const emit = defineEmits(["addItem"]);

/** Setup botostrap calsses based on props. */
const gridClass = computed(() => ["row", "g-4", "row-cols-1", `row-cols-sm-${props.colsSm}`, `row-cols-md-${props.colsMd}`, `row-cols-lg-${props.colsLg}`].join(" "));

/** Add items to cart and keep info in local storage. */
const addToCart = (data: ProductCardModel) => emit("addItem", data);
</script>
