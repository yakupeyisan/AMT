using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class AuthorityOperationClaim : Entity<Guid>
{
    public Guid AuthorityId { get; set; }
    public Guid OperationClaimId { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }
}