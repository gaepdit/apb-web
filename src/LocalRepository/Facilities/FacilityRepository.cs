using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.TestData;

namespace Apb.LocalRepository.Facilities;

public sealed class FacilityRepository : IFacilityRepository
{
    public Task<bool> FacilityExistsAsync(ApbFacilityId facilityId) =>
        Task.FromResult(FacilityData.Facilities.Any(e => e.FacilityId == facilityId));

    public async Task<FacilityView?> GetFacilityAsync(ApbFacilityId facilityId) =>
        await FacilityExistsAsync(facilityId) ? FacilityData.GetFacility(facilityId) : null;

    public void Dispose()
    {
        // Method intentionally left empty.
    }
}
