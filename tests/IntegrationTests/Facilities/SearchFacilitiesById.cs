using Apb.TestData;
using InfrastructureTests;

namespace IntegrationTests.Facilities;

public class SearchFacilitiesById
{
    [Test]
    public async Task ReturnsListIfMatchesExist()
    {
        var findString = FacilityData.Facilities.First().Id.Substring(4, 5);
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();
        var result = await repository.SearchFacilitiesById(findString);
        result.Count.Should().BePositive();
    }

    [Test]
    public async Task ReturnsEmptyListIfNoMatches()
    {
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();
        var result = await repository.SearchFacilitiesById("00000");
        result.Count.Should().Be(0);
    }
}
