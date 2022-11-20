﻿using Microsoft.Extensions.Configuration;
// using Index = blazor_pivottable.Pages.Index;

namespace Common;

public class Generator
{
    private static string[] topLevelStrategyOptions = { "VWAP", "TWAP", "WVOL", "ECLIPSE" };
    private static string[] strategyOptions = { "Hit", "Sweep", "Peg", "Fixing" };
    private static Random rand = new();
    private static string[] wayOptions = { "Buy", "Sell" };
    private static string[] instanceOptions = { "vm-1", "vm-paris", "vm-london", "vm-hongkong" };
    private static string[] venueOptions = { "ChiX", "ENX", "ENA-main", "GER-main" };
    private static string[] counterpartyOptions = { "cli-a", "cli-b", "cli-c" };
    private readonly int size;

    public Generator(IConfiguration config)
    {
        this.size = int.Parse(config["dataSize"]);
    }
    
    public List<MarketOrderVm> Execute()
    {
        var collection = new List<MarketOrderVm>();
        for (int i = 0; i < size; i++)
        {
            double coef = (double) i / size;
            
            collection.Add(new MarketOrderVm(Select<string>(topLevelStrategyOptions),
                Select(strategyOptions),
                Select(wayOptions),
                execNom: Math.Round(rand.NextDouble() * 1_000_000, MidpointRounding.ToZero) * coef,
                Select(instanceOptions),
                Select(counterpartyOptions),
                Select(Enum.GetValues<InstrumentType>()),
                Select(Enum.GetValues<VenueCategory>()),
                Select(venueOptions),
                Select(Enum.GetValues<VenueType>()),
                RandomDateTimeOffset()));
        }

        return collection;
    }
    private DateTimeOffset RandomDateTimeOffset()
    {
        return DateTimeOffset.UtcNow.AddSeconds(-Math.Round(rand.NextDouble() * 1_000_000, MidpointRounding.ToZero));
    }

    private static T Select<T>(T[] array)
    {
        return array[rand.Next(0, array.Length)];
    }
}

public class MarketOrderVm
{
    public MarketOrderVm(string topLevelStrategyName, string strategyName, string way, double execNom,
        string instanceId,
        string counterparty, InstrumentType instrumentType, VenueCategory venueCategory, string venueId
        , VenueType venueType, DateTimeOffset timestamp)
    {
        TopLevelStrategyName = topLevelStrategyName;
        StrategyName = strategyName;
        Way = way;
        ExecNom = execNom;
        InstanceId = instanceId;
        Counterparty = counterparty;
        InstrumentType = instrumentType;
        VenueCategory = venueCategory;
        VenueId = venueId;
        VenueType = venueType;
        Timestamp = timestamp;
        TimestampES = Timestamp.ToString("yyyyMMdd'T'hhmmssZ");
        EpochSeconds = timestamp.ToUnixTimeSeconds();
    }
    public long EpochSeconds { get; set; }
    public string TimestampES { get; set; }

    public MarketOrderVm()
    {
    }
    public  DateTimeOffset Timestamp { get; set; }

    public string StrategyName { get; set; }
    public string Way { get;  set;}
    public double ExecNom { get;  set;}
    public string InstanceId { get;  set;}
    public string Counterparty { get;  set;}
    public InstrumentType InstrumentType { get;  set;}
    public VenueCategory VenueCategory { get;  set;}
    public string VenueId { get;  set;}
    public VenueType VenueType { get;  set;}

    public string TopLevelStrategyName { get; set; }
}

public enum InstrumentType
{
    Equity,
    Future,
    FutureSpread
}

public enum VenueCategory
{
    LIT,
    DARK,
    DAR_AUCTION
}

public enum VenueType
{
    Main,
    Secondary,
    InternalMarket,
    DarkPool
}