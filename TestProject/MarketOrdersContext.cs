﻿using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestProject;

public class MarketOrdersContext : DbContext
{
    //v.11
    //public static string DBCnxString = "Host=localhost;Port=5432;Database=OrdersDB;Username=postgres;Password=postgres;Search Path=daily";
    
    //v.12
    public static string DBCnxString = "Host=localhost;Port=5433;Database=OrdersDB;Username=postgres;Password=postgres12;Search Path=daily";
    public MarketOrdersContext(DbContextOptions<MarketOrdersContext> optionsBuilderOptions)
        :base(optionsBuilderOptions)
    {
    }

    public MarketOrdersContext()
    {
    }
    
    public DbSet<MarketOrderVm> MarketOrderVms { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(DBCnxString);//v12 pwd postgres12
    }
    
    // protected override void OnModelCreating(DbModelBuilder modelBuilder)
    // {
    //     modelBuilder.HasDefaultSchema("public");
    //     base.OnModelCreating(modelBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("daily");
        modelBuilder
            .Entity<MarketOrderVm>(
            eb => {
                //eb.HasNoKey();
                // eb.ToView("View_BlogPostCounts");
                // eb.Property(v => v.BlogName).HasColumnName("Name");
                eb.HasKey(keyExpression: m => m.Id);
                NpgsqlIndexBuilderExtensions.IncludeProperties(eb.HasIndex(b => b.InstanceId), b => b.Timestamp);
            });
        base.OnModelCreating(modelBuilder);
    }
}


public class MarketOrdersContextFactory : IDesignTimeDbContextFactory<MarketOrdersContext>
{
    public MarketOrdersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MarketOrdersContext>();
        optionsBuilder.UseNpgsql(MarketOrdersContext.DBCnxString,
        builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", "daily"));
        

        return new MarketOrdersContext(optionsBuilder.Options);
    }
}
