using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Core.Security.Entities;

public class OperationClaim : Entity<Guid>
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }

    public OperationClaim()
    {
    }

    public OperationClaim(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public OperationClaim(Guid id, string name,string description) : this(id,name)
    {
        Description = description;
    }
}
