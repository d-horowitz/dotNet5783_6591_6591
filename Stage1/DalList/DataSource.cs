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
        public static int ProductIndex { get { return _productIndex++; } }
        public static int OrderIndex { get { return _orderIndex++; } }
        public static int OrderItemIndex { get { return _orderItemIndex++; } }
        public static int OrderId { get { return _orderId++; } }
        public static int OrderItemId { get { return _orderItemId++; } }
    }
    private static void addProducts()
    {
        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            p.Id = 100000 + i;
            p.Name = "BOOK " + (char)(i + 65);
            p.Category = (ECategory)(i % 5);
            p.Price = 90 + i * 1.1;
            p.Amount = i;
            _products[Config.ProductIndex] = p;
        }
    }
    private static void addOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            Order o = new Order();
            o.Id = Config.OrderId;
            o.Name = "Customer " + (char)(i + 97);
            o.Email = "customer" + (char)(i + 97) + "@gmail.com";
            o.Address = i + " " + (char)(i + 65) + " st.";
            o.OrderCreated = DateTime.Now - TimeSpan.FromDays(i + 1);
        }
    }
    private static void addOrderItems()
    {

    }
    private static void s_Initialize()
    {
        addProducts();
        addOrders();
        addOrderItems();
    }
    // Constructor:
    static DataSource() => s_Initialize();
};

