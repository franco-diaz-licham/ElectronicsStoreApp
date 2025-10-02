export interface ProductModel {
    id: number;
    title: string;
    price: number;
    image: string;
    brand: string;
    category: string;
    description: string;
    details: string;
    specifications: object
}

export interface ProductCardModel {
    id: number;
    title: string;
    price: number;
    image: string;
    brand: string;
    category: string;
    href: string;
}

export interface selectorBaseModel {
    name: string;
}