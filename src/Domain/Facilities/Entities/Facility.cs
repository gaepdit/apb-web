using Apb.Domain.Facilities.FacilityId;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apb.Domain.Facilities.Entities;

public class Facility : IAuditable
{
    private ApbFacilityId? _facilityId;

    [UsedImplicitly]
    internal Facility() { }

    public Facility(string facilityId) => Id = ApbFacilityId.NormalizeFacilityId(facilityId);

    public Facility(ApbFacilityId facilityId)
    {
        Id = facilityId.FacilityId;
        _facilityId = facilityId;
    }

    // Facility identity

    [Key]
    [StringLength(9)]
    public string Id { get; [UsedImplicitly] init; } = null!;

    [NotMapped]
    public ApbFacilityId FacilityId => _facilityId ??= new ApbFacilityId(Id);

    // Description

    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    // Location

    public Address? FacilityAddress { get; init; }
    public County? County { get; init; }
    public GeoCoordinates? GeoCoordinates { get; init; }
}
