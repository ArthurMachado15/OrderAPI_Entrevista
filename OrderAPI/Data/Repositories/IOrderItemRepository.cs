using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItemEntity>> Get();
        Task<OrderItemEntity> Get(int id);
        Task<OrderItemEntity> Create(OrderItemEntity orderItem);
        Task Update(OrderItemEntity orderItem);
        Task Delete(int id);
    }
}
