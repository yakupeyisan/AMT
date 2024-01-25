using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class DeviceError : Entity<Guid>
{
    public Guid DeviceId { get; set; }
    public DateTime ErrorDate { get; set; }
    /// <summary>
    /// Warning, Error, System, Critical
    /// </summary>
    [MaxLength(20)]
    public string Type { get; set; }
    /// <summary>
    /// Pending, Processing, Waiting , Complated, Verified
    /// </summary>
    [MaxLength(20)]
    public string Status { get; set; }
    public string Error { get; set; }
    public bool IsComlete { get; set; }
    public DateTime ComletedDate { get; set; }
    public Guid ComletedId { get; set; }
    public bool IsVerify { get; set; }
    public DateTime VerifiedDate { get; set; }
    public Guid VerifiedId { get; set; }
}
