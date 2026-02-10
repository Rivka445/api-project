using Entities;
using DTOs;
namespace Services
{
    public interface IOrderService
    {
        Task<NewOrderDTO> AddOrder(OrderDTO order);
        Task<NewOrderDTO> GetOrderById(int id);
    }
}