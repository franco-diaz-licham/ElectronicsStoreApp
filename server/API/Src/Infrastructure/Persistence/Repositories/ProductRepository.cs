namespace API.Src.Infrastructure.Persistence.Repositories;

public class ProductRepository(IDbConnectionFactory dbfactory) : IProductRepository
{
    public async Task<bool> BrandExistsAsync(int id)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<BrandRow>(x => x.Id == id); 
    }

    public async Task<bool> CategoryExistsAsync(int id)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<CategoryRow>(x => x.Id == id); 
    }

    public async Task<bool> TitleExistsAsync(string title)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<ProductRow>(x => x.Title == title); 
    }

    public async Task AddAsync(ProductEntity p)
    {
        // Open connection and transaction
        using var db = await dbfactory.OpenAsync();
        using var tx = db.OpenTransaction();

        // Add new product
        var row = new ProductRow
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            BrandId = p.BrandId,
            CategoryId = p.CategoryId,
            SpecsTypeId = p.SpecsTypeId,
            CreatedOnUTC = DateTime.UtcNow
        };
        await db.InsertAsync(row);

        // Get correct spec type
        switch (p.SpecsTypeId)
        {
            case (int)SpecTypeEnum.Mobile:
                var m = (MobileSpecs)p.Specs;
                await db.InsertAsync(new MobileSpecsRow
                {
                    ProductId = p.Id,
                    ScreenSize = m.ScreenSize,
                    Storage = m.Storage,
                    Battery = m.Battery,
                    Connectivity = m.Connectivity,
                    Water_Resistance = m.WaterResistance
                });
                break;
            case (int)SpecTypeEnum.TV:
                var t = (TvSpecs)p.Specs;
                await db.InsertAsync(new TvSpecsRow
                {
                    ProductId = p.Id,
                    ScreenSize = t.ScreenSize,
                    Resolution = t.Resolution,
                    Refresh_Rate = t.RefreshRate,
                    Ports = t.Ports,
                    Smart_Os = t.SmartOs
                });
                break;
            default: throw new InvalidOperationException("Unsupported type");
        }

        tx.Commit();
    }

    public async Task<(IReadOnlyList<ProductEntity> items, int total)> GetAllAsync(int skip, int take, string? query)
    {
        // Read and validate
        using var db = await dbfactory.OpenAsync();
        var sql = db.From<ProductRow>();
        if (!string.IsNullOrWhiteSpace(query)) sql.Where(x => x.Title.Contains(query) || x.Description.Contains(query));
        sql.OrderByDescending(x => x.CreatedOnUTC).Limit(skip, take);
        var rows = await db.SelectAsync(sql);

        // Calculate output
        var total = (int)await db.CountAsync(db.From<ProductRow>().Where(sql.WhereExpression));
        var items = rows.Select(row =>
        {
            var job = Task.Run(() => GetSpecs(db, row.Id, (SpecTypeEnum)row.SpecsTypeId));
            var data = new ProductCreationData(row.Title, row.Description, row.Details, row.BrandId, row.CategoryId, row.Price, row.Stock, job.Result);
            return ProductEntity.Create(data);
        }).ToList();
        return (items, total);
    }

    public async Task<ProductEntity?> GetAsync(int id)
    {
        // Read and validate
        using var db = await dbfactory.OpenAsync();
        var row = await db.SingleByIdAsync<ProductRow>(id);
        if (row == null) return null;

        // Map to output
        IProductSpecs specs = await GetSpecs(db, id, (SpecTypeEnum)row.SpecsTypeId);
        var data = new ProductCreationData(row.Title, row.Description, row.Description, row.BrandId, row.CategoryId, row.Price, row.Stock, specs);
        var output = ProductEntity.Create(data);
        return output;
    }

    private async Task<IProductSpecs> GetSpecs(IDbConnection db, int id, SpecTypeEnum type)
    {
        IProductSpecs specs = type switch
        {
            SpecTypeEnum.Mobile => await LoadMobile(db, id),
            SpecTypeEnum.TV => await LoadTv(db, id),
            //SpectTypeEnum.Gaming => await LoadGaming(db, id),
            //SpectTypeEnum.Pc => await LoadPc(db, id),
            _ => throw new InvalidOperationException("Unsupported type")
        };

        return specs;
    }

    public async Task DecreaseStockAsync(int productId, int qty)
    {
        using var db = await dbfactory.OpenAsync();
        var r = await db.SingleByIdAsync<ProductRow>(productId) ?? throw new InvalidOperationException("Product not found");
        if (r.Stock < qty) throw new InvalidOperationException("Insufficient stock");
        r.Stock -= qty;
        await db.UpdateAsync(r);
    }

    private static async Task<MobileSpecs> LoadMobile(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<MobileSpecsRow>(id) ?? throw new InvalidOperationException("Mobile specs missing");
        return new MobileSpecs(row.ScreenSize, row.Storage, row.Battery, row.Connectivity, row.Water_Resistance);
    }

    private static async Task<TvSpecs> LoadTv(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<TvSpecsRow>(id) ?? throw new InvalidOperationException("TV specs missing");
        return new TvSpecs(row.ScreenSize, row.Resolution, row.Refresh_Rate, row.Ports, row.Smart_Os);
    }

    private static async Task<TvSpecs> LoadGaming(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<TvSpecsRow>(id) ?? throw new InvalidOperationException("TV specs missing");
        return new TvSpecs(row.ScreenSize, row.Resolution, row.Refresh_Rate, row.Ports, row.Smart_Os);
    }

    private static async Task<TvSpecs> LoadTv(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<TvSpecsRow>(id) ?? throw new InvalidOperationException("TV specs missing");
        return new TvSpecs(row.ScreenSize, row.Resolution, row.Refresh_Rate, row.Ports, row.Smart_Os);
    }
}
