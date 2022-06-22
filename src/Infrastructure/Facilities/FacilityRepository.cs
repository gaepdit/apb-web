using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Apb.Infrastructure.Facilities;

public sealed class FacilityRepository : IFacilityRepository
{
    private readonly ApbDbContext _context;

    public FacilityRepository(ApbDbContext context) => _context = context;

    public Task<bool> FacilityExistsAsync(ApbFacilityId facilityId) =>
        _context.Facilities.AsNoTracking().AnyAsync(e => e.Id == facilityId.FacilityId);

    public async Task<FacilityView?> GetFacilityAsync(ApbFacilityId facilityId)
    {
        var item = await _context.Facilities.AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == facilityId.FacilityId).ConfigureAwait(false);
        return item is null ? null : new FacilityView(item);
    }

    public void Dispose() => _context.Dispose();
}
