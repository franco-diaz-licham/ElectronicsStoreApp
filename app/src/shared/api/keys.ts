// src/api/keys.ts
export const keys = {
    products: () => ["products"] as const,
    product: (id: string) => ["product", id] as const,
    me: () => ["me"] as const,
    orders: (userId: string) => ["orders", userId] as const,
    brands: () => ["brands"] as const,
    categories: () => ["categories"] as const,
};
