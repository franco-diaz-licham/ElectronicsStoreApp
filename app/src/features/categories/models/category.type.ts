import type { selectorBaseModel } from "../../products/models/product.type";

export interface CategoryModel extends selectorBaseModel {
    id: number;
    name: string;
}