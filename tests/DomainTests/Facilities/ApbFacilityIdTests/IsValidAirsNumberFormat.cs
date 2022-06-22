using Apb.Domain.Facilities.FacilityId;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class IsValidAirsNumberFormat
{
    [Test]
    [TestCaseSource(nameof(InvalidAirsNumbers))]
    public void RejectsInvalidAirsNumbers(string airs)
    {
        ApbFacilityId.IsValidAirsNumberFormat(airs).Should().BeFalse();
    }

    [Test]
    [TestCaseSource(nameof(ValidAirsNumbers))]
    public void AcceptsValidAirsNumbers(string airs)
    {
        ApbFacilityId.IsValidAirsNumberFormat(airs).Should().BeTrue();
    }

    private static IEnumerable<string> InvalidAirsNumbers()
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

    private static IEnumerable<string> ValidAirsNumbers()
    {
        yield return "00100001";
        yield return "001-00001";
    }
}
