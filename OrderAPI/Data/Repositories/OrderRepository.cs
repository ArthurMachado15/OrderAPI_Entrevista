using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly OrderAPIContext _context;
        public OrderRepository(OrderAPIContext context)
        {
            _context = context;
        }
        public async Task<OrderEntity> Create(OrderEntity order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task Delete(int id)
        {
            OrderEntity orderToDelete = await _context.Order.FindAsync(id);

            _context.Order.Remove(orderToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderEntity>> Get()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<OrderEntity> Get(int id)
        {
            return await _context.Order.Include(item => item.OrderItens).FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task Update(OrderEntity order)
        {
            _context.Entry(order).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ReturnTotal(int id) 
        {
            return await _context.OrderItem.Where(orderItem => orderItem.OrderId == id).SumAsync(item => item.ItemValue); 
        }
    }
}
