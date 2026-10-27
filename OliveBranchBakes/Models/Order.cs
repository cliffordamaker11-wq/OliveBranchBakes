namespace OliveBranchBakes.Models;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string CustomerId { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Draft";
    public List<OrderItem> Items { get; set; } = new();
}
