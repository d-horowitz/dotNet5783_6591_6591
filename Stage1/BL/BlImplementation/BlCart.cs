using BlApi;
using DalApi;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Dal;
namespace BlImplementation;
internal class BlCart : ICart
{
    private readonly IDal Dal = DalXml.Instance;//DalApi.Factory.Get();//DalList.Instance;
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
                cart.TotalPrice += BOProduct.Price;
            }
            /*int idx = -1;
            if (cart.Items != null)
                idx = cart.Items.FindIndex(item => item.Id == productId);
            DO.Product p = Dal.Product.ReadSingle(product => product.Id == productId);

            if (idx == -1)
            {
                if (p.Amount > 0)
                {
                    List<BO.OrderItem> OrderItems = new();
                    cart.Items = OrderItems;
                    BO.OrderItem orderItem = new();
                    orderItem.ProductId = p.Id;
                    orderItem.Id = BO.Config.OrderItemId;
                    orderItem.Price = p.Price;
                    orderItem.Name = p.Name;
                    orderItem.Amount = 1;
                    orderItem.TotalPrice = orderItem.Price;
                    cart.TotalPrice = orderItem.Price;
                    cart.Items.Add(orderItem);
                }
            }
            else
            {
                if (p.Amount > 0)
                {
                    cart.Items[idx].Amount++;
                    cart.Items[idx].TotalPrice += cart.Items[idx].Price;
                    cart.TotalPrice += cart.Items[idx].Price;
                }
            }*/
        }
        catch(NotEnoughInStock ex)
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
        return cart;
    }

    public BO.Cart Update(BO.Cart cart, int productId, int newAmount)
    {
        try
        {
            if (newAmount < 0)
            {
                throw new InvalidInput();
            }
            if(cart.Items == null || cart.Items.Where(oi=>oi.ProductId == productId).ToList().Count == 0)
            {
                throw new NonExistentObject();
            }
            int oiIndex = cart.Items.FindIndex(oi => oi.ProductId == productId);
            int numAdded = newAmount - cart.Items[oiIndex].Amount;
            if(numAdded > 0)
            {
                if(new BlProduct().ReadForManager(productId).AmountInStock < cart.Items[oiIndex].Amount + numAdded)
                {
                    throw new NotEnoughInStock();
                }
            }
            cart.Items[oiIndex].Amount += numAdded;
            cart.Items[oiIndex].TotalPrice += numAdded * cart.Items[oiIndex].Price;
            cart.TotalPrice += numAdded * cart.Items[oiIndex].Price;
            if(newAmount ==0)
            {
                cart.Items = cart.Items.Where(oi => oi.ProductId != productId).ToList();
            }
        }
        catch(NotEnoughInStock ex)
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
        /*int idx = -1;
        if (cart.Items != null)
            idx = cart.Items.FindIndex(item => item.Id == productId);
        if (idx == -1)
        {
            throw new BO.NonExistentObject("The item is not in the cart");
        }
        else
        {
            try
            {
                if (cart.Items[idx].Amount < newAmount)
                {
                    DO.Product p = Dal.Product.ReadSingle(product => product.Id == productId);
                    if (p.Amount > 0)
                    {
                        cart.Items[idx].Amount += newAmount;
                        cart.Items[idx].TotalPrice += cart.Items[idx].Price * newAmount;
                        cart.TotalPrice += cart.Items[idx].Price * newAmount;
                    }
                }
                else if (cart.Items[idx].Amount > newAmount && newAmount != 0)
                {
                    cart.Items[idx].Amount -= newAmount;
                    cart.Items[idx].TotalPrice -= cart.Items[idx].Price * newAmount;
                    cart.TotalPrice -= cart.Items[idx].Price * newAmount;
                }
                else if (newAmount == 0)
                {
                    cart.TotalPrice -= cart.Items[idx].Price * cart.Items[idx].Amount;
                    cart.Items[idx].Amount = 0;
                    cart.Items[idx].TotalPrice = 0;
                }
            }
            catch (NonExistentObject ex)
            {
                throw new BO.NonExistentObject(ex);
            }
            catch (InvalidInput ex)
            {
                throw new BO.InvalidInput(ex);
            }
        }*/
        return cart;
    }

    public void OrderConfirmation(BO.Cart items, string name, string mail, string Address)
    {
        try
        {
            if (items.CustomerName == "")
            {
                throw new BO.InvalidInput("Name is not valid");
            }
            if (items.CustomerAddress == "")
            {
                throw new BO.InvalidInput("Address is not valid");
            }
            if (!new EmailAddressAttribute().IsValid(mail)) { throw new BO.InvalidInput("Email is not valid"); }
            DO.Product p1;
            if (items.Items != null)
            {
                foreach (BO.OrderItem item in items.Items)
                {
                    if (item.Amount <= 0)
                    {
                        throw new BO.InvalidInput("Amount is not valid");
                    }
                    p1 = Dal.Product.ReadSingle(product => product.Id == item.ProductId);
                    if (p1.Amount < item.Amount)
                    {
                        throw new BO.Unsuccessful("Out of stock");
                    }
                }
            }
            DO.Product product = new();
            DO.Order order = new();
            order.Name = name;
            order.Address = Address;
            order.Email = mail;
            order.OrderCreated = DateTime.Now;
            order.Shipping = DateTime.MinValue;
            order.Delivery = DateTime.MinValue;
            int orderId = Dal.Order.Add(order);
            if (items.Items != null)
            {
                foreach (var i in items.Items)
                {
                    DO.OrderItem oi = new()
                    {
                        ProductId = i.ProductId,
                        OrderId = orderId,
                        Amount = i.Amount,
                        UnitPrice = i.Price
                    };
                    Dal.OrderItem.Add(oi);
                    product = Dal.Product.ReadSingle(product => product.Id == oi.ProductId);
                    product.Amount -= oi.Amount;
                    Dal.Product.Update(product);
                }
            }
        }
        catch (ObjectAlreadyExists ex)
        {
            throw new BO.ObjectAlreadyExists(ex);
        }
        catch (DataOverflow ex)
        {
            throw new BO.DataOverflow(ex);
        }
        catch (DataIsEmpty ex)
        {
            throw new BO.DataIsEmpty(ex);
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }

}
