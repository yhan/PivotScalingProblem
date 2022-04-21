using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazor_pivottable;

public interface ISubscribeToRefreshedData
{
    Guid Id { get; }
    bool CanUpdateData { get; set; }
    Task UpdateAsync(List<MarketOrderVm> marketOrders);
}
