<template>
    <!-- Hero -->
    <section id="hero" class="w-100 my-2 rounded-3 shadow bg-primary-subtle">
        <div class="p-5 text-center">
            <h1 class="mb-1">Products</h1>
            <p class="lead mb-0">Seasonal Offers and Promotions</p>
        </div>
    </section>

    <section class="my-3">
        <div class="row g-4">
            <!-- Filters -->
            <aside class="col-12 col-md-4 col-lg-3">
                <form @submit.prevent class="filters" novalidate>
                    <!-- Price -->
                    <FilterBox title="Price">
                        <RadioButton v-model="filters.price" :radio-options="PriceOptions" />
                    </FilterBox>

                    <!-- Category -->
                    <FilterBox title="Category">
                        <ul class="list-unstyled mb-0">
                            <li v-for="c in usedCategories" :key="c" class="form-check">
                                <input class="form-check-input me-2" type="checkbox" :id="`cat-${c}`" :value="c" v-model="filters.categories" />
                                <label class="form-check-label" :for="`cat-${c}`">{{ c }}</label>
                            </li>
                        </ul>
                    </FilterBox>

                    <!-- Brand -->
                    <FilterBox title="Brand">
                        <ul class="list-unstyled mb-0">
                            <li v-for="b in usedBrands" :key="b" class="form-check">
                                <input class="form-check-input me-2" type="checkbox" :id="`brand-${b}`" :value="b" v-model="filters.brands" />
                                <label class="form-check-label" :for="`brand-${b}`">{{ b }}</label>
                            </li>
                        </ul>
                    </FilterBox>

                    <div class="d-flex gap-2">
                        <button type="button" class="btn btn-secondary btn-sm shadow" @click="clearFilters">Clear filters</button>
                        <button type="button" class="btn btn-outline-secondary btn-sm shadow" @click="resetSearch">Clear search</button>
                    </div>
                </form>
            </aside>

            <!-- Products -->
            <section class="col-12 col-md-8 col-lg-9">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <div class="small text-muted" aria-live="polite">
                        {{ filtered.length }} products found
                        <span v-if="searchQuery"
                            >for "<b>{{ searchQuery }}</b
                            >"</span
                        >
                    </div>

                    <div class="d-flex align-items-center gap-4">
                        <!-- Sorting -->
                        <div class="d-flex align-items-center gap-2">
                            <label for="sort" class="small text-muted">Sort</label>
                            <select id="sort" v-model="sortBy" class="form-select form-select-sm w-auto shadow">
                                <option value="relevance">Select All</option>
                                <option value="price-asc">Price: Low → High</option>
                                <option value="price-desc">Price: High → Low</option>
                                <option value="name-asc">Name: A → Z</option>
                                <option value="name-desc">Name: Z → A</option>
                            </select>
                        </div>

                        <!-- Pager -->
                        <nav v-if="pages > 1" aria-label="Product pages">
                            <ul class="pagination pagination-sm justify-content-end m-0">
                                <div class="d-flex shadow">
                                    <li class="page-item" :class="{ disabled: page === 1 }">
                                        <button class="page-link" @click="page -= 1" :disabled="page === 1">Prev</button>
                                    </li>
                                    <li class="page-item disabled">
                                        <span class="page-link">{{ page }} / {{ pages }}</span>
                                    </li>
                                    <li class="page-item" :class="{ disabled: page === pages }">
                                        <button class="page-link" @click="page += 1" :disabled="page === pages">Next</button>
                                    </li>
                                </div>
                            </ul>
                        </nav>
                    </div>
                </div>

                <ProductGrid :products="paged" :cols-sm="2" :cols-md="2" :cols-lg="3" class="mb-4" @add-item="(data: ProductCardModel) => handleAddItem(data)" />
            </section>
        </div>
    </section>
</template>

<script setup lang="ts">
import { ref, computed, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useProducts } from "../features/products/api/useProducts";
import { useBrands } from "../features/brands/api/useBrands";
import { useCategories } from "../features/categories/api/useCategories";
import type { ProductCardModel, selectorBaseModel } from "../features/products/models/product.type";
import FilterBox from "../shared/components/FilterBox.vue";
import ProductGrid from "../features/products/components/ProductGrid.vue";
import RadioButton from "../shared/components/RadioButton.vue";
import { useCartStore } from "../stores/cartStore";
import type { BasketItemModel } from "../features/basket/models/basket.type";

const cart = useCartStore();
const route = useRoute();
const router = useRouter();
const filters = ref<{ price: string; categories: string[]; brands: string[] }>({
    price: "",
    categories: [],
    brands: [],
});

