using Apb.Domain.Facilities.FacilityId;
using Apb.LocalRepository.Facilities;
using Apb.TestData;

namespace LocalRepositoryTests.Facilities;

public class FacilityExists
{
    [Test]
    public async Task ReturnsTrueIfExists()
    {
        var facility = FacilityData.Facilities.First();
        var repo = new FacilityRepository();

        var result = await repo.FacilityExistsAsync(facility.FacilityId);

        result.Should().BeTrue();
    }

    [Test]
    public async Task ReturnsFalseIfNotExists()
    {
        var repo = new FacilityRepository();
        var result = await repo.FacilityExistsAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));
        result.Should().BeFalse();
    }
}
