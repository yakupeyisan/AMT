namespace Core.CrossCuttingConcerns.Exceptions;

public class AuthorizationException : Exception
{
    public string TranslateKey { get; set; }
    public AuthorizationException(string message,string translateKey) : base(message)
    {
        TranslateKey = translateKey;
    }
}
public class AccessDeniedException : Exception
{
    public string TranslateKey { get; set; }
    public AccessDeniedException(string message, string translateKey) : base(message)
    {
        TranslateKey = translateKey;
    }
}