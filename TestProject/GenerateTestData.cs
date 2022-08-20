using System.Globalization;
using Common;
using CsvHelper;
using CsvHelper.Configuration;

namespace TestProject;

public class GenerateTestData
{
    [Test]
    public void GenerateCSV()
    {
        var config = new Config();
        config.Add("dataSize", 1_000_000);
        
        var generator = new Generator(config);
        var marketOrderVms = generator.Execute();
        using (var writer = new StreamWriter(@"mkt-time.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<MarketOrderVmMap>();
            csv.WriteRecords(marketOrderVms);
        }
    }

    [Test]
    public void ShowESTimestamp()
    {
        TestContext.WriteLine(DateTimeOffset.UtcNow.ToString("yyyyMMdd'T'hhmmssZ"));
    }
}

public sealed class MarketOrderVmMap : ClassMap<MarketOrderVm>
{
    public MarketOrderVmMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Timestamp).Ignore();
        Map(m => m.TimestampES).Ignore();
    }
}
