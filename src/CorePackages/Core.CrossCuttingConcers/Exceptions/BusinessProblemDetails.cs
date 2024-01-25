using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class BusinessProblemDetails : TranslateProblemDetails
{
    public IDictionary<string,string> Params { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
}