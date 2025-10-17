namespace API.Src.Application.Products.Queries;

public record GetProducts(int Page = 1, int PageSize = 20, string? Query = null);

