namespace API.Src.Application.Products.Queries;

public class GetProductsHandler(IProductRepository repo)
{
    public async Task<(IReadOnlyList<object> items, int total)> Handle(GetProducts q)
    {
        var skip = Math.Max(0, (q.Page - 1)) * Math.Clamp(q.PageSize, 1, 200);
        var take = Math.Clamp(q.PageSize, 1, 200);
        var (items, total) = await repo.GetAllAsync(skip, take, q.Query);
        // Map to lightweight VM
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
