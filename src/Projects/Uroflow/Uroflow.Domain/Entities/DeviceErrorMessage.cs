using Core.Persistence.Repositories;

namespace Uroflow.Domain.Entities;

public class DeviceErrorMessage : Entity<Guid>
{
    public Guid ErrorId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedId { get; set; }
}
