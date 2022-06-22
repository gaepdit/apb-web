using Apb.Domain.Facilities.Entities;
using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Resources;

// ReSharper disable StringLiteralTypo

namespace Apb.TestData;

public static class FacilityData
{
    public static FacilityView? GetFacility(ApbFacilityId facilityId)
    {
        var item = Facilities.SingleOrDefault(e => e.FacilityId == facilityId);
        return item is null ? null : new FacilityView(item);
    }

    public const string FacilityIdThatDoesNotExist = "999-99999";

    public static IEnumerable<Facility> Facilities => new List<Facility>
    {
        new("00100001")
        {
            Name = "Apple Corp",
            Description = "Apples and more",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = null,
                City = "Atlantis",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("12100021")
        {
            Name = "Banana Corp",
            Description = "Bananas and more",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite B",
                City = "Bedford Falls",
                State = "GA",
                PostalCode = "30000",
            },
        },
        new("05100149")
        {
            Name = "Cranberry Corp",
            Description = "Cranberries and more",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = null,
                City = "Coruscant",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("17900001")
        {
            Name = "Date Corp",
            Description = "Dates and times",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite D",
                City = "Duckburg",
                State = "GA",
                PostalCode = "30000",
            },
        },
        new("05900071")
        {
            Name = "Elderberry Inc.",
            Description = "Your mother was a hamster and your father smelt of elderberries!",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = null,
                City = "Emerald City",
                State = "GA",
                PostalCode = "30000",
            },
        },
        new("05700040")
        {
            Name = "Fruit Inc.",
            Description = "Nothing but fruit",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite F",
                City = "Fer-de-Lance",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("00100005")
        {
            Name = "Guava Inc.",
            Description = "Guavalicious",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite G",
                City = "Gnu York",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("24500002")
        {
            Name = "Huckleberry LLC",
            Description = "Huckleberries & Chuckleberries",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite H",
                City = "Hill Valley",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("07300003")
        {
            Name = "Indian Fig Co.",
            Description = "Prickly pears",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite I",
                City = "Isthmus City",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("11500021")
        {
            Name = "Juniper Berry Co.",
            Description = "Genièvre",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite J",
                City = "Jump City",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("15300040")
        {
            Name = "Lingonberry LLC",
            Description = "Lingonberries",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite L",
                City = "Lost City of Atlanta",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("30500001")
        {
            Name = "Muscadine Inc.",
            Description = "Jellies and Jams",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite M",
                City = "Maycomb",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
        new("31300062")
        {
            Name = "Nectarine Corp.",
            Description = "Nectarines and More",
            FacilityAddress = new Address
            {
                Street = "123 Main Street",
                Street2 = "Suite N",
                City = "North Haverbrook",
                State = "GA",
                PostalCode = "30000",
            },
            GeoCoordinates = new GeoCoordinates { Latitude = 34.1M, Longitude = -84.5M },
        },
    };
}
