namespace Core.Persistence.Dynamic;

public record Filter(string Field, string Operator, string? Value, string? Logic, IEnumerable<Filter>? Filters)
{
    public Filter() : this(default, default, null, null, null)
    {
    }
}
