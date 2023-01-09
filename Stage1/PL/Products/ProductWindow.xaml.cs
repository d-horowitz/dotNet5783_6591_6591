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
using BlImplementation;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private readonly IBl bl;
    private readonly int id;
    private readonly bool noneditable;
    private BO.Cart cart = new();
    public ProductWindow(IBl p_bl, BO.Cart p_cart, bool p_noneditable, int? p_id = null)
    {
        InitializeComponent();
        bl = p_bl;
        cart = p_cart;
        noneditable = p_noneditable;
        DataContext = noneditable;
        CategoryInput.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
        if (p_id == null || noneditable)
        {
            AddUpdate.Click += Add;
            AddUpdate.Content = "Add";
        }
        if (p_id != null)
        {
            if (!noneditable)
            {
                AddUpdate.Click += Update;
                AddUpdate.Content = "Update";
            }
            id = (int)p_id;
            BO.Product product = bl.Product.ReadForManager(id);
            NameInput.Text = product.Name;
            PriceInput.Text = product.Price.ToString();
            CategoryInput.SelectedItem = product.Category;
            AmountInput.Text = product.AmountInStock.ToString();
        }
    }

    private void Add(object sender, RoutedEventArgs e)
    {
        try
        {
            if (noneditable)
            {
                cart = bl.Cart.Create(cart, id);
            }
            else
            {
                if (CategoryInput.SelectedItem == null)
                    throw new Exception("Category not selected");
                int id = bl.Product.Create(
                    new BO.Product
                    {
                        Name = NameInput.Text,
                        Price = Convert.ToDouble(PriceInput.Text),
                        Category = (BO.ECategory)CategoryInput.SelectedItem,
                        AmountInStock = Convert.ToInt32(AmountInput.Text)
                    }
                );
            }
            MessageBox.Show(NameInput.Text + " was added successfully" + (noneditable ? " to cart" : ""), "👍 Successful Action");
            //MessageBox.Show("the book was added succesfully with ID " + id.ToString(), "👍 Successful Action");
            Back(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "⚠ERROR");
        }
    }
    private void Update(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Product.Update(
                new BO.Product
                {
                    Id = id,
                    Name = NameInput.Text,
                    Price = Convert.ToDouble(PriceInput.Text),
                    Category = (BO.ECategory)CategoryInput.SelectedItem,
                    AmountInStock = Convert.ToInt32(AmountInput.Text)
                }
            );
            MessageBox.Show("the book was updated succesfully", "👍 Successful Action");
            Back(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "⚠ERROR");
        }
    }

    private void Back(object sender, RoutedEventArgs e)
    {
        if (noneditable)
            new Carts.CartListWindow(bl, cart).Show();
        else
            new ProductListWindow(bl, cart).Show();
        Close();
    }
}
