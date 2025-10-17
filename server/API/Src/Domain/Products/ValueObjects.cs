namespace API.Src.Domain.Products;

public interface IProductSpecs { }

/// <summary>
/// Common specifications for the mobile and tablets products.
/// </summary>
public sealed record MobileSpecs(string ScreenSize, string Storage, string Battery, string Connectivity, string WaterResistance) : IProductSpecs;

/// <summary>
/// Common specifications for tv products.
/// </summary>
public sealed record TvSpecs(string ScreenSize, string Resolution, string RefreshRate, string Ports, string SmartOs) : IProductSpecs;

/// <summary>
/// Common specifications for gaming products.
/// </summary>
public sealed record GamingSpecs(string Compatibility, string Battery, string Connectivity, string Features, string Fov) : IProductSpecs;

/// <summary>
/// Common specifications for pc and laptop products.
/// </summary>
public sealed record PcSpecs(string Compatibility, string Battery, string Connectivity, string Features, string Fov) : IProductSpecs;


