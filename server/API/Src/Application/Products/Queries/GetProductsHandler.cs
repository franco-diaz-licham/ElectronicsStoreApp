namespace API.Src.Application.Products.Queries;

public class GetProductsHandler(IProductRepository repo)
{
    public async Task<(IReadOnlyList<object> items, int total)> Handle(GetProducts query)
    {
        var skip = Math.Max(0, (query.Page - 1)) * Math.Clamp(query.PageSize, 1, 200);
        var take = Math.Clamp(query.PageSize, 1, 200);

        // fetch data and map
        var (items, total) = await repo.GetAllAsync(skip, take, query.Query);
        return (items.Select(p => new {
            p.Id,
            p.Title,
            p.Description,
            p.Price,
            p.Stock,
            p.BrandId,
            p.CategoryId
        }).ToList(), total);
    }
}
