<template>
    <main class="my-4">
        <!-- Breadcrumbs -->
        <nav aria-label="breadcrumb" class="mb-3">
            <ol class="breadcrumb small">
                <li class="breadcrumb-item"><RouterLink to="/">Home</RouterLink></li>
                <li class="breadcrumb-item"><RouterLink to="/products">Products</RouterLink></li>
                <li class="breadcrumb-item active" aria-current="page">{{ product?.title || "Product" }}</li>
            </ol>
        </nav>

        <!-- Product -->
        <section v-if="product" class="mb-4">
            <div class="row g-4 shadow rounded-3 py-3">
                <div class="col-md-6">
                    <figure class="product-figure rounded-3 d-flex align-items-center justify-content-center bg-light-subtle">
                        <img class="product-main-img img-fluid rounded" :src="product.image" :alt="product.title" loading="eager" style="max-height: 360px; object-fit: contain" />
                    </figure>
                </div>

                <div class="col-md-6">
                    <h1 class="h2 mb-1">{{ product.title }}</h1>
                    <p class="text-muted mb-2">{{ product.brand }} • {{ product.category }}</p>
                    <p class="mb-3">{{ product.description || "Short description not available." }}</p>

                    <div class="d-flex align-items-center gap-3 mb-3">
                        <span class="h4 mb-0">{{ formatCurrency(product.price) }}</span>
                        <span class="badge text-bg-success">In stock</span>
                    </div>

                    <div class="d-flex align-items-center gap-2 mb-3">
                        <label class="fw-semibold me-1" for="qtyInput">Qty:</label>
                        <div class="input-group" style="width: 160px">
                            <button aria-label="Decrease quantity" class="btn btn-outline-secondary" type="button" @click="decreaseQuantity">
                                <i class="bi bi-dash"></i>
                            </button>
                            <input id="qtyInput" aria-label="Quantity" class="form-control text-center" type="number" min="1" step="1" v-model.number="quantity" />
                            <button aria-label="Increase quantity" class="btn btn-outline-secondary" type="button" @click="increaseQuantity">
                                <i class="bi bi-plus"></i>
                            </button>
                        </div>
                    </div>

                    <div class="d-flex gap-2">
                        <button class="btn btn-secondary shadow" @click="addToCart"><i class="bi bi-cart-plus me-1"></i>Add to Cart</button>
                        <RouterLink class="btn btn-outline-secondary shadow" to="/cart">Go to Cart</RouterLink>
                    </div>
                </div>
            </div>
        </section>

        <!-- Tabs -->
        <section v-if="product">
            <ul class="nav nav-tabs mt-4 mb-3 shadow rounded-3" id="productTab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" data-bs-target="#details" data-bs-toggle="tab" role="tab" type="button">Details</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" data-bs-target="#specs" data-bs-toggle="tab" role="tab" type="button">Specifications</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" data-bs-target="#reviews" data-bs-toggle="tab" role="tab" type="button">Reviews</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" data-bs-target="#manuals" data-bs-toggle="tab" role="tab" type="button">Manuals</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" data-bs-target="#warranty" data-bs-toggle="tab" role="tab" type="button">Warranty &amp; Returns</button>
                </li>
            </ul>

            <div class="tab-content shadow rounded-3 p-3">
                <div class="tab-pane fade show active" id="details" role="tabpanel">
                    <p v-if="detailsText">{{ detailsText }}</p>
                    <p v-else class="text-muted">No details provided.</p>
                </div>

                <div class="tab-pane fade" id="specs" role="tabpanel">
                    <div class="table-responsive">
                        <table class="table table-bordered align-middle">
                            <tbody>
                                <tr v-for="row in specRows" :key="row.key">
                                    <th class="w-25">{{ row.key }}</th>
                                    <td>{{ row.value }}</td>
                                </tr>
                                <tr v-if="!specRows.length">
                                    <td colspan="2" class="text-muted">No specifications listed.</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="tab-pane fade" id="reviews" role="tabpanel">
                    <p class="text-muted mb-0">Reviews coming soon.</p>
                </div>

                <div class="tab-pane fade" id="manuals" role="tabpanel">
                    <p>See brand support page for downloadable manuals.</p>
                </div>

                <div class="tab-pane fade" id="warranty" role="tabpanel">
                    <p>12-month manufacturer warranty and 30-day returns (change-of-mind). Australian Consumer Law rights apply.</p>
                </div>
            </div>
        </section>

        <!-- Other Products -->
        <section v-if="otherProducts.length" class="w-100 pb-4">
            <h4 class="mt-5">Other Products</h4>
            <ProductGrid :products="otherProducts" :cols-sm="3" :cols-md="4" :cols-lg="6" />
        </section>

        <!-- Not found -->
        <div v-if="!loading && !product" class="alert alert-warning mt-4">Product not found.</div>
    </main>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from "vue";
