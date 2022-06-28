using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.TestData;
using System.Diagnostics.CodeAnalysis;

namespace Apb.LocalRepository.Facilities;

public sealed class FacilityRepository : IFacilityRepository
{
    private static bool FacilityExists(ApbFacilityId facilityId) =>
        FacilityData.Facilities.Any(e => e.FacilityId == facilityId);

    public Task<FacilityExistsResult> FacilityExistsAsync(ApbFacilityId facilityId) =>
        Task.FromResult(new FacilityExistsResult(facilityId.FacilityId, FacilityExists(facilityId)));

    public Task<FacilityView?> GetFacilityAsync(ApbFacilityId facilityId) =>
        Task.FromResult(FacilityExists(facilityId) ? FacilityData.GetFacility(facilityId) : null);

    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public Task<List<FacilityView>> SearchFacilitiesById(string findFacilityId) =>
        Task.FromResult(FacilityData.Facilities
            .Where(e => e.Id.Replace("-", "").Contains(findFacilityId.Replace("-", "")))
            .Select(e => new FacilityView(e)).OrderBy(e => e.Id).ToList());

    public void Dispose()
    {
        // Method intentionally left empty.
    }
}
