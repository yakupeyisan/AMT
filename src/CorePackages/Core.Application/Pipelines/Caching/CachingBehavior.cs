﻿using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    private readonly CacheSettings _cacheSettings;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger,IConfiguration configuration)
    {
        _cache = cache;
        _logger = logger;
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        if (request.BypassCache) return await next();

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            TimeSpan? slidingExpiration =
                request.SlidingExpiration ?? TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration);
            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };
            byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            await _cache.SetAsync(request.CacheKey.GetKey(), serializeData, cacheOptions, cancellationToken);
            CacheHelper.Add(request.CacheKey);
            return response;
        }
        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey.GetKey(), cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            _logger.LogInformation($"Fetched from Cache -> {request.CacheKey.GetKey()}");
        }
        else
        {
            response = await GetResponseAndAddToCache();
            _logger.LogInformation($"Added to Cache -> {request.CacheKey.GetKey()}");
        }

        return response;
    }
}