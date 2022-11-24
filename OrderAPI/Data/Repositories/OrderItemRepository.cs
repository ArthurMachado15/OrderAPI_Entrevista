using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data.Models;

namespace OrderAPI.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public readonly OrderAPIContext _context;
        public OrderItemRepository(OrderAPIContext context)
        {
            _context = context;
        }
        public async Task<OrderItemEntity> Create(OrderItemEntity order)
        {
            _context.OrderItem.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task Delete(int id)
        {
            OrderItemEntity orderToDelete = await _context.OrderItem.FindAsync(id);

            _context.OrderItem.Remove(orderToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItemEntity>> Get()
        {
            return await _context.OrderItem.ToListAsync();
        }

        public async Task<OrderItemEntity> Get(int id)
        {
            return await _context.OrderItem.FindAsync(id);
        }

        public async Task Update(OrderItemEntity orderItem)
        {
            _context.Entry(orderItem).CurrentValues.SetValues(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
