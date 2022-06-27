using Apb.ApbApp.Platform.Local;
using Apb.Domain.Facilities.Repositories;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Configure repositories and services
if (builder.Environment.IsLocalEnv())
{
    builder.Services.AddScoped<IFacilityRepository, Apb.LocalRepository.Facilities.FacilityRepository>();
}
else
{
    builder.Services.AddScoped<IFacilityRepository, Apb.Infrastructure.Facilities.FacilityRepository>();
}

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment() && !app.Environment.IsLocalEnv())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the application
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Map endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
