using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Apb.Domain.Facilities.FacilityId;

[Owned]
public record ApbFacilityId
{
    public ApbFacilityId(string facilityId)
    {
        if (!IsValidAirsNumberFormat(facilityId))
            throw new ArgumentException($"{facilityId} is not a valid AIRS number.");

        FacilityId = GetNormalizedAirsNumber(facilityId);
    }

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

    private const string AirsNumberPattern = @"^\d{3}-?\d{5}$";
    public static bool IsValidAirsNumberFormat(string facilityId) => Regex.IsMatch(facilityId, AirsNumberPattern);

    private static string GetNormalizedAirsNumber(string facilityId)
    {
        var airs = facilityId.Replace("-", "");
        return $"{airs[..3]}-{airs[3..8]}";
    }
}
