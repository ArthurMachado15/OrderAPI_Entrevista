namespace OrderAPI.Data.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public ICollection<OrderItemEntity> OrderItens { get; set; }    
    }
}
