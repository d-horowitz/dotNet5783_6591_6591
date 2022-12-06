using Dal;
using DO;

namespace DalTest;
class Program
{
    private static readonly DataSource dataSource = new();
    private static readonly DalProduct dalProduct = new();
    private static readonly DalOrder dalOrder = new();
    private static readonly DalOrderItem dalOrderItem = new();
    private static void ProductMenu()
    {
        Crud crud;
        Product p = new();
        int bookId;
        string input;
        Console.WriteLine("Choose Action:");
        Console.WriteLine("0 - Add a new book");
        Console.WriteLine("1 - Display a book");
        Console.WriteLine("2 - Display all books");
        Console.WriteLine("3 - Update a book");
        Console.WriteLine("4 - Delete a book");
        crud = (Crud)Convert.ToInt32(Console.ReadLine());
        try
        {
            switch (crud)
            {
                case Crud.Create:
                    Console.WriteLine("Enter book details:");
                    Console.WriteLine("Name:");
                    p.Name = Console.ReadLine();
                    Console.WriteLine("Amount of copies in stock:");
                    p.Amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Price:");
                    p.Price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Category:(0-Kodesh, 1-Biography, 2-Novel, 3-Fiction, 4-Children)");
                    p.Category = (ECategory)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.Add(p));
                    break;
                case Crud.Read:
                    Console.WriteLine("Enter book id:");
                    bookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.ReadSingle(product => product.Id == bookId));
                    break;
                case Crud.ReadAll:
                    List<Product> products = new(dalProduct.Read());
                    foreach (Product product in products)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                case Crud.Update:
                    Console.WriteLine("Enter book id:");
                    p.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.ReadSingle(product => product.Id == p.Id));
                    Console.WriteLine("Enter new details:");
                    Console.WriteLine("Name:");
                    input = Console.ReadLine();
                    p.Name = input == "" ? p.Name : input;
                    Console.WriteLine("Amount of copies in stock:");
                    input = Console.ReadLine();
                    p.Amount = input == "" ? p.Amount : Convert.ToInt32(input);
                    Console.WriteLine("Price:");
                    input = Console.ReadLine();
                    p.Price = input == "" ? p.Price : Convert.ToDouble(input);
                    Console.WriteLine("Category:(0-Kodesh, 1-Biography, 2-Novel, 3-Fiction, 4-Children)");
                    input = Console.ReadLine();
                    p.Category = input == "" ? p.Category : (ECategory)Convert.ToInt32(input);
                    dalProduct.Update(p);
                    break;
                case Crud.Delete:
                    Console.WriteLine("Enter book id:");
                    bookId = Convert.ToInt32(Console.ReadLine());
                    dalProduct.Delete(bookId);
                    break;
                default:
                    Console.WriteLine("ERROR\nINVALID CHOICE!!!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void OrderMenu()
    {
        Crud crud;
        Order o = new();
        int orderId;
        string input;
        Console.WriteLine("Choose Action:");
        Console.WriteLine("0 - Add a new order");
        Console.WriteLine("1 - Display an order");
        Console.WriteLine("2 - Display all orders");
        Console.WriteLine("3 - Update an order");
        Console.WriteLine("4 - Delete an order");
        crud = (Crud)Convert.ToInt32(Console.ReadLine());
        try
        {
            switch (crud)
            {
                case Crud.Create:
                    Console.WriteLine("Enter Customer details:");
                    Console.WriteLine("Name:");
                    o.Name = Console.ReadLine();
                    Console.WriteLine("Email:");
                    o.Email = Console.ReadLine();
                    Console.WriteLine("Address:");
                    o.Address = Console.ReadLine();
                    Console.WriteLine(dalOrder.Add(o));
                    break;
                case Crud.Read:
                    Console.WriteLine("Enter order id:");
                    orderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrder.ReadSingle(order=>order.Id==orderId));
                    break;
                case Crud.ReadAll:
                    List<Order> orders = new(dalOrder.Read());
                    foreach (Order order in orders)
                    {
                        Console.WriteLine(order);
                    }
                    break;
                case Crud.Update:
                    Console.WriteLine("Enter order id:");
                    o.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrder.ReadSingle(order=>order.Id==o.Id));
                    Console.WriteLine("Enter new details:");
                    Console.WriteLine("Customer Name:");
                    input = Console.ReadLine();
                    o.Name = input == "" ? o.Name : input;
                    Console.WriteLine("Email:");
                    input = Console.ReadLine();
                    o.Email = input == "" ? o.Email : input;
                    Console.WriteLine("Address:");
                    input = Console.ReadLine();
                    o.Address = input == "" ? o.Address : input;
                    Console.WriteLine("Date of shipping (dd/mm/yyyy format):");
                    input = Console.ReadLine();
                    o.Shipping = input == "" ? o.Shipping : DateTime.Parse(input);
                    Console.WriteLine("Date of Delivery (dd/mm/yyyy format):");
                    input = Console.ReadLine();
                    o.Delivery = input == "" ? o.Delivery : DateTime.Parse(input);
                    dalOrder.Update(o);
                    break;
                case Crud.Delete:
                    Console.WriteLine("Enter order id:");
                    orderId = Convert.ToInt32(Console.ReadLine());
                    dalProduct.Delete(orderId);
                    break;
                default:
                    Console.WriteLine("ERROR\nINVALID CHOICE!!!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void OrderItemMenu()
    {
        Crud crud;
        OrderItem oi = new();
        List<OrderItem> orderItems;
        int orderItemId, orderId;
        Console.WriteLine("Choose Action:");
        Console.WriteLine("0 - Add a new order item");
        Console.WriteLine("1 - Display an order item by id");
        Console.WriteLine("2 - Display all orders' items");
        Console.WriteLine("3 - Update an order item");
        Console.WriteLine("4 - Delete an order item");
        Console.WriteLine("5 - Display an order item by order id and product id");
        Console.WriteLine("6 - Display an order's items");
        crud = (Crud)Convert.ToInt32(Console.ReadLine());
        try
        {
            switch (crud)
            {
                case Crud.Create:
                    Console.WriteLine("Enter order id:");
                    oi.OrderId = Convert.ToInt32(Console.ReadLine());
                    dalOrder.ReadSingle( order=> order.Id == oi.OrderId);//oi.OrderId
                    Console.WriteLine("Enter product id");
                    oi.ProductId = Convert.ToInt32(Console.ReadLine());
                    oi.UnitPrice = dalProduct.ReadSingle(product=>product.Id==oi.ProductId).Price;
                    Console.WriteLine("Enter amount:");
                    oi.Amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrderItem.Add(oi));
                    break;
                case Crud.Read:
                    Console.WriteLine("Enter order item id:");
                    orderItemId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrderItem.ReadSingle(orderItem=>orderItem.Id==orderItemId));
                    break;
                case Crud.ReadAll:
                    orderItems = new(dalOrderItem.Read());
                    foreach (OrderItem orderItem in orderItems)
                    {
                        Console.WriteLine(orderItem);
                    }
                    break;
                case Crud.Update:
                    Console.WriteLine("Enter order item id:");
                    orderItemId = Convert.ToInt32(Console.ReadLine());
                    oi = dalOrderItem.ReadSingle(orderItem =>orderItem.Id==orderItemId);
                    Console.WriteLine("Enter new Amount:");
                    oi.Amount = Convert.ToInt32(Console.ReadLine());
                    dalOrderItem.Update(oi);
                    break;
                case Crud.Delete:
                    Console.WriteLine("Enter order item id:");
                    orderItemId = Convert.ToInt32(Console.ReadLine());
                    dalOrderItem.Delete(orderItemId);
                    break;
                case Crud.Read2param:
                    Console.WriteLine("Enter order id:");
                    orderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter product id:");
                    int productId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrderItem.ReadSingle(orderItem => orderItem.OrderId == orderId && orderItem.ProductId == productId));
                    break;
                case Crud.ReadList:
                    Console.WriteLine("Enter order id:");
                    orderId = Convert.ToInt32(Console.ReadLine());
                    orderItems = new(dalOrderItem.Read(orderItem=>orderItem.OrderId==orderId));
                    foreach (OrderItem orderItem in orderItems)
                    {
                        Console.WriteLine(orderItem);
                    }
                    break;
                default:
                    Console.WriteLine("ERROR\nINVALID CHOICE!!!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void Main()
    {
        Options choice;
        Console.WriteLine("Choose an Option:");
        Console.WriteLine("0 - Exit");
        Console.WriteLine("1 - Check Books");
        Console.WriteLine("2 - Check Orders");
        Console.WriteLine("3 - Check Order Items");
        choice = (Options)Convert.ToInt32(Console.ReadLine());
        while (choice != Options.Exit)
        {
            switch (choice)
            {
                case Options.Product:
                    ProductMenu();
                    break;
                case Options.Order:
                    OrderMenu();
                    break;
                case Options.OrderItem:
                    OrderItemMenu();
                    break;
                default:
                    Console.WriteLine("ERROR\nINVALID OPTION!!!");
                    break;
            }
            Console.WriteLine("Choose an Option:");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Check Books");
            Console.WriteLine("2 - Check Orders");
            Console.WriteLine("3 - Check Order Items");
            choice = (Options)Convert.ToInt32(Console.ReadLine());
        }
    }

    public Program()
    {
        Main();
    }
}