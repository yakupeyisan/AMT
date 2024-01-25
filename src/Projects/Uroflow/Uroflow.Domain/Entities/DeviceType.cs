using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class DeviceType : Entity<Guid>
{
    [MaxLength(50)]
    public string Name { get; set; }
}
