

using MyTrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace MyTrade.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly MongoClient _client;
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // ---------- Collections ----------
    public DbSet<Trade> Trades => Set<Trade>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Trader> Traders => Set<Trader>();
    public DbSet<MarketData> MarketData => Set<MarketData>();
    public DbSet<RiskLimit> RiskLimits => Set<RiskLimit>();
    public DbSet<Settlement> Settlements => Set<Settlement>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    // ---------- Model / Index configuration ----------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===== Trades =====
        modelBuilder.Entity<Trade>(b =>
        {
            b.HasKey(x => x.TradeId);

            b.HasIndex(x => new { x.Symbol, x.TradeDate });
            b.HasIndex(x => new { x.TraderId, x.ExecutionTime });
            b.HasIndex(x => new { x.ClientId, x.TradeDate });

            b.Property(x => x.Price).HasPrecision(18, 4);
            b.Property(x => x.TotalValue).HasPrecision(18, 4);
            b.Property(x => x.Commission).HasPrecision(18, 4);
            b.Property(x => x.Fees).HasPrecision(18, 4);
        });

        // ===== Orders =====
        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(x => x.OrderId);

            b.HasIndex(x => new { x.Status, x.CreatedAt });
            b.HasIndex(x => new { x.ClientId, x.CreatedAt });
            b.HasIndex(x => new { x.TraderId, x.Status });

            b.Property(x => x.LimitPrice).HasPrecision(18, 4);
        });

        // ===== Clients =====
        modelBuilder.Entity<Client>(b =>
        {
            b.HasKey(x => x.ClientId);
            b.HasIndex(x => new { x.Status, x.Type });
        });

        // ===== Accounts =====
        modelBuilder.Entity<Account>(b =>
        {
            b.HasKey(x => x.AccountId);
            b.HasIndex(x => new { x.ClientId, x.Status });

            b.Property(x => x.Balance).HasPrecision(18, 4);
            b.Property(x => x.AvailableBalance).HasPrecision(18, 4);
            b.Property(x => x.MarginUsed).HasPrecision(18, 4);
        });

        // ===== Positions =====
        modelBuilder.Entity<Position>(b =>
        {
            b.HasKey(x => x.PositionId);
            b.HasIndex(x => new { x.AccountId, x.Symbol }).IsUnique();
            b.HasIndex(x => new { x.Symbol, x.LastUpdated });

            b.Property(x => x.AveragePrice).HasPrecision(18, 4);
            b.Property(x => x.MarketValue).HasPrecision(18, 4);
            b.Property(x => x.UnrealizedPnL).HasPrecision(18, 4);
            b.Property(x => x.RealizedPnL).HasPrecision(18, 4);
        });

        // ===== Traders =====
        modelBuilder.Entity<Trader>(b =>
        {
            b.HasKey(x => x.TraderId);
            b.HasIndex(x => x.Email).IsUnique();
            b.HasIndex(x => new { x.Desk, x.Status });
        });

        // ===== Market Data =====
        modelBuilder.Entity<MarketData>(b =>
        {
            b.HasIndex(x => new { x.Symbol, x.Timestamp });

            b.Property(x => x.Bid).HasPrecision(18, 4);
            b.Property(x => x.Ask).HasPrecision(18, 4);
            b.Property(x => x.Last).HasPrecision(18, 4);
        });

        // ===== Risk Limits =====
        modelBuilder.Entity<RiskLimit>(b =>
        {
            b.HasKey(x => x.LimitId);
            b.HasIndex(x => new { x.EntityType, x.EntityId, x.LimitType }).IsUnique();
        });

        // ===== Settlements =====
        modelBuilder.Entity<Settlement>(b =>
        {
            b.HasKey(x => x.SettlementId);
            b.HasIndex(x => x.TradeId).IsUnique();
            b.HasIndex(x => new { x.Status, x.SettlementDate });

            b.Property(x => x.PaymentAmount).HasPrecision(18, 4);
        });

        // ===== Audit Logs =====
        modelBuilder.Entity<AuditLog>(b =>
        {
            b.HasKey(x => x.LogId);
            b.HasIndex(x => x.Timestamp);
            b.HasIndex(x => new { x.UserId, x.Timestamp });
            b.HasIndex(x => new { x.EntityType, x.EntityId });
        });
    }
}


