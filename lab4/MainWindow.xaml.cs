using System;
using System.Windows;

namespace lab4;

public partial class MainWindow : Window
{
    private readonly AdoAssistant _myTable = new AdoAssistant();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        ProductList.SelectedIndex = 0;
        ProductList.Focus();
        ProductList.DataContext = _myTable.TableLoad();
    }

    private void CreateButtonClick(object sender, RoutedEventArgs e)
    {
        if (ArticleTextBox.Text.Trim().Equals("") || NameTextBox.Text.Trim().Equals(""))
        {
            MessageBox.Show("Поля 'Article' та 'Name' не можуть бути пустими!");
            return;
        }

        _myTable.InsertIntoTable(
            ArticleTextBox.Text.Trim(),
            NameTextBox.Text.Trim(),
            UnitOfMeasureTextBox.Text.Trim(),
            ConvertToDouble(QuantityTextBox.Text.Replace(".", ",")),
            ConvertToDouble(PriceTextBox.Text.Replace(".", ","))
        );
        ProductList.DataContext = _myTable.TableLoad();
    }

    private static double? ConvertToDouble(string value)
    {
        if (double.TryParse(value, out var result))
        {
            return result;
        }

        return null;
    }

    private void UpdateButtonClick(object sender, RoutedEventArgs e)
    {
        if (ArticleTextBox.Text.Trim().Equals("") || NameTextBox.Text.Trim().Equals(""))
        {
            MessageBox.Show("Поля 'Article' та 'Name' не можуть бути пустими!");
            return;
        }

        _myTable.UpdateTable(
            Convert.ToInt32(IdTextBox.Text),
            ArticleTextBox.Text.Trim(),
            NameTextBox.Text.Trim(),
            UnitOfMeasureTextBox.Text.Trim(),
            ConvertToDouble(QuantityTextBox.Text.Replace(".", ",")),
            ConvertToDouble(PriceTextBox.Text.Replace(".", ","))
        );
        ProductList.DataContext = _myTable.TableLoad();
    }

    private void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        _myTable.DeleteFromTable(Convert.ToInt32(IdTextBox.Text));
        ProductList.DataContext = _myTable.TableLoad();
    }

    private void ClearButtonClick(object sender, RoutedEventArgs e)
    {
        ProductList.SelectedIndex = -1;
    }
}