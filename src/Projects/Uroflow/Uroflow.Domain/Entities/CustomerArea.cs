using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class CustomerArea : Entity<Guid>
{
    public Guid CustomerId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Location { get; set; }
}
