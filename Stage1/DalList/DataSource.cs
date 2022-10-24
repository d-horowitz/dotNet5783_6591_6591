using DO;
namespace Dal;

static internal class DataSource
{
    static readonly Random randomer = new Random();
    static internal Product[] Products = new Product[50];
    static internal Order[] Orders = new Order[100];
    static internal OrderItem[] OrderItems = new OrderItem[200];

}

