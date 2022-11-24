using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly OrderAPIContext _context;
        public CustomerRepository(OrderAPIContext context)
        {
            _context = context;
        }
        public async Task<CustomerEntity> Create(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task Delete(int id)
        {
            CustomerEntity customerToDelete = await _context.Customers.FindAsync(id);

            _context.Customers.Remove(customerToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerEntity>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<CustomerEntity> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task Update(CustomerEntity customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
