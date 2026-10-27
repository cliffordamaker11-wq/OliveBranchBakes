namespace OliveBranchBakes.Models;

public class OrderItem
{
    public string ProductId { get; set; } = "";
    public string VariantId { get; set; } = "";
    public string ProductName { get; set; } = "";
    public string VariantName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
