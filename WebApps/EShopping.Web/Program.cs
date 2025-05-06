var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services
    .AddRefitFor<ICatalogService>(builder.Configuration, "Catalog")
    .AddRefitFor<IBasketService>(builder.Configuration, "Basket")
    .AddRefitFor<IOrderingService>(builder.Configuration, "Ordering");



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
