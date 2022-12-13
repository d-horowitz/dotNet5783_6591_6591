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
    public ProductWindow(IBl p_bl, int? p_id = null)
    {
        InitializeComponent();
        bl = p_bl;
        CategoryInput.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
        if (p_id == null)
        {
            AddUpdate.Click += Add;
            AddUpdate.Content = "Add";
        }
        else
        {
            AddUpdate.Click += Update;
            AddUpdate.Content = "Update";
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
            MessageBox.Show("the book was added succesfully with ID " + id.ToString(), "👍 Successful Action");
            BackToProducts(sender, e);
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
            BackToProducts(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "⚠ERROR");
        }
    }

    private void BackToProducts(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        Close();
    }
}
