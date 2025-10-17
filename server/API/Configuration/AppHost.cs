using API.Src.Application.Interfaces;

namespace API.Configuration;

public class AppHost() : AppHostBase("Electronics API", typeof(AppHost).Assembly)
{
    public override void Configure(Container container)
    {
        // Configuration
        Plugins.Add(new ValidationFeature());
        Plugins.Add(new OpenApiFeature()); //swagger at: url/swagger-ui
        Plugins.Add(new CorsFeature(
            allowedOrigins: "*",
            allowCredentials: true,
            allowedHeaders: "content-type,authorization",
            allowedMethods: "GET,POST,PUT,DELETE,OPTIONS"));

        // Database
        var cs = AppSettings.GetString("ConnectionStrings:Default") ?? throw new InvalidOperationException("Missing conn string");
        container.Register(c => OrmLiteBootstrap.Create(cs));

        // Ports & adapters
        container.RegisterAutoWiredAs<ProductRepository, IProductRepository>();

        // Use cases
        container.RegisterAutoWired<CreateProductHandler>();
        container.RegisterAutoWired<GetProductsHandler>();

        // Migrations
        Migrations.Run(container);
    }
}