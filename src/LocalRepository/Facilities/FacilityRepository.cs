using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.TestData;

namespace Apb.LocalRepository.Facilities;

public sealed class FacilityRepository : IFacilityRepository
{
    public bool FacilityExists(ApbFacilityId facilityId) =>
        FacilityData.Facilities.Any(e => e.FacilityId == facilityId);
    public Task<FacilityExistsResult> FacilityExistsAsync(ApbFacilityId facilityId) =>
        Task.FromResult(new FacilityExistsResult(facilityId.FacilityId, FacilityExists(facilityId)));

    public Task<FacilityView?> GetFacilityAsync(ApbFacilityId facilityId) =>
        Task.FromResult(FacilityExists(facilityId) ? FacilityData.GetFacility(facilityId) : null);

    public void Dispose()
    {
        // Method intentionally left empty.
    }
}
