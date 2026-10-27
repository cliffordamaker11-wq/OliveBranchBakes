using OliveBranchBakes.Models;
using OliveBranchBakes.Services;

namespace OliveBranchBakes.Forms;

public sealed class MainForm : Form
{
    private readonly DataStore _store = new();
    private BakeryData _data = new();
    private readonly ListBox _products = new();
    private readonly Label _variantTitle = new();
    private readonly Label _variantDetail = new();
    private readonly Label _status = new();

    public MainForm()
    {
        Text = "Olive Branch Bakes — Phase 3A";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(820, 540);
        Size = new Size(980, 640);
        BackColor = Color.FromArgb(250, 246, 238);
        Font = new Font("Segoe UI", 10);

        var heading = new Label
        {
            Text = "Olive Branch Bakes",
            Font = new Font("Georgia", 22, FontStyle.Bold),
            ForeColor = Color.FromArgb(75, 91, 66),
            AutoSize = true,
            Location = new Point(24, 20)
        };
        var subheading = new Label
        {
            Text = "Phase 3A • Product models, saved seed data, and variant card",
            AutoSize = true,
            Location = new Point(27, 63),
            ForeColor = Color.FromArgb(100, 91, 79)
        };

        var productsLabel = new Label { Text = "Bakery products", AutoSize = true, Location = new Point(28, 112), Font = new Font(Font, FontStyle.Bold) };
        _products.Location = new Point(28, 142);
        _products.Size = new Size(330, 300);
        _products.Font = new Font("Segoe UI", 11);
        _products.SelectedIndexChanged += (_, _) => ShowSelectedProduct();

        var card = new Panel
        {
            Location = new Point(390, 142),
            Size = new Size(530, 300),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(20)
        };
        _variantTitle.Location = new Point(20, 20);
        _variantTitle.Size = new Size(475, 40);
        _variantTitle.Font = new Font("Georgia", 18, FontStyle.Bold);
        _variantTitle.ForeColor = Color.FromArgb(75, 91, 66);
        _variantDetail.Location = new Point(20, 72);
        _variantDetail.Size = new Size(475, 180);
        _variantDetail.Font = new Font("Segoe UI", 11);
        card.Controls.Add(_variantTitle);
        card.Controls.Add(_variantDetail);

        var saveButton = new Button { Text = "Save data", Location = new Point(28, 465), Size = new Size(140, 42) };
        saveButton.Click += (_, _) => SaveData();
        var reloadButton = new Button { Text = "Reload saved data", Location = new Point(180, 465), Size = new Size(175, 42) };
        reloadButton.Click += (_, _) => ReloadData();
        var calcButton = new Button { Text = "Show pricing test plan", Location = new Point(367, 465), Size = new Size(220, 42) };
        calcButton.Click += (_, _) => ShowTestPlan();

        _status.Location = new Point(28, 525);
        _status.Size = new Size(890, 42);
        _status.ForeColor = Color.FromArgb(75, 91, 66);

        Controls.AddRange(new Control[] { heading, subheading, productsLabel, _products, card, saveButton, reloadButton, calcButton, _status });

        _data = _store.LoadOrSeed();
        RefreshProducts();
        _status.Text = $"Loaded {_data.Products.Count} products. Data file: {_store.FilePath}";
    }

    private void RefreshProducts()
    {
        var selected = _products.SelectedIndex;
        _products.Items.Clear();
        foreach (var product in _data.Products)
            _products.Items.Add(product);
        _products.DisplayMember = nameof(BakeryProduct.Name);
        if (_products.Items.Count > 0)
            _products.SelectedIndex = Math.Clamp(selected, 0, _products.Items.Count - 1);
    }

    private void ShowSelectedProduct()
    {
        if (_products.SelectedItem is not BakeryProduct product) return;
        _variantTitle.Text = product.Name;
        var lines = new List<string>
        {
            product.Description,
            "",
            $"Base price: {product.BasePrice:C}",
            "Available variants:"
        };
        lines.AddRange(product.Variants.Select(v => $"• {v.Name} ({v.Size}) — adjustment {v.PriceAdjustment:+0.00;-0.00;0.00}"));
        _variantDetail.Text = string.Join(Environment.NewLine, lines);
    }

    private void SaveData()
    {
        _store.Save(_data);
        _status.Text = $"Saved {_data.Products.Count} products and {_data.Customers.Count} customers to {_store.FilePath}";
    }

    private void ReloadData()
    {
        _data = _store.LoadOrSeed();
        RefreshProducts();
        _status.Text = $"Reload complete: {_data.Products.Count} products, {_data.Customers.Count} customers.";
    }

    private void ShowTestPlan()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "docs", "PRICING_RULES_AND_TESTS.md");
        path = Path.GetFullPath(path);
        if (File.Exists(path))
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
        else
            MessageBox.Show("Open docs/PRICING_TESTS.md in the project folder to review the hand-calculated cases.",
                "Pricing test plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
