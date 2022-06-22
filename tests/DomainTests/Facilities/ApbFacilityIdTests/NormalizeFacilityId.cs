using Apb.Domain.Facilities.FacilityId;

namespace DomainTests.Facilities.ApbFacilityIdTests;

public class NormalizeFacilityId
{
    [Test]
    [TestCaseSource(nameof(ValidFacilityIds))]
    public void CorrectlyFormatsFacilityId(string input)
    {
        ApbFacilityId.NormalizeFacilityId(input).Should().Be("001-00001");
    }

    [Test]
    [TestCaseSource(nameof(InvalidFacilityIds))]
    public void ThrowsOnInvalidFacilityId(string input)
    {
        var act = () => ApbFacilityId.NormalizeFacilityId(input);
        act.Should().Throw<ArgumentException>().WithMessage($"{input} is not a valid AIRS number.");
    }

    [Test]
    public void ThrowsOnEmptyFacilityId()
    {
        var act = () => ApbFacilityId.NormalizeFacilityId(string.Empty);
        act.Should().Throw<ArgumentException>().WithMessage("AIRS number cannot be empty.");
    }

    private static IEnumerable<string> ValidFacilityIds()
    {
        yield return "00100001";
        yield return "001-00001";
    }

    private static IEnumerable<string> InvalidFacilityIds()
    {
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
