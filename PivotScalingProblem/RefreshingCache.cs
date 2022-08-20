using System;
using System.Collections.Generic;
using Common;
using Microsoft.Extensions.Logging;

namespace blazor_pivottable;

public class RefreshingCache
{
    private readonly Generator generator;
    private readonly Dictionary<Guid,ISubscribeToRefreshedData> map;
    private readonly ILogger<RefreshingCache> logger;

    public RefreshingCache(Generator generator, ILogger<RefreshingCache> logger)
    {
        this.generator = generator;
        map = new Dictionary<Guid, ISubscribeToRefreshedData>();
        this.logger = logger;
    }
    public void Refresh()
    {
        var marketOrders = generator.Execute();
        foreach (var subscriber in map.Values)
        {
            if (!subscriber.CanUpdateData)
            {
                logger.LogInformation($"Skip UI update, UI is busy");
                continue;
            }
            logger.LogInformation($"Start UI update");
            subscriber.UpdateAsync(marketOrders);
        }
    }

    public void Subscribe(ISubscribeToRefreshedData subscriber)
    {
        map.Add(subscriber.Id, subscriber);
    }

    public void Unsubscribe(ISubscribeToRefreshedData subscriber)
    {
        map.Remove(subscriber.Id);
    }
}