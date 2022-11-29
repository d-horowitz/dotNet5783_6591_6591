using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();
    public IEnumerable<DO.Product> getProductList()
    {
        return new List<DO.Product>();//change urgently
    }
    public IEnumerable<DO.Product> getCatalog()
    {
        return new List<DO.Product>();//change urgently
    }
    public DO.Product GetDetails(int productId)
    {
        return new DO.Product();//change urgently
    }

}