import { useRoute } from "vue-router";
import { formatCurrency } from "../shared/lib/helpers";
import type { ProductModel } from "../features/products/models/product.type";
import { useProducts } from "../features/products/api/useProducts";
import ProductGrid from "../features/products/components/ProductGrid.vue";
import type { BasketItemModel } from "../features/basket/models/basket.type";
import { useCartStore } from "../stores/cartStore";
import { useToast } from "../shared/composables/useToast";
import { mapToProductCardModel } from "../features/products/api/mapper";

const route = useRoute();
const product = ref<ProductModel | null>(null);
const quantity = ref(1);
const loading = ref(true);
const { data: products, isLoading: productsLoading } = useProducts();
const cart = useCartStore();
const { showAlert } = useToast();

/** Get poduct id from url. */
const productId = computed(() => {
    const idRaw = route.query.id;
    return idRaw != null ? String(idRaw) : "";
});

onMounted(getData);
watch(() => route.query.id, getData);
watch([products, productsLoading], () => {
    if (!productsLoading.value) {
        getData();
    }
});

/** Fetches data and maps it to correct models for display. */
async function getData() {
    if (productsLoading.value) return [];
    const list = products.value ?? [];
    try {
        loading.value = true;
        const found = list.find((p) => String(p.id) === productId.value);
        product.value = found || null;
    } catch (e: unknown) {
        console.error("Failed to load product data", e);
        product.value = null;
        showAlert("Failed to load product data", "danger");
    } finally {
        loading.value = false;
    }
}

/** Get details message. */
const detailsText = computed(() => product.value?.details || product.value?.description || "");

/** Get specification information. */
const specRows = computed(() => {
    const specs = product.value?.specifications;
    if (!specs) return [];
    return Object.entries(specs).map(([key, value]) => ({ key, value })); // map into object
});

/** Get related products for product grid. */
const otherProducts = computed(() => {
    if (!product.value) return [];
    const sameCategory = products.value!.filter((p) => p.id !== product.value?.id && p.category === product.value?.category).map(mapToProductCardModel);
    const rest = products.value!.filter((p) => p.id !== product.value?.id && p.category !== product.value?.category).map(mapToProductCardModel);
    return [...sameCategory, ...rest].slice(0, 6);
});

/** Handle increase in quantity */
function increaseQuantity() {
    quantity.value++;
}

/** Handle decrease in quantity */
function decreaseQuantity() {
    if (quantity.value > 1) quantity.value--;
}

/** Add items to cart and keep info in local storage. */
function addToCart() {
    if (!product.value) {
        showAlert("No product to add.", "danger");
        return;
    }

    try {
        // get quantity and add item.
        const addQty = Math.max(1, Number(quantity.value) || 1);
        const item: BasketItemModel = {
            quantity: addQty,
            product: product.value,
        };
        cart.add(item);
        showAlert(`${product.value.title} (x${addQty}) added to cart`, "success");
    } catch (e) {
        showAlert("Could not save cart.", "danger");
    }
}
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.4s;
}
.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
