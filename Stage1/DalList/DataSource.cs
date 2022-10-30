using DO;
namespace Dal;

static internal class DataSource
{
    static readonly Random randomer = new Random();
    static internal Product[] products = new Product[50];
    static internal Order[] orders = new Order[100];
    static internal OrderItem[] orderItems = new OrderItem[200];

    static internal class Config
    {
        static internal int productIndex = 0;
        static internal int orderIndex = 0;
        static internal int orderItemIndex = 0;
        static internal int OrderId = 100000;
        static internal int OrderItemId = 100000;
    }
}

