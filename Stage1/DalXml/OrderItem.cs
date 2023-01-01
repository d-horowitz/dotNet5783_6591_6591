using DalApi;
using DO;

namespace Dal;
internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem item)
    {
        return 1;
    }

    public void Delete(int id)
    {
        
    }

    public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool>? func = null)
    {
        return new List<DO.OrderItem>();
    }

    public DO.OrderItem ReadSingle(Func<DO.OrderItem, bool> func)
    {
        return new DO.OrderItem();
    }

    public void Update(DO.OrderItem item)
    {
        
    }
}