using Apb.Domain.Facilities.FacilityId;
using System.Text.Json;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class ApbFacilityIdObject
{
    [Test]
    public void HasCorrectProperties()
    {
        var facilityId = new ApbFacilityId("12345678");

        Assert.Multiple(() =>
        {
            facilityId.ToString().Should().Be("123-45678");
            facilityId.FacilityId.Should().Be("123-45678");
            facilityId.ShortString.Should().Be("12345678");
            facilityId.CountyCode.Should().Be(123);
        });
    }

    [Test]
    public void HasCorrectPropertiesFromImplicitConversion()
    {
        ApbFacilityId airs = "12345678";

        Assert.Multiple(() =>
        {
            airs.ToString().Should().Be("123-45678");
            airs.FacilityId.Should().Be("123-45678");
            airs.ShortString.Should().Be("12345678");
            airs.CountyCode.Should().Be(123);
        });
    }

    [Test]
    public void ThrowsOnInvalidFacilityId()
    {
        const string facilityId = "1";
        var act = () => new ApbFacilityId(facilityId);
        act.Should().Throw<ArgumentException>().WithMessage($"{facilityId} is not a valid AIRS number.");
    }

    [Test]
    public void ThrowsOnEmptyFacilityId()
    {
        const string facilityId = "";
        var act = () => new ApbFacilityId(facilityId);
        act.Should().Throw<ArgumentException>().WithMessage("AIRS number cannot be empty.");
    }

    [Test]
    public void HasCorrectJsonSerialization()
    {
        var airs = new ApbFacilityId("12345678");
        var jsonString = JsonSerializer.Serialize(airs);
        jsonString.Should().Be(@"{""FacilityId"":""123-45678""}");
    }
}
