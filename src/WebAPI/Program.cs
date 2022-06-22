using Apb.Domain.Facilities.Repositories;
using Apb.Infrastructure.DbContexts;
using Apb.LocalRepository.Facilities;
using Apb.WebAPI.Platform.Local;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v0", new OpenApiInfo
    {
        Version = "v0",
        Title = "Georgia Air Protection Branch Stationary Sources API",
        Contact = new OpenApiContact
        {
            Name = "Georgia EPD-IT Support",
            Email = builder.Configuration["SupportEmail"],
        },
    });
});

// Configure the database contexts, data repositories, and services
if (builder.Environment.IsLocalEnv())
{
    // Uses static data when running locally
    builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
}
else
{
    // TODO: When running on the server, requires a deployed database (connection string configured in settings)
    // builder.Services.AddDbContext<ApbDbContext>(opts =>
    //     opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    //         x => x.MigrationsAssembly("Infrastructure")));

    builder.Services.AddScoped<IFacilityRepository, Apb.Infrastructure.Facilities.FacilityRepository>();

    // TODO: Initialize database/migrations
    // builder.Services.AddHostedService<MigratorHostedService>();
}

// Build and configure the HTTP request pipeline.
var app = builder.Build();

// Configure API documentation
app.UseSwagger(opts => { opts.RouteTemplate = "api-docs/{documentName}/openapi.json"; });
app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/api-docs/v0/openapi.json", "APB API v0");
    opts.RoutePrefix = "api-docs";
    opts.DocumentTitle = "Georgia Air Protection Branch Stationary Sources API";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
