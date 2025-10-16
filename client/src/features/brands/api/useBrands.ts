import { useQuery } from "@tanstack/vue-query";
import { api } from "../../../shared/api/httpClient";
import { keys } from "../../../shared/api/keys";
import type { BrandModel } from "../models/brand.type";
import { transformApiResponse } from "../../../shared/lib/helpers";
import type { axioResponse } from "../../../shared/api/api.type";

export function useBrands() {
    return useQuery({
        queryKey: keys.brands(),
        queryFn: async () => {
            const response = await api.get("/data/brandsData.json");
            return transformApiResponse<BrandModel>(response as axioResponse<BrandModel>);
        }
    });
}
