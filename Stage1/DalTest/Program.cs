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
                    p.Name = Console.ReadLine();
                    Console.WriteLine("Amount of copies in stock:");
                    p.Amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Price:");
                    p.Price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Category:(0-Kodesh, 1-Biography, 2-Novel, 3-Fiction, 4-Children)");
                    p.Category = (ECategory)Convert.ToInt32(Console.ReadLine());
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
    public static void Main()
    {
        Console.WriteLine("Started Program");
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