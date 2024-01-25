namespace Uroflow.Application.Features.IdentityFeature.Dtos;

public class CreatedIdentityDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public bool Status { get; set; }
}
