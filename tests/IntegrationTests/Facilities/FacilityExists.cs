using Apb.Domain.Facilities.FacilityId;
using Apb.TestData;
using InfrastructureTests;

namespace IntegrationTests.Facilities;

public class FacilityExists
{
    [Test]
    public async Task ReturnsTrueIfExists()
    {
        var facility = FacilityData.Facilities.First();
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();

        var result = await repository.FacilityExistsAsync(facility.FacilityId);

        result.Should().BeTrue();
    }

    [Test]
    public async Task ReturnsFalseIfNotExists()
    {
        using var repository = await RepositoryHelper.CreateRepositoryHelper().GetFacilityRepositoryAsync();
        var result = await repository.FacilityExistsAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));
        result.Should().BeFalse();
    }
}
