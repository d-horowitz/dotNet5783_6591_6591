namespace DalApi;
public class EntityNotFoundException : Exception
{
    public override string Message =>
                            "Entity Not Found";

}

public class ObjectAlreadyExists : Exception
{
    public override string Message =>
                            "id Already Exists";

}

public class NonExistentObject : Exception
{
    public override string Message =>
                            "object does not Exist";

}

public class InvalidInput : Exception
{
    public override string Message =>
                            "Input Is Not Valid";

}

public class DataOverflow : Exception
{
    public override string Message =>
                            "data overflow";

}

public class DataIsEmpty : Exception
{
    public override string Message =>
                            "data is empty";

}
public class NotEnoughInStock : Exception
{
    public override string Message =>
                            "Not enough items in stock";

}