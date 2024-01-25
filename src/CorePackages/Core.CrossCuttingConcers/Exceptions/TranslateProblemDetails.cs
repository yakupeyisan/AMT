using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class TranslateProblemDetails : ProblemDetails
{
    public string? TranslateKey { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
}