using MyTrade.Application.Common.Interfaces;
using MyTrade.Domain.Entities;
using MyTrade.Domain.Enums;
using MongoDB.Driver;

namespace MyTrade.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;

    public OrderRepository(IMongoDatabase database)
    {
        _orders = database.GetCollection<Order>("orders");
    }

    public async Task<Order> GetByIdAsync(string id)
    {
        var order = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        return order ?? throw new KeyNotFoundException($"Order not found for Id: {id}");
    }

    public async Task<Order> GetByOrderIdAsync(string orderId)
    {
        var order = await _orders.Find(o => o.OrderId == orderId).FirstOrDefaultAsync();
        return order ?? throw new KeyNotFoundException($"Order not found for OrderId: {orderId}");
    }

    public async Task<List<Order>> GetActiveOrdersAsync(string traderId)
    {
        var filter = Builders<Order>.Filter.And(
            Builders<Order>.Filter.Eq(o => o.TraderId, traderId),
            Builders<Order>.Filter.In(o => o.Status, new[] { OrderStatus.Pending, OrderStatus.PartiallyFilled })
        );

        return await _orders.Find(filter)
            .SortBy(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status)
    {
        return await _orders.Find(o => o.Status == status)
            .SortByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> UpdateAsync(string id, Order order)
    {
        order.UpdatedAt = DateTime.UtcNow;

        // preserve identity to avoid accidental replacement with wrong Id
        order.Id = id;

        var result = await _orders.ReplaceOneAsync(o => o.Id == id, order);

        // ModifiedCount can be 0 even if replace succeeded (identical doc). MatchedCount is the real existence check.
        return result.MatchedCount == 1;
    }

    public async Task<bool> UpdateStatusAsync(string id, OrderStatus status)
    {
        var update = Builders<Order>.Update
            .Set(o => o.Status, status)
            .Set(o => o.UpdatedAt, DateTime.UtcNow);

        var result = await _orders.UpdateOneAsync(o => o.Id == id, update);

        // ModifiedCount can be 0 if status already equals requested status.
        return result.MatchedCount == 1;
    }
}
