using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(Identity identity, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(Identity identity, string ipAddress);
}