using AutoMapper;
using DTOs;
using Entities;
using Microsoft.Data.SqlClient;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserService userService )
        {
            _userService = userService;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userService = userService;
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order? order = await _orderRepository.GetOrderById(id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task<List<OrderDTO>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrders();
            List<OrderDTO> ordersDTO = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return ordersDTO;
        }
        public async Task<List<OrderDTO>> GetOrderByUserId(int userId)
        {
            if(await _userService.GetUserById(userId) == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            var orders = await _orderRepository.GetOrderByUserId(userId);

            if (orders == null || orders.Count == 0)
                throw new InvalidOperationException("No orders found for this user.");

            List<OrderDTO> ordersDTO = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return ordersDTO;
        }
        public async Task<List<OrderDTO>> GetOrdersByDate(DateOnly date)
        {
            if (date < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Date cannot be in the past.");

            List<Order> orders = await _orderRepository.GetOrdersByDate(date);
            List<OrderDTO> ordersDTO = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return ordersDTO;
        }
        public async Task<OrderDTO> AddOrder(NewOrderDTO newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));

            Order postOrder = _mapper.Map<NewOrderDTO, Order>(newOrder);
            int sum = 0;
            foreach (var item in postOrder.OrderItems)
            {
                sum += item.Dress.Price;
            }
            if (sum != postOrder.FinalPrice)
                throw new InvalidOperationException("Final price does not match the sum of items.");

            if (postOrder.OrderDate < DateOnly.FromDateTime(DateTime.Now) || postOrder.EventDate < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Order date or event date cannot be in the past.");

            Order order = await _orderRepository.AddOrder(postOrder);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task UpdateOrder(Order order, int id)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (await _orderRepository.GetOrderById(id) == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            await _orderRepository.UpdateOrder(order);
        }
        public async Task UpdateStatusOrder(Order order, int statusId)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (statusId < 1 || statusId > 4)
                throw new ArgumentOutOfRangeException(nameof(statusId), "Status ID must be between 1 and 4.");

            order.StatusId = statusId;
            await _orderRepository.UpdateStatusOrder(order);
        }
    }
}
