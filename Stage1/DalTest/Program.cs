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
                    Console.WriteLine(dalProduct.Create(p));
                    break;
                case Crud.Read:
                    Console.WriteLine("Enter book id:");
                    bookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.Read(bookId));
                    break;
                case Crud.ReadAll:
                    Product[] products = dalProduct.Read();
                    foreach (Product product in products)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                case Crud.Update:
                    Console.WriteLine("Enter book id:");
                    p.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.Read(p.Id));
                    Console.WriteLine("Enter new details:");
                    Console.WriteLine("Name:");
                    //str = Console.ReadLine();
                    //p.Name = str == null ? p.Name : str;
                    p.Name = Console.ReadLine() ?? p.Name;
                    Console.WriteLine("Amount of copies in stock:");
                    p.Amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Price:");
                    p.Price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Category:(0-Kodesh, 1-Biography, 2-Novel, 3-Fiction, 4-Children)");
                    p.Category = (ECategory)Convert.ToInt32(Console.ReadLine());
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
                    Console.WriteLine(dalOrder.Create(o));
                    break;
                case Crud.Read:
                    Console.WriteLine("Enter order id:");
                    orderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrder.Read(orderId));
                    break;
                case Crud.ReadAll:
                    Order[] orders = dalOrder.Read();
                    foreach (Order order in orders)
                    {
                        Console.WriteLine(order);
                    }
                    break;
                case Crud.Update:
                    Console.WriteLine("Enter order id:");
                    o.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalOrder.Read(o.Id));
                    Console.WriteLine("Enter new details:");
                    Console.WriteLine("Customer Name:");
                    o.Name = Console.ReadLine() ?? o.Name;
                    Console.WriteLine("Email:");
                    o.Email = Console.ReadLine() ?? o.Email;
                    Console.WriteLine("Address:");
                    o.Address = Console.ReadLine() ?? o.Address;
                    Console.WriteLine("Amount of copies in stock:");
                    break;
                /*case Crud.Delete:
                    Console.WriteLine("Enter book id:");
                    bookId = Convert.ToInt32(Console.ReadLine());
                    dalProduct.Delete(bookId);
                    break;*/
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
        Console.WriteLine("Started Program");
        Console.WriteLine(DateTime.Now);
        DateTime date = DateTime.Parse("31/12/2020");
        Console.WriteLine(date);
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