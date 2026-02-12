using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrderByUserId(int id);
        Task<List<Order>> GetOrdersByDate(DateOnly date);
        Task<Order> AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task UpdateStatusOrder(Order order);
    }
}