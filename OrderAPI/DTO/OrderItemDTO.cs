using OrderAPI.Data.Models;

namespace OrderAPI.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ItemValue { get; set; }
    }
}