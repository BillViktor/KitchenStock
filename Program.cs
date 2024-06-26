using Blazored.LocalStorage;
using KitchenStock.Components;
using KitchenStock.Components.ViewModels;
using KitchenStock.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

#region Database
//Add DbContext and set connectionstring
builder.Services.AddDbContext<KitchenStockDbContext> (option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("KitchenStockDatabase"))); 

builder.Services.AddScoped<KitchenStockRepository>();
#endregion

//Register the ViewModel as a Service
builder.Services.AddScoped<ViewModel>();

//Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
