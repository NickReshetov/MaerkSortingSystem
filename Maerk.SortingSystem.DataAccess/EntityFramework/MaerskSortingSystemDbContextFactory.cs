using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Maerk.SortingSystem.DataAccess.EntityFramework
{
public class MaerskSortingSystemDbContextFactory : IDesignTimeDbContextFactory<MaerskSortingSystemDbContext>
{
    private static string _connectionString;

    public MaerskSortingSystemDbContext CreateDbContext()
    {
        return CreateDbContext(null);
    }

    public MaerskSortingSystemDbContext CreateDbContext(string[] args)
    {
        if (string.IsNullOrEmpty(_connectionString))
            LoadConnectionString();
        
        var builder = new DbContextOptionsBuilder<MaerskSortingSystemDbContext>();

        builder.UseInMemoryDatabase(_connectionString);

        return new MaerskSortingSystemDbContext(builder.Options);
    }

    private static void LoadConnectionString()
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();

        builder.AddJsonFile("appsettings.json", optional: false);

        IConfigurationRoot configuration = builder.Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(_connectionString))
            throw new Exception("Can't load connection string from appsettings.json");
    }
}
}
