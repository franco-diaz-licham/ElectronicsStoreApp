import type { axioResponse } from "../api/api.type";

/** Format currency to AUD. */
export function formatCurrency(v: number) {
    return new Intl.NumberFormat("en-AU", { style: "currency", currency: "AUD" }).format(v);
}

export function transformApiResponse<T>(response: axioResponse<T>) {
    return Array.isArray(response?.data) ? response.data : Array.isArray(response.data?.data) ? response.data.data : [];
}
