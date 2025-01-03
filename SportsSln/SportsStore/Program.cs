using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportBetConnection"]
    );
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("catpage",
    "{category}/Page{productPage:int}",
    new {Controller = "Home", action = "Index"});

app.MapControllerRoute("page", "Page{productPage:int}",
    new {Controller = "Home", action = "Index", productPage = 1});

app.MapControllerRoute("category", "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new {Controller= "Home", Action = "Index", productPage = 1});

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);
app.Run();
