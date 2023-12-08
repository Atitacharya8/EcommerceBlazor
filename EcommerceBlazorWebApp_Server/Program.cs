using Ecommerce_Business.Repository;
using Ecommerce_Business.Repository.IRepository;
using Ecommerce_DataAccess.Data;
using EcommerceBlazorWebApp_Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>(); //<interfacerepository, implementationrepository>
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

//this will configure signalR for our application
app.MapBlazorHub(); //signalR is the heart and engine of the Blazor application
app.MapFallbackToPage("/_Host");

app.Run();
