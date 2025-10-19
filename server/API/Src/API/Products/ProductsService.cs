namespace API.Src.API.Products;

public class ProductsService(CreateProductHandler createHandler, GetProductsHandler getProductsHandler, GetProductHandler getProductHandler) : Service
{
    private readonly CreateProductHandler _createHandler = createHandler;
    private readonly GetProductsHandler _getProductsHandler = getProductsHandler;
    private readonly GetProductHandler _getProductHandler = getProductHandler;

    public async Task<IdResponse> Post(CreateProductRequest request)
    {
        if (request.Specs is null) throw new ArgumentException("specs is null!");
        try
        {
            var specs = SpecsMapper.MapSpecs(request.Specs, request.CategoryId);
            var id = await _createHandler.Handle(new CreateProduct(request.Title, request.Description, request.Details, request.BrandId, request.CategoryId, request.Price, request.Stock, specs));
            return new IdResponse { Id = id };
        }
        catch
        {
            throw;
        }
    }

    public async Task<GetProductsResponse> Get(GetProductsRequest query)
    {
        var (items, total) = await _getProductsHandler.Handle(new GetProducts(query.Page, query.PageSize, query.Query));
        return new GetProductsResponse { Total = total, Items = items.Cast<object>().ToArray() };
    }

    public async Task<GetProductResponse> Get(GetProductRequest query)
    {
        var model = await _getProductHandler.Handle(new GetProduct(query.Id)) ?? throw HttpError.NotFound("Product not found");
        return model.ConvertTo<GetProductResponse>();
    }
}
