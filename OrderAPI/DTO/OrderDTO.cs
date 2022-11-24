using OrderAPI.Data.Models;

namespace OrderAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderItemDTO> OrderItens { get; set; }
    }
}