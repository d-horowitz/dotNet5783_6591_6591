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
using PL.Orders;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private readonly IBl bl;
    private readonly BO.Cart cart = new();
    public ProductListWindow(IBl p_bl, BO.Cart p_cart)
    {
        InitializeComponent();
        bl = p_bl;
        cart = p_cart;
        MainGrid.DataContext = new
        {
            products =  bl.Product.Read(),
            orders = bl.Order.Read()
        };
        //ProductsListView.ItemsSource = bl.Product.Read();
        //OrdersListView.ItemsSource = bl.Order.Read();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
    }

    private void CategorySelected(object sender, SelectionChangedEventArgs e)
    {
        string? category = CategorySelector.SelectedItem?.ToString();
        ProductsListView.ItemsSource = bl.Product.Read(p => p.Category.ToString() == category || category == null);
    }
    private void AddNewProduct(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, cart, false).Show();
        Close();
    }

    private void Back(object sender, RoutedEventArgs e)
    {
        new MainWindow(cart).Show();
        Close();
    }
    private void UpdateProduct(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, cart, false, ((BO.ProductForList)ProductsListView.SelectedItem).Id).Show();
        Close();
    }
    private void UpdateOrder(object sender, RoutedEventArgs e)
    {
        new OrderWindow(bl, cart, ((BO.OrderForList)OrdersListView.SelectedItem).Id, true).Show();
        Close();
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        CategorySelector.SelectedItem = null;
    }
}

