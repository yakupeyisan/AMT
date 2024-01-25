using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class IdentityOperationClaim : Entity<Guid>
{
    public Guid IdentityId { get; set; }
    public Guid OperationClaimId { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }

    public IdentityOperationClaim()
    {
    }

    public IdentityOperationClaim(Guid id, Guid identityId, Guid operationClaimId) : base(id)
    {
        IdentityId = identityId;
        OperationClaimId = operationClaimId;
    }
}
