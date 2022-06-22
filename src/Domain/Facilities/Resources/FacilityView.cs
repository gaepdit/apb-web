using Apb.Domain.Facilities.Entities;
using Apb.Domain.Facilities.FacilityId;
using System.ComponentModel.DataAnnotations;

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
        County = facility.County;
        GeoCoordinates = facility.GeoCoordinates;
    }

    // Facility identity

    public string Id { get; }

    [Display(Name = "AIRS Number")]
    public ApbFacilityId FacilityId { get; }

    // Description

    [Display(Name = "Company name")]
    public string Name { get; }

    [Display(Name = "Facility description")]
    public string Description { get; }

    // Location

    [Display(Name = "Facility Address")]
    public Address? FacilityAddress { get; }

    [Display(Name = "County")]
    public County? County { get; }

    [Display(Name = "Geographic coordinates")]
    public GeoCoordinates? GeoCoordinates { get; }
}
