using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> Get();
        Task<CustomerEntity> Get(int id);
        Task<CustomerEntity> Create(CustomerEntity customer);
        Task Update(CustomerEntity customer);
        Task Delete(int id);
    }
}
