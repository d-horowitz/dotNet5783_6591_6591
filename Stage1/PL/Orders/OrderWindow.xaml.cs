using System;
using System.Collections.Generic;
using System.Globalization;
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
using BlImplementation;
using PL.Products;
using PL.PO;

namespace PL.Orders
{

    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>

    public partial class OrderWindow : Window
    {
        private readonly IBl bl;
        private readonly int id;
        public readonly bool editable;
        private readonly Cart cart = new();

        public OrderWindow(IBl p_bl, Cart p_cart, int p_id, bool p_editable)
        {
            InitializeComponent();
            bl = p_bl;
            id = p_id;
            cart = p_cart;
            editable = p_editable;
            Id.Content = Id.Content.ToString() + p_id;
            BO.Order order = bl.Order.Read(p_id);
            Order.DataContext = order;
            Items.ItemsSource = order.Items;
            buttons.DataContext = new
            {
                editable = p_editable
            };
            //DataContext = editable;
            Ship.Visibility = Visibility.Hidden;
            Deliver.Visibility = Visibility.Hidden;
            if (editable && order.OrderCreated != DateTime.MinValue)
            {
                if (order.Shipping == DateTime.MinValue)
                    Ship.Visibility = Visibility.Visible;
                else if (order.Delivery == DateTime.MinValue)
                    Deliver.Visibility = Visibility.Visible;
            }

            /*AddUpdate.Click += Update;
            AddUpdate.Content = "Update";
            id = (int)p_id;
            BO.Product product = bl.Product.ReadForManager(id);
            NameInput.Text = product.Name;
            PriceInput.Text = product.Price.ToString();
            CategoryInput.SelectedItem = product.Category;
            AmountInput.Text = product.AmountInStock.ToString();*/

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (editable)
            {
                new ProductListWindow(bl, cart).Show();
                Close();
            }
            else
            {
                new OrderTracking(bl,cart, id).Show();
                Close();
            }
        }
        private void ShipOrder(object sender, RoutedEventArgs e)
        {
            if (editable)
            {
                bl.Order.UpdateShipping(id);
                BO.Order order = bl.Order.Read(id);
                Order.DataContext = order;
                Items.ItemsSource = order.Items;
                //DataContext = editable;
                Ship.Visibility = Visibility.Hidden;
                Deliver.Visibility = Visibility.Hidden;
                if (editable && order.OrderCreated != DateTime.MinValue)
                {
                    if (order.Shipping == DateTime.MinValue)
                        Ship.Visibility = Visibility.Visible;
                    else if (order.Delivery == DateTime.MinValue)
                        Deliver.Visibility = Visibility.Visible;
                }
            }
        }
        private void DeliverOrder(object sender, RoutedEventArgs e)
        {
            if (editable)
            {
                bl.Order.UpdateDelivery(id);
                BO.Order order = bl.Order.Read(id);
                Order.DataContext = order;
                Items.ItemsSource = order.Items;
                //DataContext = editable;
                Ship.Visibility = Visibility.Hidden;
                Deliver.Visibility = Visibility.Hidden;
                if (editable && order.OrderCreated != DateTime.MinValue)
                {
                    if (order.Shipping == DateTime.MinValue)
                        Ship.Visibility = Visibility.Visible;
                    else if (order.Delivery == DateTime.MinValue)
                        Deliver.Visibility = Visibility.Visible;
                }
            }
        }

    }
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
