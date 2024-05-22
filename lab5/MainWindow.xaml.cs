using System.Windows;
using lab5.Context;

namespace lab5;

public partial class MainWindow : Window
{
    private readonly MyDbContext _context = new MyDbContext();
    
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        ProductsDataGrid.ItemsSource = _context.Products.ToList();

        UnitsOfMeasuresDataGrid.ItemsSource = _context.UnitsOfMeasures.ToList();
        
        JoiningTablesDataGrid.ItemsSource = _context.Products.Join(
            _context.UnitsOfMeasures,
            p => p.UnitOfMeasureId,
            u => u.Id,
            (p, u) => new
        {
            p.Id,
            p.Article,
            p.Name,
            UnitOfMeasure = u.Name,
            p.Quantity,
            p.Price
        }).ToList();
    }

    private void FindProductByNameButtonClick(object sender, RoutedEventArgs e)
    {
        var name = ProductNameTextBox.Text;
        var products = _context.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        FindProductsByNameDataGrid.ItemsSource = products;
    }

    private void FindProductByPriceButtonClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not PriceRange priceRange) return;
        
        var minPrice = priceRange.MinPrice;
        var maxPrice = priceRange.MaxPrice;
        
        var products = _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        FindProductsByPriceDataGrid.ItemsSource = products;
    }
}