using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> addOrder(Order order);
        Task<Order> GetById(int id);
    }
}