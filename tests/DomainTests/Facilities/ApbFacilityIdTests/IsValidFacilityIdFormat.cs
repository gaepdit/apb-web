using Apb.Domain.Facilities.FacilityId;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class IsValidFacilityIdFormat
{
    [Test]
    [TestCaseSource(nameof(ValidFacilityIds))]
    public void AcceptsValidAirsNumbers(string airs)
    {
        ApbFacilityId.IsValidFacilityIdFormat(airs).Should().BeTrue();
    }

    [Test]
    [TestCaseSource(nameof(InvalidFacilityIds))]
    public void RejectsInvalidAirsNumbers(string airs)
    {
        ApbFacilityId.IsValidFacilityIdFormat(airs).Should().BeFalse();
    }

    private static IEnumerable<string> ValidFacilityIds()
    {
        yield return "00100001";
        yield return "001-00001";
    }

    private static IEnumerable<string> InvalidFacilityIds()
    {
        yield return "";
        yield return "111";
        yield return "abc";
        yield return "0010001";
        yield return "001000001";
        yield return "04130010001";
        yield return "001-0001";
        yield return "01-00001";
        yield return "0001-00001";
    }
}
