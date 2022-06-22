using Apb.Domain.Data;
using Apb.Domain.Facilities.Entities;
using Apb.Domain.Facilities.FacilityId;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Apb.Domain.Facilities.Resources;

public class FacilityView
{
    public FacilityView(Facility facility)
    {
        Id = facility.Id;
        FacilityId = facility.FacilityId;
        Name = facility.Name;
        Description = facility.Description;
        FacilityAddress = facility.FacilityAddress;
        GeoCoordinates = facility.GeoCoordinates;
    }

    // Facility identity

    [JsonPropertyName("facilityId")]
    public string Id { get; }

    [JsonIgnore]
    [Display(Name = "AIRS Number")]
    public ApbFacilityId FacilityId { get; }

    // Description

    [Display(Name = "Company name")]
    public string Name { get; }

    [Display(Name = "Facility description")]
    public string Description { get; }

    // Location

    public County County => CountyData.Counties[FacilityId.CountyCode];

    [Display(Name = "Facility Address")]
    public Address? FacilityAddress { get; }

    [Display(Name = "Geographic coordinates")]
    public GeoCoordinates? GeoCoordinates { get; }
}
