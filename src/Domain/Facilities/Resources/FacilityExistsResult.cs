namespace Apb.Domain.Facilities.Resources;

public record FacilityExistsResult
(
    string FacilityId,
    bool Exists
);
