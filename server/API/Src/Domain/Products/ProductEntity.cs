namespace API.Src.Domain.Products;

public sealed class ProductEntity : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Details { get; private set; }
    public decimal Price { get; private set; }
    public int BrandId { get; private set; }
    public BrandEntity? Brand { get; private set; }
    public int CategoryId { get; private set; }
    public CategoryEntity? Category { get; private set; }
    public int SpecsTypeId { get; private set; }
    public SpecTypesEntity? SpecTypes { get; private set; }
    public int Stock { get; private set; }
    public IProductSpecs Specs { get; private set; }
    public DateTime CreatedOn { get; private set; }

    private ProductEntity(string title, string description, string details, int brandId, int categoryId, decimal price, int stockQty, IProductSpecs product)
    {
        Title = title;
        Description = description;
        Details = details;
        BrandId = brandId;
        CategoryId = categoryId;
        SetSpecs(categoryId);
        Price = price;
        Stock = stockQty;
        CreatedOn = DateTime.UtcNow;
        Specs = product;
    }

    /// <summary>
    /// Create Mehod which validates argument input before instantiating a instance of Product.
    /// </summary>
    public static ProductEntity Create(ProductCreationData data)
    {
        if (string.IsNullOrWhiteSpace(data.Title)) throw new ArgumentException("Title required");
        if (string.IsNullOrWhiteSpace(data.Description)) throw new ArgumentException("Description required");
        if (string.IsNullOrWhiteSpace(data.Details)) throw new ArgumentException("Details required");
        if (data.Price < 0) throw new ArgumentException("Price must be >= 0");
        if (data.Stock < 0) throw new ArgumentException("Stock must be >= 0");
        return new ProductEntity(data.Title.Trim(), data.Description.Trim(), data.Details.Trim(), data.BrandId, data.CategoryId, decimal.Round(data.Price, 2), data.Stock, data.Specs);
    }

    public void DecreaseStock(int qty)
    {
        if (qty <= 0) throw new ArgumentException("qty > 0");
        if (Stock < qty) throw new InvalidOperationException("Insufficient stock");
        Stock -= qty;
    }

    /// <summary>
    /// Sets specification type based on category type. For example, Pcs will be PC spect type.
    /// </summary>
    public void SetSpecs(int categoryId)
    {
        SpecsTypeId = categoryId switch
        {
            (int)CategoryEnum.MobilePhones => (int)SpecTypeEnum.Mobile,
            (int)CategoryEnum.TVs => (int)SpecTypeEnum.TV,
            (int)CategoryEnum.Gaming => (int)SpecTypeEnum.Gaming,
            (int)CategoryEnum.Electronics => (int)SpecTypeEnum.Mobile,
            (int)CategoryEnum.PCs => (int)SpecTypeEnum.PC,
            (int)CategoryEnum.Laptops => (int)SpecTypeEnum.PC,
            _ => throw new ArgumentException(),
        };
    }

    public void SetTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title required");
        Title = value.Trim();
    }

    public void SetDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title required");
        Description = value.Trim();
    }

    public void SetDetails(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title required");
        Details = value.Trim();
    }

    public void SetPrice(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Price cannot be less than 0");
        Price = decimal.Round(value, 2);
    }

    public void SetCategory(int value, CategoryEntity? model)
    {
        if (value <= 0) throw new ArgumentException("Category Id cannot be less than 0");
        CategoryId = value;
        Category = model;
    }

    public void SetBrand(int value, BrandEntity? model)
    {
        if (value <= 0) throw new ArgumentException("Brand Id cannot be less than 0");
        BrandId = value;
        Brand = model;
    }

    public void SetStock(int value)
    {
        if (value < 0) throw new ArgumentException("Stock must be >= 0");
        Stock = value;
    }
}

/// <summary>
/// Create argument data.
/// </summary>
public sealed record ProductCreationData(string Title, string Description, string Details, int BrandId, int CategoryId, decimal Price, int Stock, IProductSpecs Specs);