using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebApiShopContext _webApiShopContext;
        public OrderRepository(WebApiShopContext webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<Order> GetById(int id)
        {
            return await _webApiShopContext.Orders.FindAsync(id);
        }
        public async Task<Order> addOrder(Order order)
        {
            await _webApiShopContext.Orders.AddAsync(order);
            await _webApiShopContext.SaveChangesAsync();
            return order;
        }

    }
}
