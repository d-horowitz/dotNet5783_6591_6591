using DalApi;
using DO;

namespace Dal;
internal class Product : IProduct
{
    public int Add(DO.Product item)
    {
        return 1;
    }

    public void Delete(int id)
    {
        
    }

    public IEnumerable<DO.Product> Read(Func<DO.Product, bool>? func = null)
    {
        return new List<DO.Product>();
    }

    public DO.Product ReadSingle(Func<DO.Product, bool> func)
    {
        return new DO.Product();
    }

    public void Update(DO.Product item)
    {
        
    }
}

