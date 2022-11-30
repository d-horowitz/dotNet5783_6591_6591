using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();
    public IEnumerable<DO.Product> GetProductList()
    {
        return new List<DO.Product>();//change urgently
    }
    public IEnumerable<DO.Product> GetCatalog()
    {
        return new List<DO.Product>();//change urgently
    }
    public DO.Product GetDetails(int productId)
    {
        return new DO.Product();//change urgently
    }
    public void Add(BO.Product p)
    {
        //change urgently
    }
    public void Delete(int productId)
    {
        //change urgently
    }
    public void Update()
}
