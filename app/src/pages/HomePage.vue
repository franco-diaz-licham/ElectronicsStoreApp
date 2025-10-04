<template>
    <!-- Hero -->
    <HeroBanner src="/images/index-banner.png" alt="Seasonal promotions banner with featured products" />
    
    <!-- Main Content -->
    <section class="mt-2 mb-5">
        <div class="row g-4">
            <!-- Left Ad -->
            <div class="col-lg-2">
                <SideAd src="/images/index-add-1.png" alt="Advertisement: side offer one" />
            </div>

            <!-- Center -->
            <section class="col-lg-8 col-12 my-4">
                <!-- Hot Deals -->
                <SectionHeaders title="Hot Deals" :to="{ path: '/products', query: { sort: 'hot' } }" />
                <p class="small text-muted mb-2" aria-live="polite">{{ hotDeals.length }} products featured</p>
                <ProductGrid :products="hotDeals" :cols-sm="2" :cols-md="3" :cols-lg="4" />

                <!-- Latest -->
                <SectionHeaders title="Latest" :to="{ path: '/products', query: { sort: 'latest' } }" class="mt-5" />
                <p class="small text-muted mb-2" aria-live="polite">{{ latest.length }} products featured</p>
                <ProductGrid :products="latest" :cols-sm="2" :cols-md="4" :cols-lg="4" @add-item="handleAddItem" />
            </section>

            <!-- Right Ad -->
            <div class="col-lg-2">
                <SideAd src="/images/index-add-2.png" alt="Advertisement: side offer two" />
            </div>
        </div>
    </section>
</template>

<script setup lang="ts">
import { useProducts } from "../features/products/api/useProducts";
import { mapToProductCardModel } from "../features/products/api/mapper";
import type { ProductCardModel } from "../features/products/models/product.type";
import HeroBanner from "../features/home/components/HeroBanner.vue";
import SideAd from "../features/home/components/SideAd.vue";
import SectionHeaders from "../features/home/components/SectionHeaders.vue";
import ProductGrid from "../features/products/components/ProductGrid.vue";
import { computed } from "vue";
import { useCartStore } from "../stores/cartStore";
import type { BasketItemModel } from "../features/basket/models/basket.type";
import { useToast } from "../shared/composables/useToast";

// Data hooks.
const { data: products } = useProducts();
const cart = useCartStore();
const { showAlert } = useToast();

/** Products classified as hot deals. */
const hotDeals = computed<ProductCardModel[]>(() => (products.value ?? []).slice(0, 4).map(mapToProductCardModel));

/** Products classified as latest. */
const latest = computed<ProductCardModel[]>(() => (products.value ?? []).slice(4, 8).map(mapToProductCardModel));

const handleAddItem = (data: ProductCardModel) => {
    const selectedProduct = products.value?.find((x) => x.id === data.id);
    if (!selectedProduct) return;

    try {
        const item: BasketItemModel = {
            quantity: 1,
            product: selectedProduct,
        };
        cart.add(item);
        showAlert(`${data.title} (x${1}) added to cart`, "success");
    } catch (e) {
        showAlert("Could not save cart.", "danger");
    }
};
</script>
