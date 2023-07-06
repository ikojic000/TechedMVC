using TechedRazor.Services.CoinServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechedRazor.Data;
using TechedRazor.Services.ApiServices;
using TechedRazor.Services.CoinServices.Impl;
using TechedRazor.Services.ApiServices.Impl;
using TechedRazor.Models.Validators;
using TechedRazor.Models.ViewModel;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<TechedRazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TechedRazorContext") ?? throw new InvalidOperationException("Connection string 'TechedRazorContext' not found.")));

builder.Services.AddScoped<ICoinMappingService, CoinMappingService>();
builder.Services.AddScoped<IPublicApiService, PublicApiService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<ICoinValidationService, CoinValidadionService>();
builder.Services.AddScoped<IValidator<CoinDTO>, CoinValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();