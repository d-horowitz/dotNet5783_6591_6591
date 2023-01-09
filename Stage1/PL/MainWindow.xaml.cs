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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using PL.Orders;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBl bl = Factory.Get();
        private readonly BO.Cart cart = new();
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(BO.Cart? p_cart = null)
        {
            InitializeComponent();
            if (p_cart != null)
            {
                cart = p_cart;
            }
        }

        private void GoToProducts(object sender, RoutedEventArgs e)
        {
            new Products.ProductListWindow(bl, cart).Show();
            Close();
        }

        private void NewOrder(object sender, RoutedEventArgs e)
        {
            new Carts.CartListWindow(bl, cart).Show();
            Close();
        }

        private void Track(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderTracking(bl, cart,Convert.ToInt32(OrderNumber.Text)).Show();
                Close();
            }
            catch(BO.NonExistentObject ex)
            {
                MessageBox.Show("Order not found.", "⚠" + ex.Message);
            }
            catch(Exception ex){
                MessageBox.Show("Please enter a valid order number.", "⚠"+ex.Message);
            }
        }
    }
}
