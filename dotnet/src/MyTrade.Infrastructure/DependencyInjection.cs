using MyTrade.Application.Common.Interfaces;
using MyTrade.Application.Services;
using MyTrade.Infrastructure.Data;
using MyTrade.Infrastructure.EmailSender;
using MyTrade.Infrastructure.Redis;
using MyTrade.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.EntityFrameworkCore.Extensions;
using StackExchange.Redis;

namespace MyTrade.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DBSettings>(configuration.GetSection("Mongo"));

        var mongoOptions = configuration
            .GetSection("Mongo")
            .Get<DBSettings>()
            ?? throw new InvalidOperationException("Mongo configuration missing");

        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(mongoOptions.ConnectionString));

        services.AddSingleton<IMongoDatabase>(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(mongoOptions.DatabaseName));

        // respositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();

        services.AddRedisInfrastructure(configuration);
        return services;
    }

    private static readonly object _redisInitLock = new();
    private static bool _redisInitialized;

    private static IServiceCollection AddRedisInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis");

        if (string.IsNullOrWhiteSpace(redisConnectionString))
        {
            throw new InvalidOperationException(
                "Redis connection string is missing. Add it to configuration under ConnectionStrings:Redis.");
        }

        // Initialize pool once (important if AddInfrastructureServices is called more than once in tests)
        lock (_redisInitLock)
        {
            if (!_redisInitialized)
            {
                RedisConnectionPool.Initialize(redisConnectionString, poolSize: 100);
                _redisInitialized = true;
            }
        }

        // Register a singleton multiplexer (your pool provides one)
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            RedisConnectionPool.Instance.GetConnection());

        // Register cache service (prefer an interface for decoupling)
        services.AddSingleton<RedisCacheService>();
        // or: services.AddSingleton<IRedisCacheService, RedisCacheService>();

        return services;
    }
}
