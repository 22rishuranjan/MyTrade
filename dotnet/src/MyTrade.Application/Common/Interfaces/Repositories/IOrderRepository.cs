
using MyTrade.Domain.Entities;
using MyTrade.Domain.Enums;

namespace MyTrade.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(string id);
    Task<Order> GetByOrderIdAsync(string orderId);
    Task<List<Order>> GetActiveOrdersAsync(string traderId);
    Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(string id, Order order);
    Task<bool> UpdateStatusAsync(string id, OrderStatus status);
}
