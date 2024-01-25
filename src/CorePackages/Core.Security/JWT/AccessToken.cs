namespace Core.Security.JWT;

public record AccessToken(string Token, DateTime Expiration);
