using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Apb.Domain.Facilities.Models;

public record ApbFacilityId
{
    public ApbFacilityId(string id) =>
        ShortString = IsValidAirsNumberFormat(id)
            ? GetNormalizedAirsNumber(id)
            : throw new ArgumentException($"{id} is not a valid AIRS number.");

    // Properties

    public string FacilityId => $"{ShortString[..3]}-{ShortString[3..8]}";

    [JsonIgnore]
    public string ShortString { get; }

    [JsonIgnore]
    public string EpaFacilityIdentifier => $"GA00000013{ShortString}";

    // Operators & overrides

    public static implicit operator ApbFacilityId(string id) => new(id);

    public override string ToString() => FacilityId;

    // Static methods

    private const string AirsNumberPattern = @"^\d{3}-?\d{5}$";
    public static bool IsValidAirsNumberFormat(string id) => Regex.IsMatch(id, AirsNumberPattern);

    private static string GetNormalizedAirsNumber(string id) => id.Replace("-", "");
}
