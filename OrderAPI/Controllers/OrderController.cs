using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data.Models;
using OrderAPI.Data.Repositories;
using OrderAPI.DTO;
using System.Net;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            try
            {
                IEnumerable<OrderEntity> OrderList = await _orderRepository.Get();

                IEnumerable<OrderDTO> orders = OrderList.Select(order => new OrderDTO()
                {
                    Id = order.Id,
                    Number = order.Number,
                    CustomerId = order.CustomerId
                });

                return StatusCode(200, orders);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderById(int id) 
        {
            try
            {
                if (await _orderRepository.Get(id) == null)
                    return NotFound("Pedido não encontrado!");

                await _orderRepository.Delete(id);
                return StatusCode(200, "Pedido excluído com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order.Id == 0)
                    return StatusCode(400, "ID com valor igual a zero!");

                OrderEntity entity = await _orderRepository.Get(order.Id);

                if (entity == null) 
                {
                    return StatusCode(404, "Não foi possível localizar o Pedido!");
                }

                entity.Number = order.Number;

                await _orderRepository.Update(entity);
                return StatusCode(200, "Cliente atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id) 
        {
            try
            {
                OrderEntity entity = await _orderRepository.Get(id);

                if (entity == null)
                {
                    return NotFound("Pedido não encontrado!");
                }

                OrderDTO dtoOrder = new OrderDTO()
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Number = entity.Number,
                    OrderItens = entity.OrderItens.Select(item => new OrderItemDTO()
                    {
                        Id = item.Id,
                        ItemValue = item.ItemValue,
                        Name = item.Name,
                    }).ToList()
                };

                return StatusCode(200, dtoOrder);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateNewOrder([FromBody] OrderDTO order) 
        {
            try
            {
                OrderEntity entity = new OrderEntity()
                {
                    CustomerId = order.CustomerId,
                    Number = order.Number,
                    Date = DateTime.UtcNow,
                    OrderItens = order.OrderItens.Select(item => new OrderItemEntity()
                    {
                        ItemValue = item.ItemValue,
                        Name = item.Name,
                    }).ToList()
                };

                OrderEntity newOrder = await _orderRepository.Create(entity);
                order.Id = newOrder.Id;

                return StatusCode(200, order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }

        }

        [HttpGet("total/{idPedido}")]
        public async Task<ActionResult<int>> GetSumItens(int idPedido)
        {
            return await _orderRepository.ReturnTotal(idPedido);
        }
    }
}
