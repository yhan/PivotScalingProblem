using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace blazor_pivottable;

public class RefreshingCacheService : BackgroundService
{
    private readonly RefreshingCache cache;
    private readonly int cycleInSeconds;

    public RefreshingCacheService(RefreshingCache cache, IConfiguration config)
    {
        this.cache = cache;
        this.cycleInSeconds = int.Parse(config["cycleInSeconds"]);
    }
        
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            cache.Refresh();
            await Task.Delay(TimeSpan.FromSeconds(cycleInSeconds), stoppingToken);
        }
    }
}