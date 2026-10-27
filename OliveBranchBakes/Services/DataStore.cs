using System.Text.Json;
using OliveBranchBakes.Models;

namespace OliveBranchBakes.Services;

/// <summary>
/// Small JSON file store for the Phase 3A prototype. Data is written under LocalApplicationData.
/// </summary>
public sealed class DataStore
{
    private readonly string _dataDirectory;
    private readonly string _filePath;
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public DataStore(string? dataDirectory = null)
    {
        _dataDirectory = dataDirectory ??
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OliveBranchBakes");
        _filePath = Path.Combine(_dataDirectory, "bakery-data.json");
    }

    public string FilePath => _filePath;

    public BakeryData LoadOrSeed()
    {
        Directory.CreateDirectory(_dataDirectory);

        if (File.Exists(_filePath))
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<BakeryData>(json, Options);
                if (data is not null) return data;
            }
            catch (JsonException)
            {
                // A malformed file is not silently overwritten; seed data is returned for this run.
            }
        }

        var seeded = BakeryData.CreateSeedData();
        Save(seeded);
        return seeded;
    }

    public void Save(BakeryData data)
    {
        Directory.CreateDirectory(_dataDirectory);
        var json = JsonSerializer.Serialize(data, Options);
        File.WriteAllText(_filePath, json);
    }
}

public class BakeryData
{
    public List<BakeryProduct> Products { get; set; } = new();
    public List<Customer> Customers { get; set; } = new();
    public List<Order> Orders { get; set; } = new();

    public static BakeryData CreateSeedData() => new()
    {
        Products = new()
        {
            new BakeryProduct
            {
                Id = "bread-sourdough", Name = "Sourdough Loaf",
                Description = "Slow-fermented country loaf.", BasePrice = 8.00m,
                Variants = new()
                {
                    new ProductVariant { Id = "bread-standard", Name = "Standard", Size = "1 loaf", PriceAdjustment = 0m },
                    new ProductVariant { Id = "bread-large", Name = "Large", Size = "1 large loaf", PriceAdjustment = 3.00m }
                }
            },
            new BakeryProduct
            {
                Id = "cookie-box", Name = "Cookie Box",
                Description = "A mixed box of house cookies.", BasePrice = 12.00m,
                Variants = new()
                {
                    new ProductVariant { Id = "cookie-six", Name = "Six Pack", Size = "6 cookies", PriceAdjustment = 0m },
                    new ProductVariant { Id = "cookie-twelve", Name = "Dozen", Size = "12 cookies", PriceAdjustment = 9.00m }
                }
            },
            new BakeryProduct
            {
                Id = "cupcake", Name = "Vanilla Cupcakes",
                Description = "Vanilla cupcakes with buttercream.", BasePrice = 18.00m,
                Variants = new()
                {
                    new ProductVariant { Id = "cupcake-six", Name = "Half Dozen", Size = "6 cupcakes", PriceAdjustment = 0m },
                    new ProductVariant { Id = "cupcake-twelve", Name = "Dozen", Size = "12 cupcakes", PriceAdjustment = 15.00m }
                }
            }
        },
        Customers = new()
        {
            new Customer { Id = "customer-demo", FullName = "Demo Customer", Email = "demo@example.com", Phone = "555-0100" }
        },
        Orders = new()
    };
}
