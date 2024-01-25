using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class DeviceErrorStatusLog : Entity<Guid>
{
    public Guid ErrorId { get; set; }
    [MaxLength(20)]
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedId { get; set; }
}