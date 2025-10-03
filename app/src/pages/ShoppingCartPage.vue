<template>
    <div class="my-4">
        <!-- Cart Table -->
        <section class="w-100 my-3">
            <h2 class="text-center mb-4">Cart</h2>

            <div v-if="items.length" class="table-responsive shadow rounded-3 p-3">
                <table class="table table-bordered align-middle text-center">
                    <caption class="text-start">
                        Items currently in your shopping cart
                    </caption>
                    <thead class="table-light">
                        <tr>
                            <th class="text-start" scope="col">Product</th>
                            <th scope="col">Price</th>
                            <th scope="col" style="width: 160px">Quantity</th>
                            <th scope="col">Total</th>
                            <th scope="col" style="width: 60px">Remove</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in items" :key="item.product.id">
                            <td class="text-start d-flex align-items-center gap-2">
                                <img :src="item.product.image" :alt="item.product.title" class="cart-img" />
                                <span>{{ item.product.title }}</span>
                            </td>
                            <td>{{ formatCurrency(item.product.price) }}</td>
                            <td>
                                <div class="input-group input-group-sm mx-auto" style="width: 120px">
                                    <button class="btn btn-outline-secondary" type="button" @click="decreaseQuantity(item)">-</button>
                                    <input class="form-control text-center quantity-input" type="number" v-model.number="item.quantity" min="1" />
                                    <button class="btn btn-outline-secondary" type="button" @click="increaseQuantity(item)">+</button>
                                </div>
                            </td>
                            <td>{{ formatCurrency(item.product.price * item.quantity) }}</td>
                            <td>
                                <button class="btn btn-sm btn-outline-danger" @click="removeItem(item)">
                                    <i class="bi bi-trash"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div v-else class="alert alert-light border" role="status">Your cart is empty. <RouterLink class="alert-link" to="/products">Browse products</RouterLink>.</div>
        </section>

        <!-- Summary -->
        <section v-if="items.length" class="w-100 mb-5">
            <div class="row justify-content-end">
                <div class="col-md-6">
                    <div class="summary-box shadow">
                        <div class="d-flex justify-content-between">
                            <span>Items:</span>
                            <span>{{ count }}</span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span>Subtotal:</span>
                            <span>{{ formatCurrency(subtotal) }}</span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span>Shipping:</span>
                            <span>{{ formatCurrency(shipping) }}</span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span>GST (10%):</span>
                            <span>{{ formatCurrency(gst) }}</span>
                        </div>
                        <hr />
                        <div class="d-flex justify-content-between fw-bold">
                            <span>Total:</span>
                            <span aria-live="polite">{{ formatCurrency(total) }}</span>
                        </div>
                    </div>
                    <div class="d-flex justify-content-between mt-4">
                        <RouterLink class="btn btn-outline-secondary" to="/products">Continue shopping</RouterLink>
                        <button class="btn btn-secondary px-4" @click="checkout">Checkout</button>
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>

<script setup lang="ts">
import { formatCurrency } from "../shared/lib/helpers";
import { useCartStore } from "../stores/cartStore";
import type { BasketItemModel } from "../features/basket/models/basket.type";
import { useToast } from "../shared/composables/useToast";
import { storeToRefs } from "pinia";

// const cart = ref([]);
const cart = useCartStore();

// state & getters as refs
const { items, total, subtotal, count, shipping, gst } = storeToRefs(cart);

// actions stay on the store
const { remove, clear, updateQuantity } = cart;
const { showAlert } = useToast();

/** Increase product quantity. */
function increaseQuantity(item: BasketItemModel) {
    try {
        const quantity = (item.quantity || 0) + 1;
        updateQuantity(item.product.id, quantity);
        showAlert(`Added to cart: ${item.product.title} (x${1})`, "success");
    } catch (e) {
        showAlert("Could not save cart.", "danger");
    }
}

/** Decrease item quantity. */
function decreaseQuantity(item: BasketItemModel) {
    try {
        let quantity = item.quantity || 0;
        if (quantity > 1) updateQuantity(item.product.id, (quantity -= 1));
        showAlert(`Removed from cart: ${item.product.title} (x${1})`, "warning");
    } catch (e) {
        showAlert("Could not save cart.", "danger");
    }
}

/** Remove items from the cart. */
function removeItem(item: BasketItemModel) {
    try {
        remove(item.product.id);
        showAlert(`Removed from cart: ${item.product.title}`, "warning");
    } catch (e) {
        showAlert("Could not save cart.", "danger");
    }
}

/** Show checkout alert. */
function checkout() {
    alert("Proceed to checkout...");
}
</script>

<style scoped>
.summary-box {
    border-radius: 0.75rem;
    padding: 1rem;
    border: 1px solid #dee2e6;
    background: #f8f9fa;
}

.cart-img {
    width: 60px;
    height: 60px;
    object-fit: cover;
    border-radius: 0.5rem;
    border: 1px solid #dee2e6;
}

.quantity-input {
    width: 56px;
    text-align: center;
}
</style>
