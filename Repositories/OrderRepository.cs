using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public OrderRepository(EventDressRentalContext webApiShopContext)
        {
            _eventDressRentalContext = webApiShopContext;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).FirstOrDefaultAsync(o=>o.Id==id);
        }
        public async Task<List<Order>> GetOrderByUserId(int id)
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress)
                            .Where(o=> o.UserId == id).OrderBy(o => o.OrderDate).ToListAsync();     
        }
        public async Task<List<Order>> GetOrdersByDate(DateOnly date)
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress)
                       .Where(o => o.EventDate <= date && o.EventDate > DateOnly.FromDateTime(DateTime.Now)).OrderBy(o => o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ToListAsync();
        }
        public async Task<Order> addOrder(Order order)
        {
            await _eventDressRentalContext.Orders.AddAsync(order);
            await _eventDressRentalContext.SaveChangesAsync();
            return order;
        }

    }
}
