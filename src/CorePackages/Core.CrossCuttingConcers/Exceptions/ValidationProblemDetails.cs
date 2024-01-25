using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ValidationProblemDetails : TranslateProblemDetails
{
    public object? Errors { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}
