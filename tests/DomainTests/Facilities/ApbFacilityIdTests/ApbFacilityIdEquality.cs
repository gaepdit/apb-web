using Apb.Domain.Facilities.FacilityId;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class ApbFacilityIdEquality
{
    [Test]
    public void IsTrueForEquivalentFacilityIds()
    {
        var facilityId1 = new ApbFacilityId("12345678");
        var facilityId2 = new ApbFacilityId("123-45678");
        
        Assert.Multiple(() =>
        {
            (facilityId1 == facilityId2).Should().BeTrue();
            facilityId1.Equals(facilityId2).Should().BeTrue();
            (facilityId1 != facilityId2).Should().BeFalse();
        });
    }

    [Test]
    public void IsFalseForDifferentFacilityIds()
    {
        var facilityId1 = new ApbFacilityId("12345678");
        var facilityId2 = new ApbFacilityId("87654321");
        
        Assert.Multiple(() =>
        {
            (facilityId1 == facilityId2).Should().BeFalse();
            facilityId1.Equals(facilityId2).Should().BeFalse();
            (facilityId1 != facilityId2).Should().BeTrue();
        });
    }
}
