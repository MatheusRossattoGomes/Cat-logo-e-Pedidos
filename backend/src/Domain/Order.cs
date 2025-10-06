
namespace Domain;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; } 
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}