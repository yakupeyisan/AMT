﻿namespace Core.Application.Pipelines.Caching;

public interface ICacheRemoverRequest
{
    bool BypassCache { get; }
    List<string> Keys { get; }
}
