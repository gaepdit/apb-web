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
        var repository = new FacilityRepository();

        var result = await repository.FacilityExistsAsync(facility.FacilityId);

        Assert.Multiple(() =>
        {
            result.Exists.Should().BeTrue();
            result.FacilityId.Should().Be(facility.FacilityId.FacilityId);
        });
    }

    [Test]
    public async Task ReturnsFalseIfNotExists()
    {
        var repository = new FacilityRepository();

        var result = await repository.FacilityExistsAsync(new ApbFacilityId(FacilityData.FacilityIdThatDoesNotExist));

        Assert.Multiple(() =>
        {
            result.Exists.Should().BeFalse();
            result.FacilityId.Should().Be(FacilityData.FacilityIdThatDoesNotExist);
        });
    }
}