/** Sorting options. */
const PriceOptions = [
    { value: "under100", label: "Under $100" },
    { value: "100to500", label: "$100 - $500" },
    { value: "over500", label: "Above $500" },
    { value: "", label: "Any price" },
];

/** Sort by options for sorting component. */
const sortBy = ref<"relevance" | "price-asc" | "price-desc" | "name-asc" | "name-desc">("relevance");

/** Current page. */
const page = ref(1);

/** Maximum products per page. */
const pageSize = 9;

/** Search string from the search bar. */
const searchQuery = computed(() => (route.query.q || "").toString().trim());

// Data hooks.
const { data: products, isLoading: productsLoading } = useProducts();
const { data: brands, isLoading: brandsLoading } = useBrands();
const { data: categories, isLoading: categoriesLoading } = useCategories();

/** Convert loaded products into card-ready models with href */
const productsForCard = computed<ProductCardModel[]>(() => {
    if (productsLoading.value) return [];
    const arr = products.value ?? [];
    return arr.map((p, i) => ({
        id: p.id ?? i,
        title: p.title,
        price: p.price,
        image: p.image,
        brand: p.brand,
        category: p.category,
        href: `/product-details?id=${p.id ?? i}`,
    }));
});

/** Get unique values for filters. */
function uniqueSortedByName<T extends selectorBaseModel>(list: T[] = []): string[] {
    const set = new Set(list.map((x) => x.name).filter(Boolean) as string[]);
    return [...set].sort((a, b) => a.localeCompare(b));
}

/** Unique values for brands. */
const usedBrands = computed<string[]>(() => {
    if (brandsLoading.value) return [];
    return uniqueSortedByName(brands.value);
});

/** Uniqye values for categories. */
const usedCategories = computed<string[]>(() => {
    if (categoriesLoading.value) return [];
    return uniqueSortedByName(categories.value);
});

/** Filtering and sorting pipeline. */
const filtered = computed<ProductCardModel[]>(() => {
    let list = [...productsForCard.value];

    // Search (name/brand/category)
    if (searchQuery.value) {
        const q = searchQuery.value.toLowerCase();
        list = list.filter((p) => p.title?.toLowerCase().includes(q) || p.brand?.toLowerCase().includes(q) || p.category?.toLowerCase().includes(q));
    }

    // Category filter
    if (filters.value.categories.length) {
        const set = new Set(filters.value.categories);
        list = list.filter((p) => p.category && set.has(p.category));
    }

    // Brand filter
    if (filters.value.brands.length) {
        const set = new Set(filters.value.brands);
        list = list.filter((p) => p.brand && set.has(p.brand));
    }

    // Price filter
    switch (filters.value.price) {
        case "under100":
            list = list.filter((p) => p.price < 100);
            break;
        case "100to500":
            list = list.filter((p) => p.price >= 100 && p.price <= 500);
            break;
        case "over500":
            list = list.filter((p) => p.price > 500);
            break;
    }

    // Sorting
    switch (sortBy.value) {
        case "price-asc":
            list.sort((a, b) => a.price - b.price);
            break;
        case "price-desc":
            list.sort((a, b) => b.price - a.price);
            break;
        case "name-asc":
            list.sort((a, b) => a.title.localeCompare(b.title));
            break;
        case "name-desc":
            list.sort((a, b) => b.title.localeCompare(a.title));
            break;
        default:
            break;
    }

    return list;
});

/** Calculate the number of pages. */
const pages = computed(() => Math.max(1, Math.ceil(filtered.value.length / pageSize)));

/** Get the number of products per current page. */
const paged = computed<ProductCardModel[]>(() => {
    const start = (page.value - 1) * pageSize;
    return filtered.value.slice(start, start + pageSize);
});

// Keep page in range & reset on new search
watch([filtered, page], () => {
    if (page.value > pages.value) page.value = 1;
});
watch(
    () => route.query.q,
    () => {
        page.value = 1;
    }
);

/** Clear all filters. */
function clearFilters() {
    filters.value = { price: "", categories: [], brands: [] };
    page.value = 1;
}

/** Clear search results. */
function resetSearch() {
    const qless = { ...route.query };
    delete qless.q;
    router.replace({ path: route.path, query: qless });
}

/** Handle added item to basket. */
const handleAddItem = (data: ProductCardModel) => {
    const selectedProduct = products.value?.find((x) => x.id === data.id);
    if(!selectedProduct) return;
    const item: BasketItemModel = {
        quantity: 1,
        product: selectedProduct,
    };
    cart.add(item);
};
</script>
