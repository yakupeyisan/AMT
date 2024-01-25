using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Core.Security.Entities;

public class Authority : Entity<Guid>
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public virtual ICollection<AuthorityOperationClaim> AuthorityOperationClaims { get; set; }
    public Authority()
    {

        AuthorityOperationClaims = new HashSet<AuthorityOperationClaim>();
    }
}
