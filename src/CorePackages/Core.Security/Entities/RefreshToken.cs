using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Core.Security.Entities;

public class RefreshToken : Entity<Guid>
{
    public Guid IdentityId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    [MaxLength(50)]
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    [MaxLength(50)]
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }

    public string? ReasonRevoked { get; set; }
    //public bool IsExpired => DateTime.UtcNow >= Expires;
    //public bool IsRevoked => Revoked != null;
    //public bool IsActive => !IsRevoked && !IsExpired;

    public virtual Identity Identity { get; set; }

    public RefreshToken()
    {
    }

    public RefreshToken(Guid id,Guid identityId ,string token, DateTime expires, DateTime created, string createdByIp, DateTime? revoked,
                        string revokedByIp, string replacedByToken, string reasonRevoked)
    {
        Id = id;
        IdentityId = identityId;
        Token = token;
        Expires = expires;
        Created = created;
        CreatedByIp = createdByIp;
        Revoked = revoked;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
        ReasonRevoked = reasonRevoked;
    }
}
