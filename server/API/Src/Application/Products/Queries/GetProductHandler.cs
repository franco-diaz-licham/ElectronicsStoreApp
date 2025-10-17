namespace API.Src.Application.Products.Queries;

public class GetProductHandler(IProductRepository repo)
{
    public readonly IProductRepository _repo = repo;

    public Task<ProductEntity?> Handle(GetProduct query) => _repo.GetAsync(query.Id);
}
