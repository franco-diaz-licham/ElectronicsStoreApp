import { useQuery } from "@tanstack/vue-query";
import { api } from "../../../shared/api/httpClient";
import { keys } from "../../../shared/api/keys";
import type { CategoryModel } from "../models/category.type";
import { transformApiResponse } from "../../../shared/lib/helpers";
import type { axioResponse } from "../../../shared/api/api.type";

export function useCategories() {
    return useQuery({
        queryKey: keys.categories(),
        queryFn: async () => {
            const response = await api.get("/data/categoriesData.json");
            return transformApiResponse<CategoryModel>(response as axioResponse<CategoryModel>);
        }
    });
}
