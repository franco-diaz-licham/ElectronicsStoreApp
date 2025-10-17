namespace API.Src.Application.Products.Commands;

public sealed record CreateProduct(string Sku, string Name, int BrandId, int CategoryId, decimal Price, int StockQty);
