namespace Core.Persistence.Dynamic;

public record Sort(string Field, string Dir)
{
    public Sort() : this(default, default)
    {
    }
}