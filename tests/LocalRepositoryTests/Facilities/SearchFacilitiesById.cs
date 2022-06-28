using Apb.LocalRepository.Facilities;
using Apb.TestData;

namespace LocalRepositoryTests.Facilities;

public class SearchFacilitiesById
{
    [Test]
    public async Task ReturnsListIfMatchesExist()
    {
        var findString = FacilityData.Facilities.First().Id.Substring(4, 5);
        var repository = new FacilityRepository();
        var result = await repository.SearchFacilitiesById(findString);
        result.Count.Should().BePositive();
    }

    [Test]
    public async Task ReturnsEmptyListIfNoMatches()
    {
        var repository = new FacilityRepository();
        var result = await repository.SearchFacilitiesById("00000");
        result.Count.Should().Be(0);
    }
}
