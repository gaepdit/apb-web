using Apb.Domain.Facilities.Entities;
using Apb.Domain.Facilities.FacilityId;

namespace DomainTests.Facilities.FacilityConstructorTests;

public class NewFacility
{
    [Test]
    public void HasCorrectProperties()
    {
        var facilityId = new ApbFacilityId("00100000");
        var facility = new Facility(facilityId);
        facility.FacilityId.ShortString.Should().Be(facilityId.ShortString);
    }

    [Test]
    public void HasCorrectPropertiesFromImplicitConversion()
    {
        const string facilityId = "00100000";
        var facility = new Facility(facilityId);
        facility.FacilityId.ShortString.Should().Be(facilityId);
    }

    [Test]
    public void ThrowsOnInvalidAirsNumber()
    {
        const string facilityId = "1";
        var act = () => new Facility(facilityId);
        act.Should().Throw<ArgumentException>().WithMessage($"{facilityId} is not a valid AIRS number.");
    }
}
