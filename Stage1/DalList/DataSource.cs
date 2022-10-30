using DO;
namespace Dal;

static internal class DataSource
{
    static internal readonly Random _randomer = new Random();
    static internal Product[] _products = new Product[50];
    static internal Order[] _orders = new Order[100];
    static internal OrderItem[] _orderItems = new OrderItem[200];

    static internal class Config
    {
        static internal int _productIndex = 0;
        static internal int _orderIndex = 0;
        static internal int _orderItemIndex = 0;
        static internal int _orderId = 100000;
        static internal int _orderItemId = 100000;
        public static int orderId { get { return _orderId++; } }
        public static int orderItemId { get { return _orderItemId++; } }
    }
    private static void addProducts()
    {
        
    }
    private static void addOrders()
    {

    }
    private static void addOrderItems()
    {

    }
};

