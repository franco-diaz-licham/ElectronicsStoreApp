import type { ProductCardModel, ProductModel } from "../models/product.type";

/** Maps product model to product card model. */
export function mapToProductCardModel(p: ProductModel): ProductCardModel {
    return {
        id: p.id,
        title: p.title,
        price: p.price,
        image: p.image,
        brand: p.brand,
        category: p.category,
        href: `/product-details?id=${p.id}`,
    };
}
