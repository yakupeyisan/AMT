using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class CustomerContact : Entity<Guid>
{
    public Guid CustomerId { get; set; }
    [MaxLength(100)]
    public string FullName { get; set; }
    [MaxLength(250)]
    public string Email { get; set; }
    [MaxLength(50)]
    public string Phone { get; set; }

}
