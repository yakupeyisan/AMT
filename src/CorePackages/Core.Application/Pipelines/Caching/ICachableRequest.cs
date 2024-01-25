namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    bool BypassCache { get; }
    CachableKeyParam CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }
}
