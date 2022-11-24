namespace OrderAPI.Data.Models
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }
    }
}
