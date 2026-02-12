using DTOs;
using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(NewOrderDTO newOrder);
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int id);
        Task<List<OrderDTO>> GetOrderByUserId(int id);
        Task<List<OrderDTO>> GetOrdersByDate(DateOnly date);
        Task UpdateOrder(Order order, int id);
        Task UpdateStatusOrder(Order order, int statusId);
    }
}