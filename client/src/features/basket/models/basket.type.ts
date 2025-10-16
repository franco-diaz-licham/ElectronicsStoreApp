import type { ProductModel } from "../../products/models/product.type";

export interface BasketItemModel {
    quantity: number;
    product: ProductModel;
}
