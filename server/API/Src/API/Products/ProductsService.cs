namespace API.Src.API.Products;

public class ProductsService(CreateProductHandler createHandler, GetProductsHandler getProductsHandler, GetProductHandler getProductHandler) : Service
{
    private readonly CreateProductHandler _createHandler = createHandler;
    private readonly GetProductsHandler _getProductsHandler = getProductsHandler;
    private readonly GetProductHandler _getProductHandler = getProductHandler;

    public async Task<IdResponse> Post(CreateProductRequest req)
    {
        var id = await _createHandler.Handle(new CreateProduct(req.Title, req.Description, req.BrandId, req.CategoryId, req.Price, req.Stock));
        return new IdResponse { Id = id };
    }

    public async Task<GetProductsResponse> Get(GetProductsRequest req)
    {
        var (items, total) = await _getProductsHandler.Handle(new GetProducts(req.Page, req.PageSize, req.Query));
        return new GetProductsResponse { Total = total, Items = items.Cast<object>().ToArray() };
    }

    public async Task<GetProductResponse> Get(GetProductRequest query)
    {
        var order = await _getProductHandler.Handle(new GetProduct(query.Id)) ?? throw HttpError.NotFound("Product not found");
        return new GetProductResponse
        {
            Id = query.Id
        };
    }
}
