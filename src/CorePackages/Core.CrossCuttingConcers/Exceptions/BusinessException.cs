namespace Core.CrossCuttingConcerns.Exceptions;

public class BusinessException : Exception
{
    public string TranslateKey { get; set; }
    public IDictionary<string, string> Params = new Dictionary<string, string>();
    public BusinessException(string message,string translateKey) : base(message)
    {
        this.TranslateKey = translateKey;
    }
    //public BusinessException(string message) : base(message)
    //{
    //}
}