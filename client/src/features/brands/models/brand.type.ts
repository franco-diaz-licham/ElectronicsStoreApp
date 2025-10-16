import type { selectorBaseModel } from "../../products/models/product.type";

export interface BrandModel extends selectorBaseModel {
    id: number;
    name: string;
    categoryIds: number[]
}