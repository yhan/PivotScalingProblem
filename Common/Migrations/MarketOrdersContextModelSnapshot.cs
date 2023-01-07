﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestProject;

#nullable disable

namespace TestProject.Migrations
{
    [DbContext(typeof(MarketOrdersContext))]
    partial class MarketOrdersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Common.MarketOrderVm", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Counterparty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("EpochSeconds")
                        .HasColumnType("bigint");

                    b.Property<double>("ExecNom")
                        .HasColumnType("double precision");

                    b.Property<string>("InstanceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InstrumentType")
                        .HasColumnType("integer");

                    b.Property<string>("StrategyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TimestampES")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TopLevelStrategyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VenueCategory")
                        .HasColumnType("integer");

                    b.Property<string>("VenueId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VenueType")
                        .HasColumnType("integer");

                    b.Property<string>("Way")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstanceId");

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("InstanceId"), new[] { "Timestamp" });

                    b.ToTable("MarketOrderVms");
                });
#pragma warning restore 612, 618
        }
    }
}