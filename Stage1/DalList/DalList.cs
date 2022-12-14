using DalApi;
namespace Dal;
sealed internal class DalList : IDal
{
    private DalList()
    {

    }
    public static IDal Instance { get; } = new DalList();
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}

