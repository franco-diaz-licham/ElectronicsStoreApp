namespace API.Src.Application.Interfaces;

public interface IProductRepository
{
    Task<bool> BrandExistsAsync(int brandId);
    Task<bool> CategoryExistsAsync(int categoryId);
    Task<bool> TitleExistsAsync(string sku);
    Task AddAsync(ProductEntity product);
    Task<(IReadOnlyList<ProductEntity> items, int total)> GetAllAsync(int skip, int take, string? q);
    Task<ProductEntity?> GetAsync(int id);
    Task DecreaseStockAsync(int productId, int qty);
}
