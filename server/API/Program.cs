var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseServiceStack(new AppHost());
app.UseHttpsRedirection();
app.Run();
