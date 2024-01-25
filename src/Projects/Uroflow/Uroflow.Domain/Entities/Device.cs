using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class Device : Entity<Guid>
{
    public Guid IdentityId { get; set; }
    public Guid CustomerAreaId { get; set; }
    public Guid DeviceTypeId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string SerialNumber { get; set; }
    [MaxLength(50)]
    public string ModelYear { get; set; }
    public DateTime ProductionDate { get; set; }
    public byte VarrantlyYear { get; set; }
    [MaxLength(10)]
    public string Version { get; set; }
}
