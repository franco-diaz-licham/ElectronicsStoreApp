namespace API.Src.API.Products;

[Route("/products", "POST")]
public class CreateProductRequest : IReturn<IdResponse>
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Details { get; set; } = "";
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public JsonNode? Specs { get; set; }
}

[Route("/products", "GET")]
public class GetProductsRequest : IReturn<GetProductsResponse>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Query { get; set; }
}

[Route("/products/{Id}", "GET")]
public class GetProductRequest : IReturn<GetProductResponse>
{
    public int Id { get; set; }
}

public class GetProductsResponse 
{ 
    public int Total { get; set; } 
    public object[] Items { get; set; } = []; 
}

public class GetProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Details { get; set; } = "";
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public object? Specs { get; set; }
}

public class IdResponse 
{ 
    public int Id { get; set; } 
}

