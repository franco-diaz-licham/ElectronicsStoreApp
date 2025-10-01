import { useQuery } from "@tanstack/vue-query";
import { api } from "../../../shared/api/httpClient";
import { keys } from "../../../shared/api/keys";
import type { ProductModel } from "../models/product.type";
import type { axioResponse } from "../../../shared/api/api.type";
import { transformApiResponse } from "../../../shared/lib/helpers";

export function useProducts() {
    return useQuery<ProductModel[]>({
        queryKey: keys.products(),
        queryFn: async () => {
            const response = await api.get("/data/productsData.json");
            return transformApiResponse<ProductModel>(response as axioResponse<ProductModel>);
        }
    });
}
