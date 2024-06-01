using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace ApiManeroSize.Entites;

public class SizeEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string sizeTitle { get; set; } = null!;
    public string PartitionKey { get; set; } = "Sizes";
}
