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
using System.ComponentModel;

namespace PL.Orders
{
    public class NotBooleanToVisibilityConverterShip : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            BO.EOrderStatus status = (BO.EOrderStatus)value;
            if (status == BO.EOrderStatus.Processed)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NotBooleanToVisibilityConverterDeliver : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            BO.EOrderStatus status = (BO.EOrderStatus)value;
            if (status == BO.EOrderStatus.Shipped)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>

    public partial class OrderWindow : Window
    {
        private readonly IBl bl;
        private readonly int id;
        public readonly bool editable;
        private readonly Cart cart = new();
        private Order _order = new();
        public Order M_Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("M_Cart"));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public OrderWindow(IBl p_bl, Cart p_cart, int p_id, bool p_editable)
        {
            InitializeComponent();
            bl = p_bl;
            id = p_id;
            cart = p_cart;
            editable = p_editable;
            Id.Content = Id.Content.ToString() + p_id;
            _order.Instance = bl.Order.Read(p_id);
            Order.DataContext = _order;
            Items.ItemsSource = _order.Instance.Items;
            buttons.DataContext = new
            {
                editable = p_editable, _order
            };
            //DataContext = editable;
            /*Ship.Visibility = Visibility.Collapsed;
            Deliver.Visibility = Visibility.Collapsed;
            if (editable && order.OrderCreated != DateTime.MinValue)
            {
                if (order.Shipping == DateTime.MinValue)
                    Ship.Visibility = Visibility.Visible;
                else if (order.Delivery == DateTime.MinValue)
                    Deliver.Visibility = Visibility.Visible;
            }*/

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
                M_Order.Instance = bl.Order.Read(id);
                //Order.DataContext = _order;
                //Items.ItemsSource = _order.Instance.Items;
                //DataContext = editable;
                /*Ship.Visibility = Visibility.Collapsed;
                Deliver.Visibility = Visibility.Collapsed;
                if (editable && order.OrderCreated != DateTime.MinValue)
                {
                    if (order.Shipping == DateTime.MinValue)
                        Ship.Visibility = Visibility.Visible;
                    else if (order.Delivery == DateTime.MinValue)
                        Deliver.Visibility = Visibility.Visible;
                }*/
            }
        }
        private void DeliverOrder(object sender, RoutedEventArgs e)
        {
            if (editable)
            {
                bl.Order.UpdateDelivery(id);
                M_Order.Instance = bl.Order.Read(id);
                //Order.DataContext = _order;
                //Items.ItemsSource = _order.Instance.Items;
                //DataContext = editable;
                /*Ship.Visibility = Visibility.Collapsed;
                Deliver.Visibility = Visibility.Collapsed;
                if (editable && order.OrderCreated != DateTime.MinValue)
                {
                    if (order.Shipping == DateTime.MinValue)
                        Ship.Visibility = Visibility.Visible;
                    else if (order.Delivery == DateTime.MinValue)
                        Deliver.Visibility = Visibility.Visible;
                }*/
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
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
