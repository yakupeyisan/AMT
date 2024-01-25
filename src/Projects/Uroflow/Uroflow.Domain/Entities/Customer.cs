using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Uroflow.Domain.Entities;

public class Customer : Entity<Guid>
{
    [MaxLength(250)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Address { get; set; }
}
