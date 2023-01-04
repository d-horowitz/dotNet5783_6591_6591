using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl;
    public ProductListWindow(IBl p_bl)
    {
        InitializeComponent();
        bl = p_bl;
        ProductsListView.ItemsSource = bl.Product.Read();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
    }

    private void CategorySelected(object sender, SelectionChangedEventArgs e)
    {
        string? category = CategorySelector.SelectedItem?.ToString();
        ProductsListView.ItemsSource = bl.Product.Read(p => p.Category.ToString() == category || category == null);
    }
    private void AddNew(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl).Show();
        Close();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
    private void Update(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, ((BO.ProductForList)ProductsListView.SelectedItem).Id).Show();
        Close();
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        CategorySelector.SelectedItem = null;
    }
}

