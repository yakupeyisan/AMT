using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uroflow.Domain.Entities;
public class VerificationCode : Entity<Guid>
{
    public Guid UserId { get; set; }
    public required string Code { get; set; }
    public DateTime ExpiredAt { get; set; }
    public bool IsUsed { get; set; }
    public bool IsSend { get; set; }
}