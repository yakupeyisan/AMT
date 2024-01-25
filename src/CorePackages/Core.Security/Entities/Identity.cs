using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities;
public class Identity : BaseTimeStampEntity<Guid>
{
    [MaxLength(32)]
    public required string UserName { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public byte[]? PasswordHash { get; set; }
    public bool Status { get; set; }

    public virtual ICollection<IdentityAuthority> UserAuthorities { get; set; }
    public virtual ICollection<IdentityOperationClaim> UserOperationClaims { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    public Identity()
    {
        UserAuthorities = new HashSet<IdentityAuthority>();
        UserOperationClaims = new HashSet<IdentityOperationClaim>();
        RefreshTokens = new HashSet<RefreshToken>();
    }

    public Identity(Guid id, string userName, byte[] passwordSalt, byte[] passwordHash,
                bool status) : this()
    {
        Id = id;
        UserName = userName;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
    }
}
