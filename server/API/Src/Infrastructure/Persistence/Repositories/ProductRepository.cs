namespace API.Src.Infrastructure.Persistence.Repositories;

public class ProductRepository(IDbConnectionFactory dbfactory) : IProductRepository
{
    public async Task<bool> BrandExistsAsync(int id)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<BrandsRow>(x => x.Id == id); 
    }

    public async Task<bool> CategoryExistsAsync(int id)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<CategoriesRow>(x => x.Id == id); 
    }

    public async Task<bool> TitleExistsAsync(string title)
    { 
        using var db = await dbfactory.OpenAsync(); 
        return await db.ExistsAsync<ProductsRow>(x => x.Title == title); 
    }

    public async Task AddAsync(ProductEntity p)
    {
        // Open connection and transaction
        using var db = await dbfactory.OpenAsync();
        using var tx = db.OpenTransaction();

        // Add new product
        var row = new ProductsRow
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            BrandId = p.BrandId,
            CategoryId = p.CategoryId,
            SpecsTypeId = p.SpecsTypeId,
            CreatedOnUtc = DateTime.UtcNow
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
                    WaterResistance = m.WaterResistance
                });
                break;
            case (int)SpecTypeEnum.TV:
                var t = (TvSpecs)p.Specs;
                await db.InsertAsync(new TvSpecsRow
                {
                    ProductId = p.Id,
                    ScreenSize = t.ScreenSize,
                    Resolution = t.Resolution,
                    RefreshRate = t.RefreshRate,
                    Ports = t.Ports,
                    SmartOs = t.SmartOs
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
        var sql = db.From<ProductsRow>();
        if (!string.IsNullOrWhiteSpace(query)) sql.Where(x => x.Title.Contains(query) || x.Description.Contains(query));
        sql.OrderByDescending(x => x.CreatedOnUtc).Limit(skip, take);
        var rows = await db.LoadSelectAsync(sql);

        // Calculate output
        var items = rows.Select(row =>
        {
            var job = Task.Run(() => GetSpecs(db, row.Id, (SpecTypeEnum)row.SpecsTypeId));
            var data = new ProductCreationData(row.Title, row.Description, row.Details, row.BrandId, row.CategoryId, row.Price, row.Stock, job.Result);
            return ProductEntity.Create(data);
        }).ToList();
        return (items, rows.Count);
    }

    public async Task<ProductEntity?> GetAsync(int id)
    {
        // Read and validate
        using var db = await dbfactory.OpenAsync();
        var row = await db.LoadSingleByIdAsync<ProductsRow>(id);
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
            SpecTypeEnum.Gaming => await LoadGaming(db, id),
            SpecTypeEnum.PC => await LoadPC(db, id),
            _ => throw new InvalidOperationException("Unsupported type")
        };

        return specs;
    }

    public async Task DecreaseStockAsync(int productId, int qty)
    {
        using var db = await dbfactory.OpenAsync();
        var r = await db.SingleByIdAsync<ProductsRow>(productId) ?? throw new InvalidOperationException("Product not found");
        if (r.Stock < qty) throw new InvalidOperationException("Insufficient stock");
        r.Stock -= qty;
        await db.UpdateAsync(r);
    }

    private static async Task<MobileSpecs> LoadMobile(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<MobileSpecsRow>(id) ?? throw new InvalidOperationException("Mobile specs missing");
        return new MobileSpecs(row.ScreenSize, row.Storage, row.Battery, row.Connectivity, row.WaterResistance);
    }

    private static async Task<TvSpecs> LoadTv(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<TvSpecsRow>(id) ?? throw new InvalidOperationException("TV specs missing");
        return new TvSpecs(row.ScreenSize, row.Resolution, row.RefreshRate, row.Ports, row.SmartOs);
    }

    private static async Task<GamingSpecs> LoadGaming(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<GamingSpecsRow>(id) ?? throw new InvalidOperationException("Gaming specs missing");
        return new GamingSpecs(row.Resolution, row.Storage, row.Battery, row.Features, row.BackwardCompatibility);
    }

    private static async Task<PcSpecs> LoadPC(IDbConnection db, int id)
    {
        var row = await db.SingleByIdAsync<PcSpecs>(id) ?? throw new InvalidOperationException("PC specs missing");
        return new PcSpecs(row.Storage, row.Memory, row.Ports, row.Battery, row.Wireless, row.CPU);
    }
}
