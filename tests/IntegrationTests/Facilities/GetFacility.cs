using Apb.Domain.Facilities.FacilityId;
using Apb.TestData;
using InfrastructureTests;

namespace IntegrationTests.Facilities;

public class GetFacility
{
    [Test]
    public async Task ReturnsFacilityIfExists()
    {
        var facility = FacilityData.Facilities.First();
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();

        var result = await repository.GetFacilityAsync(facility.FacilityId);

        result.Should().BeEquivalentTo(facility);
    }

    [Test]
    public async Task ReturnsNullIfNotExists()
    {
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();
        var result = await repository.GetFacilityAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));
        result.Should().BeNull();
    }
}
