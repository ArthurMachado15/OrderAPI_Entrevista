using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data.Models;
using OrderAPI.Data.Repositories;
using OrderAPI.DTO;
using System.Net;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderItemController(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAllOrderItens()
        {
            try
            {
                IEnumerable<OrderItemEntity> OrderItemList = await _orderItemRepository.Get();

                IEnumerable<OrderItemDTO> orderItens = OrderItemList.Select(orderItem => new OrderItemDTO()
                {
                    Id = orderItem.Id,
                    ItemValue = orderItem.ItemValue,
                    Name = orderItem.Name,
                });

                return StatusCode(200, orderItens);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItemById(int id) 
        {
            try
            {
                OrderItemEntity OrderItem = await _orderItemRepository.Get(id);
                if (OrderItem == null)
                    return StatusCode(404, "Item do pedido não encontrado!");

                OrderEntity Order = await _orderRepository.Get(OrderItem.OrderId);
                if (Order == null)
                    return StatusCode(404, "Pedido não encontrado!");

                if (Order.OrderItens.Count == 1) 
                {
                    return StatusCode(400, "Não é possível excluir, pois o pedido irá ficar sem nenhum item!");
                }

                await _orderItemRepository.Delete(id);
                return StatusCode(200, "Item do pedido excluído com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateOrderItem([FromBody] OrderItemDTO orderItem)
        {
            try
            {
                if (orderItem.Id == 0)
                    return StatusCode(400, "ID com valor igual a zero!");

                OrderItemEntity entity = await _orderItemRepository.Get(orderItem.Id);

                entity.ItemValue = orderItem.ItemValue;
                entity.Name = orderItem.Name;


                await _orderItemRepository.Update(entity);
                return StatusCode(200, "Item do pedido atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItemById(int id) 
        {
            try
            {
                OrderItemEntity entity = await _orderItemRepository.Get(id);

                if (entity == null)
                {
                    return NotFound("Cliente não encontrado!");
                }

                OrderItemDTO dtoOrderItem = new OrderItemDTO()
                {
                    Id = entity.Id,
                    ItemValue = entity.ItemValue,
                    Name = entity.Name
                };

                return StatusCode(200, dtoOrderItem);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateNewOrderItem(int idPedido, [FromBody] OrderItemDTO orderItem) 
        {
            try
            {
                OrderItemEntity entity = new OrderItemEntity()
                {
                    Id = orderItem.Id,
                    ItemValue = orderItem.ItemValue,
                    Name = orderItem.Name,
                    OrderId = idPedido,
                };

                OrderItemEntity newOrderItem = await _orderItemRepository.Create(entity);
                orderItem.Id = newOrderItem.Id;

                return StatusCode(200, orderItem);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }

        }
    }
}
