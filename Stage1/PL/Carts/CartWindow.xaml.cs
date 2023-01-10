using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PL.PO;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly IBl bl;
        private Cart cart;
        public CartWindow(IBl p_bl, Cart p_cart)
        {
            InitializeComponent();
            bl = p_bl;
            cart = p_cart;
            //ObservableCollection<BO.OrderItem> list = new ();
            //cart.Items.ForEach(x => list.Add(x));
            //(cart.Items);
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
                int id = bl.Cart.OrderConfirmation(PtoB.ConvertCart(cart));
                MessageBox.Show("Order confirmed. Order ID: " + id);
                new Orders.OrderTracking(bl, new Cart() { Items = new() }, id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Increment(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem changed = (BO.OrderItem)((Button)sender).DataContext;

                cart = BtoP.ConvertCart(bl.Cart.Update(PtoB.ConvertCart(cart), changed.ProductId, changed.Amount + 1));
                MainGrid.DataContext = cart;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
            }
        }

        private void Decrement(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem changed = (BO.OrderItem)((Button)sender).DataContext;
                cart = BtoP.ConvertCart(bl.Cart.Update(PtoB.ConvertCart(cart), changed.ProductId, changed.Amount - 1));
                MainGrid.DataContext = cart;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
            }
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem changed = (BO.OrderItem)((Button)sender).DataContext;
                cart = BtoP.ConvertCart(bl.Cart.Update(PtoB.ConvertCart(cart), changed.ProductId, 0));
                MainGrid.DataContext = cart;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
            }
        }
    }
}
