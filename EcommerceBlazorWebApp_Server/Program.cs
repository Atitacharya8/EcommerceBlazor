using Ecommerce_Business.Repository;
using Ecommerce_Business.Repository.IRepository;
using Ecommerce_DataAccess.Data;
using EcommerceBlazorWebApp_Server.Data;
using EcommerceBlazorWebApp_Server.Service;
using EcommerceBlazorWebApp_Server.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();  // <interfacerepository, implementationrepository>
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>(); // <interfacerepository, implementationrepository>
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk3MDA0NkAzMjMzMmUzMDJlMzBEL2tXWk15SUdhUzU3OFhudkoxU0VERFFKMkltNUIwa0RiVlNNZEo3UFI4PQ==");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
} 

app.UseHttpsRedirection();



app.UseStaticFiles();

app.UseRouting();
SeedDatabase();

//this will configure signalR for our application
app.MapBlazorHub(); //signalR is the heart and engine of the Blazor application
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }  
}
