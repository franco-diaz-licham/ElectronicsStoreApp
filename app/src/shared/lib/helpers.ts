import type { axioResponse } from "../api/api.type";

/** Format currency to AUD. */
export function formatCurrency(v: number) {
    const AUD = new Intl.NumberFormat("en-AU", { style: "currency", currency: "AUD" });
    return AUD.format(v);
}

/** Get axio response. */
export function transformApiResponse<T>(response: axioResponse<T>) {
    return Array.isArray(response?.data) ? response.data : Array.isArray(response.data?.data) ? response.data.data : [];
}

/** format datetime to local Australian time. */
export function formatDate(date: string){
    return new Date(date).toLocaleDateString();
}
