using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apb.Domain.ValueObjects;

[Owned]
public record GeoCoordinates
{
    [Column(TypeName = "decimal(8, 6)")]
    public decimal Latitude { get; init; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal Longitude { get; init; }
}
