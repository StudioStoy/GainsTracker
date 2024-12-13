﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GainsTracker.Data;

public class GainsDbContextFactory : IDesignTimeDbContextFactory<GainsDbContext>
{
    public GainsDbContext CreateDbContext() => CreateDbContext([]);

    public GainsDbContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json in the Data project
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<GainsDbContext>();

        var connectionString = configuration.GetConnectionString("Development");
        optionsBuilder
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();

        return new GainsDbContext(optionsBuilder.Options);
    }
}