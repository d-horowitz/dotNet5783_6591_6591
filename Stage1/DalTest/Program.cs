using Dal;
using DO;

namespace DalTest;
class Program
{
    private static readonly DataSource dataSource = new();
    private static readonly DalProduct dalProduct = new();
    private static readonly DalOrder dalOrder = new();
    private static readonly DalOrderItem dalOrderItem = new();
    private void ProductMenu()
    {
        Crud crud;
        Console.WriteLine("Choose Action:");
        Console.WriteLine("0 - Add a new book");
        Console.WriteLine("1 - Display a book");
        Console.WriteLine("2 - Display all books");
        Console.WriteLine("3 - Update a book");
        Console.WriteLine("4 - Delete a book");
        crud = (Crud)Convert.ToInt32(Console.ReadKey());
        switch (crud)
        {
            case Crud.Create:
                Product p = new();
                Console.WriteLine("Enter book details:");
                Console.WriteLine("Name:");
                p.Name = Console.ReadLine();
                Console.WriteLine("Amount of copies in stock:");
                p.Amount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Price:");
                p.Amount = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Category:(0-Kodesh, 1-Biography, 2-Novel, 3-Fiction, 4-Children)");
                p.Category = (ECategory)Convert.ToInt32(Console.ReadKey());
                dalProduct.Create(p);
                break;
            case Crud.Read:
                Console.WriteLine("Enter book id:");
                int bookId = Convert.ToInt32(Console.ReadLine());
                dalProduct.Read(bookId);
                break;
            case Crud.ReadAll:
                Product[] products = dalProduct.Read();
                break;
            case Crud.Update:
                break;
            case Crud.Delete:
                break;
            default:
                Console.WriteLine("ERROR\nINVALID CHOICE!!!");
                break;
        }
    }
    public static void Main()
    {
        Console.WriteLine("Started Program");
        //Product[] products = dalProduct.Read();
        //foreach(Product product in products)
        //{
        //    Console.WriteLine(product);
        //}
        Options choice;
        Console.WriteLine("Options:");
        Console.WriteLine("0 - Exit");
        Console.WriteLine("1 - Check Books");
        Console.WriteLine("2 - Check Orders");
        Console.WriteLine("3 - Check Order Items");
        choice = (Options)Convert.ToInt32(Console.ReadKey());
        while (choice != Options.Exit)
        {
            try
            {
                switch (choice)
                {
                    case Options.Product:
                        break;
                    case Options.Order:
                        break;
                    case Options.OrderItem:
                        break;
                    default:
                        Console.WriteLine("ERROR\nINVALID OPTION!!!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            choice = (Options)Convert.ToInt32(Console.ReadKey());
        }
    }

    public Program()
    {
        Main();
    }
}