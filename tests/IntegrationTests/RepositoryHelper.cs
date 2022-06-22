using Apb.Domain.Facilities.Repositories;
using Apb.Infrastructure.DbContexts;
using Apb.Infrastructure.Facilities;
using Apb.TestData;
using IntegrationTests;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTests;

public sealed class RepositoryHelper : IDisposable
{
    private readonly ApbDbContext _context;

    private RepositoryHelper() =>
        _context = new ApbDbContext(GlobalSetup.DbOptions!, null);

    public static RepositoryHelper CreateRepositoryHelper() => new();

    public void ClearChangeTracker() => _context.ChangeTracker.Clear();

    public async Task<IFacilityRepository> GetFacilityRepositoryAsync()
    {
        await SeedFacilityDataAsync();
        return new FacilityRepository(_context);
    }

    private async Task SeedFacilityDataAsync()
    {
        if (!await _context.Facilities.AnyAsync())
        {
            await _context.Facilities.AddRangeAsync(FacilityData.Facilities);
            await _context.SaveChangesAsync();
        }
    }

    public void Dispose() => _context.Dispose();
}
