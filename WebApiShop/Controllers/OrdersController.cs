using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventDressRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService,IProductService productService)
        {
            _productService = productService;
            _orderService= orderService;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewOrderDTO>> GetId(int id)
        {
            NewOrderDTO order = await _orderService.GetOrderById(id);
            return order != null ? Ok(order) : NotFound();
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<NewOrderDTO>> Post([FromBody] OrderDTO order)
        {
             
            NewOrderDTO orderr = await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(Get), new { Id = orderr.Id }, orderr);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
