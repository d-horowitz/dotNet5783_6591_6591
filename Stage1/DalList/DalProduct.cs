using DalApi;
using DO;
namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product p)
    {
        p.Id = DataSource.Config.ProductId;
        DataSource._products.Add(p);
        return p.Id;
    }
    /*
    public Product Read(int productId)
    {
        foreach (Product p in DataSource._products)
        {
            if (p.Id == productId) { return p; }
        }
        throw new Exception("Product not found");
    }
    */
    public Product ReadSingle(Func<Product, bool> func)
    {
        Predicate<Product> myFunc = new(func);
        return DataSource._products.Find(myFunc);
    }
    public IEnumerable<Product> Read(Func<Product, bool>? func = null)
    {
        IEnumerable<Product> products;
        if (func != null)
            products = new List<Product>(DataSource._products.Where(func));
        else
            products = new List<Product>(DataSource._products);
        return products;
    }
    public void Delete(int productId)
    {
        if (!DataSource._products.Remove(new Product { Id = productId }))
            throw new Exception("Product not found");
    }
    public void Update(Product p)
    {
        for (int i = 0; i < DataSource._products.Count; i++)
        {
            if (DataSource._products[i].Id == p.Id)
            {
                DataSource._products[i] = p;
                return;
            }
        }
        throw new Exception("Product not found");
    }
}