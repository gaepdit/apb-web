using Apb.Infrastructure.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests;

[SetUpFixture]
public class GlobalSetup
{
    internal static DbContextOptions<ApbDbContext>? DbOptions;
    private static ApbDbContext? _context;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        // ReSharper disable once StringLiteralTypo
        var config = new ConfigurationBuilder().AddJsonFile("testsettings.json").Build();
        var dbConn = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        DbOptions = new DbContextOptionsBuilder<ApbDbContext>().UseSqlServer(dbConn).Options;

        _context = new ApbDbContext(new DbContextOptionsBuilder<ApbDbContext>().UseSqlServer(dbConn).Options, null);
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => _context?.Dispose();
}
