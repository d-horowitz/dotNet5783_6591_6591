namespace BO;
public class NotExistException : Exception
{
    public NotExistException(Exception ex) : base("The item does not exist", ex) { }
}
public class DataIsEmpty : Exception
{
    public DataIsEmpty(Exception ex) : base("Data is empty", ex) { }
}
public class InvalidInput : Exception
{
    public InvalidInput(string msg) : base(msg) { }
    public InvalidInput(Exception ex) : base("Input is invalid", ex) { }
}
public class NonExistentObject : Exception
{
    public NonExistentObject(Exception ex) : base("Object does not exist", ex) { }
    public NonExistentObject(string msg) : base(msg) { }
}
public class DataOverflow : Exception
{
    public DataOverflow(Exception ex) : base("Data overflow", ex) { }
}
public class ObjectAlreadyExists : Exception
{
    public ObjectAlreadyExists(string msg) : base(msg) { }
    public ObjectAlreadyExists(Exception ex) : base("Object already exists", ex) { }
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
    public NotEnoughInStock(Exception ex) : base("Not Enough Items in Stock", ex) { }
    public NotEnoughInStock(string msg) : base(msg) { }
}