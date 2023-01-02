namespace BO;
public class NotExistException : Exception
{
    public NotExistException(Exception exc) : base("The item does not exist", exc) { }
}
public class DataIsEmpty : Exception
{
    public DataIsEmpty(Exception exc) : base("Data is empty", exc) { }
}
public class InvalidInput : Exception
{
    public InvalidInput(string msg) : base(msg) { }
    public InvalidInput(Exception exc) : base("Input is not valud", exc) { }
}
public class NonExistentObject : Exception
{
    public NonExistentObject(Exception exc) : base("Object is not exist", exc) { }
    public NonExistentObject(string msg) : base(msg) { }
}
public class DataOverflow : Exception
{
    public DataOverflow(Exception exc) : base("Data overflow", exc) { }
}
public class ObjectAlreadyExists : Exception
{
    public ObjectAlreadyExists(string msg) : base(msg) { }
    public ObjectAlreadyExists(Exception exc) : base("Object already exists", exc) { }
}
public class ProductExistsAtSomeOrder : Exception
{
    public ProductExistsAtSomeOrder() : base("Product is exist at some order") { }
}
public class Unsuccessful : Exception
{
    public Unsuccessful(string msg) : base(msg) { }
}
public class NotEnoughInStock : Exception
{
    public NotEnoughInStock(Exception exc) : base("Not Enough Items in Stock", exc) { }
    public NotEnoughInStock(string msg) : base(msg) { }
}