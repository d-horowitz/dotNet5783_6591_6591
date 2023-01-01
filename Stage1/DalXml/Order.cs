using DalApi;
using DO;
namespace Dal;
internal class Order : IOrder
{
    public int Add(DO.Order item)
    {
        return 1;
    }
    public DO.Order ReadSingle(Func<DO.Order, bool> func)
    {
        return new DO.Order();
    }
    public IEnumerable<DO.Order> Read(Func<DO.Order, bool>? func = null)
    {
        return new List<DO.Order>();
    }
    public void Delete(int id)
    {

    }
    public void Update(DO.Order item)
    {

    }
}