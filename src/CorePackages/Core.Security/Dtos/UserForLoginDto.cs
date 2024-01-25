namespace Core.Security.Dtos;

public record UserForLoginDto(string Email, string Password, string? AuthenticatorCode);
