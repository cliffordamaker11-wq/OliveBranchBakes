namespace OliveBranchBakes.Models;

public class ProductVariant
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "";
    public string Size { get; set; } = "";
    public decimal PriceAdjustment { get; set; }
    public bool IsAvailable { get; set; } = true;
}
