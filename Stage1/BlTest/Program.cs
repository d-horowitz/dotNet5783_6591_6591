
using BlApi;
using BlImplementation;

IBl blListEntity = new Bl();
BO.Product Product = new();
BO.Cart newCart = new();
BO.OrderItem newOrderItem = new();
Dal.DataSource ds = new();
int choice;
int temp;
int id;
string name;
string mail;
string adress;
int amount;

//===================================product===================================
/// <summary>
/// Sending a request to get all the products and prints their details
/// </summary>
void ReadProduct()
{
    foreach (var item in blListEntity.Product.Read())
        Console.WriteLine(item);
}

/// <summary>
/// Sending a request to get all the products and prints their details for the catalog
/// </summary>
void ReadCatalogProduct()
{
    foreach (var item in blListEntity.Product.ReadCatalog())
        Console.WriteLine(item);
}

/// <summary>
/// Sending a request to get one product by ID and prints its details for buyeer
/// </summary>
void ReadProductForCustomer()
{
    Console.WriteLine("Please enter ID");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Product.ReadForCustomer(temp));
}

/// <summary>
/// Sending a request to get one product by ID and prints its details for dairector
/// </summary>
void ReadProductForManager()
{
    Console.WriteLine("Please enter ID");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Product.ReadForManager(temp));
}

/// <summary>
/// Sending a request to get new details for an existing product 
/// from the user and updates the changes
/// </summary>
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
            double price1 = Convert.ToDouble(price);
            Product.Price = price1;
        }

        Console.WriteLine("Enter new amount in stock");
        string inStock = Console.ReadLine();
        if (!string.IsNullOrEmpty(inStock))
        {
            int inStock1 = Convert.ToInt32(inStock);
            Product.AmountInStock = inStock1;
        }
        blListEntity.Product.Update(Product);
        Console.WriteLine("The update was successful");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

/// <summary>
///  Adds a new product to the list products
/// </summary>
void CreateProduct()
{
    try
    {
        Product.Id = BO.Config.ProductId;
        Console.WriteLine("Enter name");
        Product.Name = Console.ReadLine();
        Console.WriteLine("Enter price");
        Product.Price = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter amount in stock");
        Product.AmountInStock = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter category: רהיטים - 1, רהיטי מטבח - 2");
        Product.Category = (BO.ECategory)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($@"succses! id is: {blListEntity.Product.Create(Product)}");
    }
    catch (BO.DataOverflow ex) { Console.WriteLine(ex); }
    catch (BO.ObjectAlreadyExists ex) { Console.WriteLine(ex); }
    catch (BO.InvalidInput ex) { Console.WriteLine(ex); }
}


/// <summary>
/// Allows the user to choose the requested service for actions on a product
/// </summary>
void ProductFunction()
{
    Console.WriteLine("Enter your choice: 1 - Read , 2 - Read catalog , 3 - Read Product for buyer , 4 - Read Product for dairector , 5 - Update , 6 - Creat ");
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
    catch (BO.DataIsEmpty ex) { Console.WriteLine(ex); }
    catch (BO.InvalidInput ex) { Console.WriteLine(ex); }
    catch (BO.NonExistentObject ex) { Console.WriteLine(ex); }
    catch (BO.DataOverflow ex) { Console.WriteLine(ex); }
    catch (BO.ObjectAlreadyExists ex) { Console.WriteLine(ex); }
    catch (BO.ProductExistsAtSomeOrder ex) { Console.WriteLine(ex); }

}
//===================================order=====================================

/// <summary>
/// Sending a request to get all the orders and prints their details
/// </summary>
void Read()
{
    List<BO.OrderForList> newOrder = new();
    newOrder = (List<BO.OrderForList>)blListEntity.Order.Read();
    foreach (var item in newOrder)
    {
        Console.WriteLine(item);
    }
}

/// <summary>
/// Sending a request to get specific order for dairector
/// </summary>
void ReadOrderForManager()
{
    BO.Order order = new();
    Console.WriteLine("Please enter order ID");
    temp = Convert.ToInt32(Console.ReadLine());
    order = blListEntity.Order.Read(temp);
    Console.WriteLine(order);
}

/// <summary>
/// Order shipping date update
/// </summary>
void UpdateOrderShipping()
{
    Console.WriteLine("Please enter an order number");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Order.UpdateShipping(temp));
}

/// <summary>
/// Order delivering date update
/// </summary>
void UpdateOrderDelivery()
{
    Console.WriteLine("Please enter an order number");
    temp = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(blListEntity.Order.UpdateDelivery(temp));
}

/// <summary>
/// Allows the user to choose the requested service for actions on a order
/// </summary>
void OrderFunction()
{
    Console.WriteLine("Enter your choice:  1 - Read all 2 - Read for dairector , 3 - Update order shipping , 4 - Update order delivery ");
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
    catch (BO.DataIsEmpty ex) { Console.WriteLine(ex); }
    catch (BO.InvalidInput ex) { Console.WriteLine(ex); }
    catch (BO.NonExistentObject ex) { Console.WriteLine(ex); }
}

//===================================cart======================================

/// <summary>
/// Add a new product to the cart list
/// </summary>
void AddToCart()
{
    Console.WriteLine("Please enter Product ID");
    id = Convert.ToInt32(Console.ReadLine());
    newCart = blListEntity.Cart.Create(newCart, id);
    Console.WriteLine(newCart);
}

/// <summary>
/// Sending a request to get a new amount for specific item and update id
/// </summary>
void UpdateToCart()
{
    Console.WriteLine("Please enter product ID");
    id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Please enter amount");
    amount = Convert.ToInt32(Console.ReadLine());
    newCart = blListEntity.Cart.Update(newCart, id, amount);
    Console.WriteLine(newCart);
}

/// <summary>
/// Order confirmation process,
/// gets userdetails and creat a new order
/// </summary>
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

/// <summary>
/// Allows the user to choose the requested service for actions on a cart
/// </summary>
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
    catch (BO.Unsuccessful ex) { Console.WriteLine(ex); }
    catch (BO.InvalidInput ex) { Console.WriteLine(ex); }
    catch (BO.NonExistentObject ex) { Console.WriteLine(ex); }
    catch (BO.DataOverflow ex) { Console.WriteLine(ex); }
    catch (BO.ObjectAlreadyExists ex) { Console.WriteLine(ex); }
    catch (BO.DataIsEmpty ex) { Console.WriteLine(ex); }
}

//======================================main===================================

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