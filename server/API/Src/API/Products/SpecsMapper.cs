namespace API.Src.API.Products;

public static class SpecsMapper
{
    /// <summary>
    /// Maps the dynamic object to a concrete specs object.
    /// </summary>
    public static IProductSpecs MapSpecs(JsonNode node, int categoryId)
    {
        var type = GetType(categoryId);
        IProductSpecs? output = null;
        if (type == SpecTypeEnum.Mobile) output = JsonSerializer.Deserialize<MobileSpecs>(node);
        else if (type == SpecTypeEnum.Gaming) output = JsonSerializer.Deserialize<GamingSpecs>(node);
        else if (type == SpecTypeEnum.PC) output = JsonSerializer.Deserialize<PcSpecs>(node);
        else if (type == SpecTypeEnum.TV) output = JsonSerializer.Deserialize<TvSpecs>(node);
        if (output is null) throw new ArgumentException("Object could not be mapped");
        return output;
    }

    /// <summary>
    /// Gets correct Spec type based on category id.
    /// </summary>
    private static SpecTypeEnum GetType(int categoryId)
    {
        var type = categoryId switch
        { 
            (int)CategoryEnum.MobilePhones => SpecTypeEnum.Mobile,
            (int)CategoryEnum.TVs => SpecTypeEnum.TV,
            (int)CategoryEnum.Gaming => SpecTypeEnum.Gaming,
            (int)CategoryEnum.Electronics => SpecTypeEnum.Mobile,
            (int)CategoryEnum.PCs => SpecTypeEnum.PC,
            (int)CategoryEnum.Laptops => SpecTypeEnum.PC,
            _ => throw new ArgumentException(),
        };

        return type;
    }
}
