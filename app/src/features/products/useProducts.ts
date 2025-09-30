import { useQuery } from "@tanstack/vue-query";
import { api } from "../../shared/api/httpClient";
import { keys } from "../../shared/api/keys";

export type Product = { id: string; name: string; price: number };

export function useProducts() {
    return useQuery({
        queryKey: keys.products(),
        queryFn: async () => (await api.get<Product[]>("/products")).data,
        staleTime: 60_000,
        retry: 2,
    });
}
