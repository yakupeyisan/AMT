namespace Core.Persistence.Paging;

public class BasePageableModel
{
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public BasePageableModel() { }
    public BasePageableModel(int ındex, int size, int count, int pages, bool hasPrevious, bool hasNext)
    {
        Index = ındex;
        Size = size;
        Count = count;
        Pages = pages;
        HasPrevious = hasPrevious;
        HasNext = hasNext;
    }
}