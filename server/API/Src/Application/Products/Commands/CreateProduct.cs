namespace API.Src.Application.Products.Commands;

public sealed record CreateProduct(string Title, string Description, string Details, int BrandId, int CategoryId, decimal Price, int Stock, IProductSpecs specs);
