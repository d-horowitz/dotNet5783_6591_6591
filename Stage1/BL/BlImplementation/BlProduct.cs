using BlApi;
using DalApi;
using Dal;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private readonly IDal dal = DalApi.Factory.Get() ?? throw new Exception("Failed to load Dal");//DalList.Instance;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> Read(Func<DO.Product, bool>? func = null)
    {
        try
        {
            lock (dal)
            {
                return from p in dal.Product.Read(func)
                       orderby p.Name
                       select new BO.ProductForList()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           Category = (BO.ECategory)p.Category
                       };
            }
        }
        catch (DataIsEmpty ex)
        {
            throw new BO.DataIsEmpty(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> ReadCatalog(Func<DO.Product, bool>? func = null)
    {
        try
        {
            lock (dal)
            {
                return from p in dal.Product.Read(func)
                       let isInStock = p.Amount > 0
                       select new BO.ProductItem()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           AmountInStock = p.Amount,
                           InStock = isInStock,
                           Category = (BO.ECategory)p.Category
                       };
            }
        }

        catch (DataIsEmpty ex)
        {
            throw new BO.DataIsEmpty(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product ReadForManager(int productId)
    {
        if (productId > 0)
        {
            try
            {
                lock (dal)
                {
                    DO.Product DOproduct = dal.Product.ReadSingle(product => product.Id == productId);
                    BO.Product BOproduct = new()
                    {
                        Id = DOproduct.Id,
                        Name = DOproduct.Name,
                        Price = DOproduct.Price,
                        Category = (BO.ECategory)DOproduct.Category,
                        AmountInStock = DOproduct.Amount
                    };
                    return BOproduct;
                }
            }
            catch (InvalidInput ex)
            {
                throw new BO.InvalidInput(ex);
            }
            catch (NonExistentObject ex)
            {
                throw new BO.NonExistentObject(ex);
            }
        }
        else
        {
            throw new BO.InvalidInput("id is not valid");
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem ReadForCustomer(int productId, BO.Cart cart)
    {
        if (productId > 0)
        {
            try
            {
                lock (dal)
                {
                    DO.Product DOproduct = dal.Product.ReadSingle(product => product.Id == productId);
                    BO.ProductItem BOproduct = new()
                    {
                        Id = DOproduct.Id,
                        Name = DOproduct.Name,
                        Price = DOproduct.Price,
                        Category = (BO.ECategory)DOproduct.Category,
                        InStock = DOproduct.Amount > 0,
                        AmountInStock = cart.Items.Where(itm => itm.ProductId == productId).First().Amount
                    };
                    return BOproduct;
                }
            }
            catch (InvalidInput ex)
            {
                throw new BO.InvalidInput(ex);
            }
            catch (NonExistentObject ex)
            {
                throw new BO.NonExistentObject(ex);
            }
        }
        else
        {
            throw new BO.InvalidInput("id is not valid");
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(BO.Product product)
    {
        int id;
        if (product.Name == null || product.Name == "") throw new BO.InvalidInput("name value is not valid");
        if (product.Price < 0) throw new BO.InvalidInput("price value is not valid");
        if (product.AmountInStock < 0) throw new BO.InvalidInput("amount in stock value is not valid");
        DO.Product p = new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.ECategory)product.Category,
            Amount = product.AmountInStock
        };
        try
        {
            lock (dal)
            {
                id = dal.Product.Add(p);
            }
        }
        catch (DataOverflow ex)
        {
            throw new BO.DataOverflow(ex);
        }
        catch (ObjectAlreadyExists ex)
        {
            throw new BO.ObjectAlreadyExists(ex);
        }
        return id;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int productId)
    {
        try
        {
            lock (dal)
            {
                if (dal.OrderItem.Read(oi => oi.ProductId == productId).ToList().Count != 0)
                {
                    throw new BO.ObjectAlreadyExists("Product already exists in order(s)");
                }
                dal.Product.Delete(productId);
            }
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product product)
    {
        if (product.Id < 0) throw new BO.InvalidInput("id is not valid");
        if (product.Name == null || product.Name == "") throw new BO.InvalidInput("name value is not valid");
        if (product.Price < 0) throw new BO.InvalidInput("price value is not valid");
        if (product.AmountInStock < 0) throw new BO.InvalidInput("amount in stock value is not valid");
        DO.Product p = new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.ECategory)product.Category,
            Amount = product.AmountInStock
        };
        try
        {
            lock (dal)
            {
                dal.Product.Update(p);
            }
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }
}
