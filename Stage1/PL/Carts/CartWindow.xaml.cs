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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly IBl bl;
        private BO.Cart cart;
        public CartWindow(IBl p_bl, BO.Cart p_cart)
        {
            InitializeComponent();
            bl = p_bl;
            cart = p_cart;
            MainGrid.DataContext = cart;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            new CartListWindow(bl, cart).Show();
            Close();
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = bl.Cart.OrderConfirmation(cart);
                MessageBox.Show("Order confirmed. Order ID: " + id);
                new Orders.OrderTracking(bl, new BO.Cart(), id).Show();
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
