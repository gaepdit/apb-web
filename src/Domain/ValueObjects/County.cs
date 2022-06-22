using Microsoft.EntityFrameworkCore;

namespace Apb.Domain.ValueObjects;

[Owned]
public record County
(
    short Id,
    string Name
);
