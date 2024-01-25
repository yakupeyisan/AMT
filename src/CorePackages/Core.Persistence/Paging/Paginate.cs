namespace Core.Persistence.Paging;

public record Paginate<T>(int From, int Index, int Size, int Count, int Pages, IList<T> Items) : IPaginate<T>
{
    internal Paginate(IEnumerable<T> source, int index, int size, int from) : this(default, default, default, default, default, default)
    {
        var enumerable = source as T[] ?? source.ToArray();

        if (from > index)
            throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

        if (source is IQueryable<T> querable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = querable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = querable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        else
        {
            Index = index;
            Size = size;
            From = from;
            Count = enumerable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
    }

    internal Paginate() : this(default, default, default, default, default, new T[0])
    {
    }

    public bool HasPrevious => Index - From > 0;
    public bool HasNext => Index - From + 1 < Pages;
}

internal record Paginate<TSource, TResult>(int Index, int Size, int Count, int Pages, int From, IList<TResult> Items) : IPaginate<TResult>
{
    public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
                    int index, int size, int from) : this(default, default, default, default, default, default)
    {
        var enumerable = source as TSource[] ?? source.ToArray();

        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

        if (source is IQueryable<TSource> queryable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = queryable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();
            Items = new List<TResult>(converter(items));
        }
        else
        {
            Index = index;
            Size = size;
            From = from;
            Count = enumerable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();
            Items = new List<TResult>(converter(items));
        }
    }


    public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter) : this(source.Index, source.Size, source.Count, source.Pages, source.From, new List<TResult>(converter(source.Items)))
    {
    }

    public bool HasPrevious => Index - From > 0;

    public bool HasNext => Index - From + 1 < Pages;
}

public static class Paginate
{
    public static IPaginate<T> Empty<T>()
    {
        return new Paginate<T>();
    }

    public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source,
                                                            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
    {
        return new Paginate<TSource, TResult>(source, converter);
    }
}