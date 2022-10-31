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
        Console.WriteLine("0 - Create a new product");
        crud = (Crud)Convert.ToInt32(Console.ReadKey());
        switch (crud)
        {
            case Crud.Create:
                Product p;
                p
                break;
            case Crud.Read:
                break;
            case Crud.ReadAll:
                break;
            case Crud.Update:
                break;
            case Crud.Delete:
                break;
            default:
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
        Console.WriteLine("options:\n0 - Exit\n1 - Check Products\n2 - Check Orders\n3 - Check Order Items");
        choice = (Options)Convert.ToInt32(Console.ReadKey());
        while (choice != Options.Exit)
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
            choice = (Options)Convert.ToInt32(Console.ReadKey());
        }
    }

    public Program()
    {
        Main();
    }
}