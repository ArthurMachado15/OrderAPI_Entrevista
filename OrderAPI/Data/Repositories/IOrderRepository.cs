using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> Get();
        Task<OrderEntity> Get(int id);
        Task<OrderEntity> Create(OrderEntity customer);
        Task Update(OrderEntity customer);
        Task Delete(int id);
        Task<int> ReturnTotal(int id);
    }
}
