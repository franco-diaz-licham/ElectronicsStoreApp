import { defineStore } from "pinia";
import { computed, ref, watch } from "vue";
import type { BasketItems } from "../entities/BasketItems";

/** Local storage key. */
const STORAGE_KEY = "cart:v1";

export const useCartStore = defineStore("cart", () => {
    /** Get basket items from local storage. */
    const initial = (() => {
        try {
            return JSON.parse(localStorage.getItem(STORAGE_KEY) ?? "[]") as BasketItems[];
        } catch {
            return [] as BasketItems[];
        }
    })();

    /** Gets all the current items stored in local storage. */
    const items = ref<BasketItems[]>(initial);

    /** Counts the total cost of basket items. */
    const total = computed(() => items.value.reduce((s, i) => s + i.price * i.qty, 0));

    /** The total number of items in the basket. */
    const count = computed(() => items.value.reduce((n, i) => n + i.qty, 0));

    /** Adds a item to the current basket. */
    function add(item: BasketItems) {
        const found = items.value.find((x) => x.id === item.id);
        found ? (found.qty += item.qty) : items.value.push({ ...item });
    }

    /** Removes an item from the current basket. */
    function remove(id: string) {
        items.value = items.value.filter((i) => i.id !== id);
    }

    /** Clears the entire basket. */
    function clear() {
        items.value = [];
    }

    /** Save cart information to local storage. */
    watch(
        items,
        (val) => {
            localStorage.setItem(STORAGE_KEY, JSON.stringify(val));
        },
        { deep: true }
    );

    return { items, total, count, add, remove, clear };
});
