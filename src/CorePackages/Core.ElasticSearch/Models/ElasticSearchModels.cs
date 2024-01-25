using Nest;
namespace Core.ElasticSearch.Models;

public record ElasticSearchModel(Id ElasticId, string IndexName);
public record ElasticSearchInsertUpdateModel(Id ElasticId, string IndexName, object Item) : ElasticSearchModel(ElasticId, IndexName);
public record ElasticSearchInsertManyModel(Id ElasticId, string IndexName, object[] Items) : ElasticSearchModel(ElasticId, IndexName);
public record ElasticSearchGetModel<T>(string ElasticId, T Item);
public record ElasticSearchConfig(string ConnectionString, string UserName, string Password);
public record SearchParameters(string IndexName, int From = 0, int Size = 10);
public record SearchByQueryParameters(string IndexName, int From, int Size, string QueryName, string Query, string[] Fields) : SearchParameters(IndexName, From, Size);
public record SearchByFieldParameters(string IndexName, int From, int Size, string FieldName, string Value) : SearchParameters(IndexName, From, Size);
public record IndexModel(string IndexName, string AliasName, int NumberOfReplicas = 3, int NumberOfShards = 3);
public record ElasticSearchResult(bool Success, string Message) : IElasticSearchResult //todo: refactor
{
    public ElasticSearchResult(bool success) : this(success, default)
    {
    }
}