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
        var repo = new FacilityRepository();

        var result = await repo.GetFacilityAsync(facility.FacilityId);

        result.Should().BeEquivalentTo(facility);
    }

    [Test]
    public async Task ReturnsNullIfNotExists()
    {
        var repo = new FacilityRepository();
        var result = await repo.GetFacilityAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));
        result.Should().BeNull();
    }
}
