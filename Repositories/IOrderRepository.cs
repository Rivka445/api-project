using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrderByUserId(int id);
        Task<List<Order>> GetOrdersByDate(DateOnly date);
        Task<List<Order>> GetAllOrders();
        Task<Order> addOrder(Order order);
    }
}