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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private readonly int id;
        private readonly IBl bl;
        //public ObservableCollection<Tuple<decimal, decimal>> MyCollection { get; }
        public OrderTracking(IBl p_bl, int p_id)
        {
            InitializeComponent();
            bl = p_bl;
            id = p_id;
            Id.Content = Id.Content.ToString() + p_id;
            BO.OrderTracking tracking = bl.Order.TrackOrder(p_id);
            Order.DataContext = tracking;
            Items.ItemsSource = new ObservableCollection<Tuple<DateTime, BO.EOrderStatus>>( tracking.TrackList);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
        private void Details(object sender, RoutedEventArgs e)
        {
            new OrderWindow(bl,id,false).Show();
            Close();
        }
    }
}
