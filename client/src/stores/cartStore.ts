import { defineStore } from "pinia";
import { computed, ref, watch } from "vue";
import type { BasketItemModel } from "../features/basket/models/basket.type";

/** Local storage key. */
const STORAGE_KEY = "cart";

export const useCartStore = defineStore("cart", () => {
    /** Get basket items from local storage. */
    const initial = (() => {
        try {
            return JSON.parse(localStorage.getItem(STORAGE_KEY) ?? "[]") as BasketItemModel[];
        } catch {
            return [] as BasketItemModel[];
        }
    })();

    /** Gets all the current items stored in local storage. */
    const items = ref<BasketItemModel[]>(initial);

    /** Determines shipping costs. */
    const shipping = computed(() => (items.value.length ? 25 : 0));

    /** GST calculation. */
    const gst = computed(() => subtotal.value * 0.1);

    /** Counts the total cost of basket items. */
    const subtotal = computed(() => items.value.reduce((s, i) => s + i.product.price * i.quantity, 0));

    /** Calculates the total cost of the order. */
    const total = computed(() => subtotal.value + shipping.value + gst.value);

    /** The total number of items in the basket. */
    const count = computed(() => items.value.reduce((n, i) => n + i.quantity, 0));

    /** Adds a item to the current basket. */
    function add(item: BasketItemModel) {
        const found = items.value.find((x) => x.product.id === item.product.id);
        found ? (found.quantity += item.quantity) : items.value.push({ ...item });
    }

    /** Removes an item from the current basket. */
    function remove(id: number) {
        items.value = items.value.filter((i) => i.product.id !== id);
    }

    /** Updates quantity */
    function updateQuantity(id: number, qty: number) {
        const found = items.value.find((x) => x.product.id === id);
        if (!found) return;
        found.quantity = qty;
    }

    /** Clears the entire basket. */
    function clear() {
        items.value = [];
    }

    // Save cart information to local storage.
    watch(
        items,
        (val) => {
            localStorage.setItem(STORAGE_KEY, JSON.stringify(val));
        },
        { deep: true }
    );

    return { items, total, subtotal, count, shipping, gst, add, remove, clear, updateQuantity };
});
