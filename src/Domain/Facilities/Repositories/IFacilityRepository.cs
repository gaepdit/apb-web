using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Resources;

namespace Apb.Domain.Facilities.Repositories;

public interface IFacilityRepository : IDisposable
{
    Task<FacilityExistsResult> FacilityExistsAsync(ApbFacilityId facilityId);
    Task<FacilityView?> GetFacilityAsync(ApbFacilityId facilityId);
}
