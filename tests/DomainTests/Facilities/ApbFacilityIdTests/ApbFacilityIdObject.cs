using System.Text.Json;
using Apb.Domain.Facilities.Models;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class ApbFacilityIdObject
{
    [Test]
    public void HasCorrectlyFormattedProperties()
    {
        var airs = new ApbFacilityId("12345678");

        Assert.Multiple(() =>
        {
            airs.ToString().Should().Be("123-45678");
            airs.FacilityId.Should().Be("123-45678");
            airs.ShortString.Should().Be("12345678");
            airs.EpaFacilityIdentifier.Should().Be("GA0000001312345678");
        });
    }

    [Test]
    public void HasCorrectlyFormattedPropertiesFromImplicitConversion()
    {
        ApbFacilityId airs = "12345678";

        Assert.Multiple(() =>
        {
            airs.ToString().Should().Be("123-45678");
            airs.FacilityId.Should().Be("123-45678");
            airs.ShortString.Should().Be("12345678");
            airs.EpaFacilityIdentifier.Should().Be("GA0000001312345678");
        });
    }

    [Test]
    public void ThrowsOnInvalidAirsNumber()
    {
        const string id = "abc";
        var act = () => new ApbFacilityId(id);
        act.Should().Throw<ArgumentException>().WithMessage($"{id} is not a valid AIRS number.");
    }
    
    [Test]
    public void HasCorrectJsonSerialization()
    {
        var airs = new ApbFacilityId("12345678");
        var jsonString = JsonSerializer.Serialize(airs);
        jsonString.Should().Be(@"{""FacilityId"":""123-45678""}");
    }
}
