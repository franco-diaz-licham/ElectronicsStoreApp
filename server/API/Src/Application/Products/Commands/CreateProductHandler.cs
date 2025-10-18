namespace API.Src.Application.Products.Commands;

public class CreateProductHandler(IProductRepository repo)
{
    public async Task<int> Handle(CreateProduct command)
    {
        // Validate
        if (!await repo.BrandExistsAsync(command.BrandId)) throw new InvalidOperationException("Brand not found");
        if (!await repo.CategoryExistsAsync(command.CategoryId)) throw new InvalidOperationException("Category not found");
        if (await repo.TitleExistsAsync(command.Title)) throw new InvalidOperationException("SKU already exists");

        var data = new ProductCreationData(command.Title, command.Description, command.Details, command.BrandId, command.CategoryId, command.Price, command.Stock, command.specs);
        var product = ProductEntity.Create(data);
        await repo.AddAsync(product);
        return product.Id;
    }
}
