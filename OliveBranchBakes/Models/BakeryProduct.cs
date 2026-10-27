namespace OliveBranchBakes.Models;

public class BakeryProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; } = true;
    public List<ProductVariant> Variants { get; set; } = new();
}
