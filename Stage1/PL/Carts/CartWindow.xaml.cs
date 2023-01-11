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
using System.ComponentModel;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window, INotifyPropertyChanged
    {
        private readonly IBl bl;
        //private Cart cart;

        private Cart _cart;
        public Cart M_Cart
        {
            get
            {
                return _cart;
            }
            set
            {
                _cart = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("M_Cart"));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        public CartWindow(IBl p_bl, Cart p_cart)
        {
            InitializeComponent();
            bl = p_bl;
            _cart = p_cart;
            //ObservableCollection<BO.OrderItem> list = new ();
            //cart.Items.ForEach(x => list.Add(x));
            //(cart.Items);
            MainGrid.DataContext = M_Cart;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            new CartListWindow(bl, M_Cart).Show();
            Close();
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = bl.Cart.OrderConfirmation(PtoB.Cart(M_Cart.Instance));
                MessageBox.Show("Order confirmed. Order ID: " + id);
                new Orders.OrderTracking(bl, new Cart() { Instance = new() { Items = new() } }, id).Show();
                Close();
            }
            catch (BO.InvalidInput)
            {
                MessageBox.Show("Please fill out your details", "⚠ERROR");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
            }
        }

        private void Increment(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderItem changed = (OrderItem)((Button)sender).DataContext;

                M_Cart.Instance = BtoP.Cart(bl.Cart.Update(PtoB.Cart(M_Cart.Instance), changed.ProductId, changed.Amount + 1));
                //MainGrid.DataContext = cart;
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
                OrderItem changed = (OrderItem)((Button)sender).DataContext;
                M_Cart.Instance = BtoP.Cart(bl.Cart.Update(PtoB.Cart(M_Cart.Instance), changed.ProductId, changed.Amount - 1));
                //MainGrid.DataContext = cart;
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
                OrderItem changed = (OrderItem)((Button)sender).DataContext;
                M_Cart.Instance = BtoP.Cart( bl.Cart.Update(PtoB.Cart(M_Cart.Instance), changed.ProductId, 0));
                //MainGrid.DataContext = cart;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "⚠ERROR");
            }
        }
    }
}
