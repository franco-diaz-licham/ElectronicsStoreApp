using API.Src.Application.Interfaces;

namespace API.Src.Application.Products.Commands;

public class CreateProductHandler(IProductRepository repo)
{
    public async Task<int> Handle(CreateProduct command)
    {
        // Validate
        if (!await repo.BrandExistsAsync(command.BrandId)) throw new InvalidOperationException("Brand not found");
        if (!await repo.CategoryExistsAsync(command.CategoryId)) throw new InvalidOperationException("Category not found");
        if (await repo.TitleExistsAsync(command.Sku)) throw new InvalidOperationException("SKU already exists");

        var product = ProductEntity.Create(command.Sku, command.Name, command.BrandId, command.CategoryId, command.Price, command.StockQty);
        await repo.AddAsync(product);
        return product.Id;
    }
}
