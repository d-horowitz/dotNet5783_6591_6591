using System;
using System.Windows;
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
        private readonly PO.Cart cart = new() { Instance = new() { Items = new() } };
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(PO.Cart? p_cart = null)
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

        private void StartSimulation(object sender, RoutedEventArgs e)
        {
            new Simulation.Simulation(bl).Show();
        }
    }
}
