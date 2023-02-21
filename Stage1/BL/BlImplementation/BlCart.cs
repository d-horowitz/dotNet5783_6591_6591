using BlApi;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlCart : ICart
{
    private readonly IDal dal = DalApi.Factory.Get() ?? throw new Exception("Failed to load Dal");//DalList.Instance;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Create(BO.Cart cart, int productId)
    {
        try
        {
            if (cart.Items != null && cart.Items.Where(oi => oi.ProductId == productId).ToList().Count != 0)
            {
                int oiIndx = cart.Items.FindIndex(oi => oi.ProductId == productId);
                if (new BlProduct().ReadForManager(productId).AmountInStock > cart.Items[oiIndx].Amount)
                {
                    cart.Items[oiIndx].Amount++;
                    cart.Items[oiIndx].TotalPrice += cart.Items[oiIndx].Price;
                    cart.TotalPrice += cart.Items[oiIndx].Price;
                }
                else
                {
                    throw new NotEnoughInStock();
                }
            }
            else
            {
                BO.Product BOProduct = new BlProduct().ReadForManager(productId);
                if (BOProduct.AmountInStock <= 0)
                {
                    throw new NotEnoughInStock();
                }
                if (cart.Items == null)
                {
                    cart.Items = new List<BO.OrderItem>();
                }
                cart.Items.Add(
                    new BO.OrderItem
                    {
                        Id = BO.Config.OrderItemId,
                        Amount = 1,
                        Name = BOProduct.Name,
                        Price = BOProduct.Price,
                        ProductId = productId,
                        TotalPrice = BOProduct.Price
                    }
                );
                cart.TotalPrice = cart.Items.Sum(oi => oi.TotalPrice);
            }
            return cart;
        }
        catch (NotEnoughInStock ex)
        {
            throw new BO.NotEnoughInStock(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Update(BO.Cart cart, int productId, int newAmount)
    {
        try
        {
            if (newAmount < 0)
            {
                throw new InvalidInput();
            }
            if (cart.Items == null || !(from oi in cart.Items
                                        where oi.ProductId == productId
                                        select oi).Any())
            {
                throw new NonExistentObject();
            }
            int oiIndex = cart.Items.FindIndex(oi => oi.ProductId == productId);
            int numAdded = newAmount - cart.Items[oiIndex].Amount;
            if (numAdded > 0)
            {
                if (new BlProduct().ReadForManager(productId).AmountInStock < cart.Items[oiIndex].Amount + numAdded)
                {
                    throw new NotEnoughInStock();
                }
            }
            cart.Items[oiIndex].Amount += numAdded;
            cart.Items[oiIndex].TotalPrice = cart.Items[oiIndex].Price * cart.Items[oiIndex].Amount;
            if (newAmount == 0)
            {
                cart.Items = (from oi in cart.Items
                              where oi.ProductId != productId
                              select oi).ToList();
            }
            cart.TotalPrice = cart.Items.Sum(oi => oi.TotalPrice);
            return cart;
        }
        catch (NotEnoughInStock ex)
        {
            throw new BO.NotEnoughInStock(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int OrderConfirmation(BO.Cart cart)
    {
        try
        {
            if (cart.CustomerName == null || cart.CustomerName == "" || cart.CustomerAddress == null || cart.CustomerAddress == "" || cart.CustomerEmail == null || cart.CustomerEmail == "")
            {
                throw new InvalidInput();
            }
            if ((from oi in cart.Items
                 where oi.Amount <= 0
                 select oi
                ).Any())
            {
                throw new InvalidInput();
            }
            if ((from oi in cart.Items
                 where oi.Amount > new BlProduct().ReadForManager(oi.ProductId).AmountInStock
                 select oi
                ).Any())
            {
                throw new NotEnoughInStock();
            }
            lock (dal)
            {
                int OrderId = dal.Order.Add(
                    new DO.Order
                    {
                        Name = cart.CustomerName,
                        Address = cart.CustomerAddress,
                        Email = cart.CustomerEmail,
                        OrderCreated = DateTime.Now,
                        Shipping = DateTime.MinValue,
                        Delivery = DateTime.MinValue
                    }
                    );
                cart.Items.ForEach((oi) =>
                {
                    DO.Product product = dal.Product.ReadSingle(p => p.Id == oi.ProductId);
                    dal.Product.Update(new DO.Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Category = product.Category,
                        Price = product.Price,
                        Amount = product.Amount - oi.Amount
                    });
                    dal.OrderItem.Add(new DO.OrderItem
                    {
                        OrderId = OrderId,
                        ProductId = oi.ProductId,
                        Amount = oi.Amount,
                        UnitPrice = oi.Price
                    });
                });
                return OrderId;
            }
        }
        catch (InvalidInput ex)
        {
            throw new BO.InvalidInput(ex);
        }
        catch (NotEnoughInStock ex)
        {
            throw new BO.NotEnoughInStock(ex);
        }
    }

}
