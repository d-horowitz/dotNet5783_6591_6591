
using BlApi;
using BlImplementation;

IBl blListEntity = new Bl();
BO.Product Product = new();
BO.Cart newCart = new();
int choice;
int temp;
int id;
string name;
string mail;
string adress;
int amount;

//product
void ReadProduct()
{
    foreach (var item in blListEntity.Product.Read())
        Console.WriteLine(item);
}

void ReadCatalogProduct()
{
    foreach (var item in blListEntity.Product.ReadCatalog())
        Console.WriteLine(item);
}

void ReadProductForCustomer()
{
    Console.WriteLine("Please enter ID");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Product.ReadForCustomer(temp));
}

void ReadProductForManager()
{
    Console.WriteLine("Please enter ID");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Product.ReadForManager(temp));
}

void UpdateProduct()
{
    Console.WriteLine("Please enter ID");

    id = Convert.ToInt32(Console.ReadLine());
    try
    {
        Product = blListEntity.Product.ReadForManager(id);
        Console.WriteLine(Product);
        Console.WriteLine("Enter new name for the product");
        string tryChice = Console.ReadLine();
        if (!string.IsNullOrEmpty(tryChice))
        {
            Product.Name = tryChice;
        }

        Console.WriteLine("Enter new category for the product: 1 - rahitim, 2 - rahiteiMitbach");
        string category = Console.ReadLine();
        if (!string.IsNullOrEmpty(category))
        {
            int Category1 = Convert.ToInt32(category);
            Product.Category = (BO.ECategory)Category1;
        }

        Console.WriteLine("Enter new price for the product");
        string price = Console.ReadLine();
        if (!string.IsNullOrEmpty(price))
        {
            Product.Price = Convert.ToDouble(price);
        }

        Console.WriteLine("Enter new amount in stock");
        string inStock = Console.ReadLine();
        if (!string.IsNullOrEmpty(inStock))
        {
            Product.AmountInStock = Convert.ToInt32(inStock);
        }
        blListEntity.Product.Update(Product);
        Console.WriteLine("The update was successful");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

void CreateProduct()
{
    try
    {
        Product.Id = BO.Config.ProductId;
        Console.WriteLine("Enter name");
        Product.Name = Console.ReadLine();
        Console.WriteLine("Enter price");
        Product.Price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Enter amount in stock");
        Product.AmountInStock = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter category: רהיטים - 1, רהיטי מטבח - 2");
        Product.Category = (BO.ECategory)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($@"succses! id is: {blListEntity.Product.Create(Product)}");
    }
    catch (BO.DataOverflow ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.ObjectAlreadyExists ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.InvalidInput ex)
    {
        Console.WriteLine(ex);
    }
}

void ProductFunction()
{
    Console.WriteLine("Enter your choice: 1 - Read , 2 - Read catalog , 3 - Read Product for customer , 4 - Read Product for manager , 5 - Update , 6 - Create ");
    choice = Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case (1):
                ReadProduct();
                break;
            case (2):
                ReadCatalogProduct();
                break;
            case (3):
                ReadProductForCustomer();
                break;
            case (4):
                ReadProductForManager();
                break;
            case (5):
                UpdateProduct();
                break;
            case (6):
                CreateProduct();
                break;
            default:
                break;
        }
    }
    catch (BO.DataIsEmpty ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.InvalidInput ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.NonExistentObject ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.DataOverflow ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.ObjectAlreadyExists ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.ProductExistsAtSomeOrder ex)
    {
        Console.WriteLine(ex);
    }

}
//order

void Read()
{
    List<BO.OrderForList> newOrder = new();
    newOrder = (List<BO.OrderForList>)blListEntity.Order.Read();
    foreach (var item in newOrder)
    {
        Console.WriteLine(item);
    }
}

void ReadOrderForManager()
{
    BO.Order order = new();
    Console.WriteLine("Please enter order ID");
    temp = Convert.ToInt32(Console.ReadLine());
    order = blListEntity.Order.Read(temp);
    Console.WriteLine(order);
}

void UpdateOrderShipping()
{
    Console.WriteLine("Please enter an order number");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Order.UpdateShipping(temp));
}

void UpdateOrderDelivery()
{
    Console.WriteLine("Please enter an order number");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Order.UpdateDelivery(temp));
}

void OrderFunction()
{
    Console.WriteLine("Enter your choice:  1 - Read all 2 - Read for manager , 3 - Update order shipping , 4 - Update order delivery ");
    choice = Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case (1):
                Read();
                break;
            case (2):
                ReadOrderForManager();
                break;
            case (3):
                UpdateOrderShipping();
                break;
            case (4):
                UpdateOrderDelivery();
                break;
            default:
                break;
        }
    }
    catch (BO.DataIsEmpty ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.InvalidInput ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.NonExistentObject ex)
    {
        Console.WriteLine(ex);
    }
}

//cart

void AddToCart()
{
    Console.WriteLine("Please enter Product ID");
    id = Convert.ToInt32(Console.ReadLine());
    newCart = blListEntity.Cart.Create(newCart, id);
    Console.WriteLine(newCart);
}

void UpdateToCart()
{
    Console.WriteLine("Please enter product ID");
    id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Please enter amount");
    amount = Convert.ToInt32(Console.ReadLine());
    newCart = blListEntity.Cart.Update(newCart, id, amount);
    Console.WriteLine(newCart);
}

void OrderConfirmationTheCart()
{
    Console.WriteLine("Please enter name");
    name = Console.ReadLine();
    Console.WriteLine("Please enter mail");
    mail = Console.ReadLine();
    Console.WriteLine("Please enter address");
    adress = Console.ReadLine();
    blListEntity.Cart.OrderConfirmation(newCart, name, mail, adress);
    Console.WriteLine("The serving has been made");
}

void CartFunction()
{
    Console.WriteLine("Enter your choice:  1 - Add , 2 - Update , 3 - Comfirmation ");
    choice = Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case (1):
                AddToCart();
                break;
            case (2):
                UpdateToCart();
                break;
            case (3):
                OrderConfirmationTheCart();
                break;
            default:
                break;
        }
    }
    catch (BO.Unsuccessful ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.InvalidInput ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.NonExistentObject ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.DataOverflow ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.ObjectAlreadyExists ex)
    {
        Console.WriteLine(ex);
    }
    catch (BO.DataIsEmpty ex)
    {
        Console.WriteLine(ex);
    }
}

//main

void Main()
{
    do
    {
        Console.WriteLine("Enter your choice: 0 - Exit 1 - Product , 2 - Order , 3 - cart ");
        choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (1):
                ProductFunction();
                break;
            case (2):
                OrderFunction();
                break;
            case (3):
                CartFunction();
                break;
            default:
                break;
        }
    } while (choice != (0));
}
Main();