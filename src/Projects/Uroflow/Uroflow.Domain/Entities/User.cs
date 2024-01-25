using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;
public class User : Entity<Guid>
{

    public Guid IdentityId { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public bool IsVerify { get; set; }

}
