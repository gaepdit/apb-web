using Apb.Domain.Facilities.FacilityId;

namespace Apb.Domain.Facilities.Entities;

public class Facility : IAuditable
{
    [UsedImplicitly]
    internal Facility() { }

    public Facility(string facilityId) => FacilityId = new ApbFacilityId(facilityId);
    public Facility(ApbFacilityId facilityId) => FacilityId = facilityId;

    // Facility identity

    public Guid Id { get; [UsedImplicitly] init; } = Guid.NewGuid();
    public ApbFacilityId FacilityId { get; } = null!;

    // Description

    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    // Location

    public Address? FacilityAddress { get; init; }
    public County? County { get; init; }
    public GeoCoordinates? GeoCoordinates { get; init; }
}
