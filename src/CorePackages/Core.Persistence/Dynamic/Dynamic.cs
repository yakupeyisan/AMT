using System.Linq;

namespace Core.Persistence.Dynamic;
public record Dynamic(IEnumerable<Sort>? Sort, Filter? Filter)
{
    public Dynamic() : this(null, null)
    {
    }
}
