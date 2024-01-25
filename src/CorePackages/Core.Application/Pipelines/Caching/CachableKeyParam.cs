using Newtonsoft.Json;

namespace Core.Application.Pipelines.Caching;

public record CachableKeyParam(Type ResponseType, string Key, int Page, int PageSize, Dictionary<string, string> IdList, string Query)
{
    public CachableKeyParam(Type responseType,string key, int page, int pageSize) : this(responseType,key, page, pageSize, new(), "")
    {
    }

    public string GetKey()
    {
        return $"{Key}.{Page}.{PageSize}.{Query}";
    }
    public int GetStart()
    {
        return Page * PageSize;
    }
    public int GetEnd()
    {
        return (Page + 1) * PageSize;
    }
    public void AddIdList(string jsonList)
    {
        var items = JsonConvert.DeserializeObject<List<string>>(jsonList);
        items.ForEach(e =>
        {
            if (IdList.ContainsKey(e) == false)
            {
                IdList.Add(e, e);
            }
        });
    }
    public bool CheckContains(string key)
    {
        return IdList.ContainsKey(key);
    }
    
}
