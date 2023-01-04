using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private readonly IDal Dal = DalApi.Factory.Get();//DalList.Instance;
    public IEnumerable<BO.ProductForList> Read(Func<DO.Product, bool>? func = null)
    {
        List<BO.ProductForList> productsList = new();
        try
        {
            IEnumerable<DO.Product> products = Dal.Product.Read(func);
            foreach (var p in products)
            {
                BO.ProductForList item = new()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category = (BO.ECategory)p.Category
                };
                productsList.Add(item);
            }
            return productsList;
        }
        catch (DataIsEmpty ex)
        {
            throw new BO.DataIsEmpty(ex);
        }
    }
    public IEnumerable<BO.ProductItem> ReadCatalog()
    {
        List<BO.ProductItem> catalog = new();
        try
        {
            IEnumerable<DO.Product> c = Dal.Product.Read();
            foreach (var item in c)
            {
                BO.ProductItem pi = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Category = (BO.ECategory)item.Category,
                    Amount = item.Amount,
                    InStock = item.Amount > 0
                };
                catalog.Add(pi);
            }
            return catalog;
        }

        catch (DataIsEmpty ex)
        {
            throw new BO.DataIsEmpty(ex);
        }
    }
    public BO.Product ReadForManager(int productId)
    {
        if (productId > 0)
        {
            try
            {
                DO.Product DOproduct = Dal.Product.ReadSingle(product => product.Id == productId);
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
    public BO.ProductItem ReadForCustomer(int productId, BO.Cart cart)
    {
        if (productId > 0)
        {
            try
            {
                DO.Product DOproduct = Dal.Product.ReadSingle(product => product.Id == productId);
                BO.ProductItem BOproduct = new()
                {
                    Id = DOproduct.Id,
                    Name = DOproduct.Name,
                    Price = DOproduct.Price,
                    Category = (BO.ECategory)DOproduct.Category,
                    InStock = DOproduct.Amount > 0,
                    Amount = cart.Items.Where(itm => itm.ProductId == productId).First().Amount
                };
                return BOproduct;
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
        try { id = Dal.Product.Add(p); }
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
    public void Delete(int productId)
    {
        /*foreach (var oi in Dal.OrderItem.Read(orderItem => orderItem.ProductId == productId))
        {
            DO.Order order = Dal.Order.ReadSingle(order => order.Id == oi.OrderId);
            if (DateTime.Now < order.Shipping)
            {
                throw new BO.ProductExistsAtSomeOrder();
            }
        }*/
        try
        {
        if(Dal.OrderItem.Read(oi=>oi.ProductId==productId).ToList().Count != 0)
            {
                throw new BO.ObjectAlreadyExists("Product already exists in order(s)");
            }
            Dal.Product.Delete(productId);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }

    }
    public void Update(BO.Product product)
    {
        if (product.Id < 0) throw new BO.InvalidInput("id is not valid");
        if (product.Name == null|| product.Name == "") throw new BO.InvalidInput("name value is not valid");
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
            Dal.Product.Update(p);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }
}
