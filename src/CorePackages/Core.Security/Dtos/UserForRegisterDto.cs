namespace Core.Security.Dtos;

public record UserForRegisterDto(string Email, string Password, string FirstName, string LastName);
