using BlApi;
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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CartListWindow : Window
    {
        private readonly IBl bl;
        private BO.Cart cart = new();
        public CartListWindow(IBl p_bl, BO.Cart p_cart)
        {
            InitializeComponent();
            bl = p_bl;
            cart = p_cart;
            ProductList.ItemsSource = bl.Product.ReadCatalog();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
        }
        private void CategorySelected(object sender, RoutedEventArgs e)
        {
            string? category = CategorySelector.SelectedItem?.ToString();
            ProductList.ItemsSource = bl.Product.ReadCatalog(p => p.Category.ToString() == category || category == null);
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            new Products.ProductWindow(bl, cart, true,((BO.ProductItem)ProductList.SelectedItem).Id).Show();
            Close();
            /*try
            {
                if (ProductList.SelectedItem == null)
                    return;
                cart = bl.Cart.Create(cart, ((BO.ProductItem)ProductList.SelectedItem).Id);
                MessageBox.Show(((BO.ProductItem)ProductList.SelectedItem).Name + " was added successfully to cart", "👍 Successful Action");
                ProductList.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
                ProductList.SelectedItem = null;
            }*/
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow(cart).Show();
            Close();
        }
        private void ShowCart(object sender, RoutedEventArgs e)
        {
            new CartWindow(bl, cart).Show();
            Close();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            CategorySelector.SelectedItem = null;
        }
    }
}
