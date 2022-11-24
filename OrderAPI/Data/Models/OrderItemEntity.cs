namespace OrderAPI.Data.Models
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ItemValue { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
    }
}
