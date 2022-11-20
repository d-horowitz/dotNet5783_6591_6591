using DalApi;
using DO;
namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product p)
    {
        p.Id = DataSource.Config.ProductId;
        DataSource._products[DataSource.Config.ProductIndex] = p;
        return p.Id;
    }
    public Product Read(int productId)
    {
        foreach (Product p in DataSource._products)
        {
            if (p.Id == productId) { return p; }
        }
        throw new Exception("Product not found");
    }
    public IEnumerable<Product> Read() { 
        Product[] products = new Product[DataSource.Config._productIndex];
        for (int i = 0; i < products.Length; i++)
        {
            products[i] = DataSource._products[i];
        }
        return products;
    }
    public void Delete(int productId)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config._productIndex--; i++)
        {
            if (DataSource._products[i].Id == productId || found)
            {
                found = true;
                DataSource._products[i] = DataSource._products[i + 1];
            }
        }
        if (!found)
        {
            DataSource.Config._productIndex++;
            throw new Exception("Product not found");
        }
    }
    public void Update(Product p)
    {
        for (int i = 0; i < DataSource.Config._productIndex; i++)
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
//{
//    public int Create(Product p)
//    {
//        p.Id = DataSource.Config.ProductId;
//        DataSource._products[DataSource.Config.ProductIndex] = p;
//        return p.Id;
//    }
//    public Product Read(int productId)
//    {
//        foreach (Product p in DataSource._products)
//        {
//            if (p.Id == productId) { return p; }
//        }
//        throw new Exception("Product not found");
//    }
//    public Product[] Read()
//    {
//        Product[] products = new Product[DataSource.Config._productIndex];
//        for (int i = 0; i < products.Length; i++)
//        {
//            products[i] = DataSource._products[i];
//        }
//        return products;
//    }
//    public void Delete(int productId)
//    {
//        bool found = false;
//        for (int i = 0; i < DataSource.Config._productIndex--; i++)
//        {
//            if (DataSource._products[i].Id == productId || found)
//            {
//                found = true;
//                DataSource._products[i] = DataSource._products[i+1];
//            }
//        }
//        if (!found)
//        {
//            DataSource.Config._productIndex++;
//            throw new Exception("Product not found");
//        }
//    }
//    public void Update(Product p)
//    {
//        for (int i = 0; i < DataSource.Config._productIndex; i++)
//        {
//            if (DataSource._products[i].Id==p.Id)
//            {
//                DataSource._products[i] = p;
//                return;
//            }
//        }
//        throw new Exception("Product not found");
//    }
//}

