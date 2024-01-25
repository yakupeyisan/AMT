using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class IdentityAuthority : Entity<Guid>
{
    public Guid IdentityId { get; set; }
    public Guid AuthorityId { get; set; }
    public virtual Authority Authority { get; set; }

    public IdentityAuthority()
    {
    }

    public IdentityAuthority(Guid id, Guid identityId, Guid authorityId) : base(id)
    {
        IdentityId = identityId;
        AuthorityId = authorityId;
    }
}
