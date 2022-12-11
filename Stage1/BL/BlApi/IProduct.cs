using BO;
namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList> Read(Func<DO.Product, bool>? func = null);
    public IEnumerable<ProductItem> ReadCatalog();
    public Product ReadForManager(int productId);
    public Product ReadForCustomer(int productId);
    public int Create(Product product);
    public void Delete(int productId);
    public void Update(Product product);
}

