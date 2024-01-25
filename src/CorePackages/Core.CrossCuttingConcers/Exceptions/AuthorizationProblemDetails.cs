using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class AuthorizationProblemDetails : TranslateProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}