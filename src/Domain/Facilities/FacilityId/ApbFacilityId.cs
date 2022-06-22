using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Apb.Domain.Facilities.FacilityId;

[Owned]
public record ApbFacilityId
{
    public ApbFacilityId(string facilityId) => FacilityId = NormalizeFacilityId(facilityId);

    public string FacilityId { get; }

    // Read-only properties

    [JsonIgnore]
    public string ShortString => FacilityId.Replace("-", "");

    [JsonIgnore]
    public short CountyCode => Convert.ToInt16(ShortString[..3]);

    // Operators & overrides

    public static implicit operator ApbFacilityId(string facilityId) => new(facilityId);

    public override string ToString() => FacilityId;

    // Static methods

    private const string FacilityIdPattern = @"^\d{3}-?\d{5}$";
    public static bool IsValidFacilityIdFormat(string facilityId) => Regex.IsMatch(facilityId, FacilityIdPattern);

    public static string NormalizeFacilityId(string facilityId)
    {
        if(string.IsNullOrEmpty(facilityId))
            throw new ArgumentException("AIRS number cannot be empty.");
            
        if (!IsValidFacilityIdFormat(facilityId))
            throw new ArgumentException($"{facilityId} is not a valid AIRS number.");

        var id = facilityId.Replace("-", "");
        return $"{id[..3]}-{id[3..8]}";
    }
}
