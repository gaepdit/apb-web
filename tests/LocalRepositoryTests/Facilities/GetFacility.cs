using Apb.Domain.Facilities.FacilityId;
using Apb.LocalRepository.Facilities;
using Apb.TestData;

namespace LocalRepositoryTests.Facilities;

public class GetFacility
{
    [Test]
    public async Task ReturnsFacilityIfExists()
    {
        var facility = FacilityData.Facilities.First();
        var repository = new FacilityRepository();

        var result = await repository.GetFacilityAsync(facility.FacilityId);

        result.Should().BeEquivalentTo(facility);
    }

    [Test]
    public async Task ReturnsNullIfNotExists()
    {
        var repository = new FacilityRepository();
        var result = await repository.GetFacilityAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));
        result.Should().BeNull();
    }
}
