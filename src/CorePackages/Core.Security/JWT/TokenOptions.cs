namespace Core.Security.JWT;

public record TokenOptions(string Audience, string Issuer, int AccessTokenExpiration, string SecurityKey, int RefreshTokenTTL);
