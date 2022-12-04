using BlApi;
using DalApi;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace BlImplementation;
internal class BlCart : ICart
{
    private readonly IDal Dal = new Dal.DalList();
    public BO.Cart Create(BO.Cart items, int id)
    {
        try
        {
            int idx = -1;
            if (items.Items != null)
                idx = items.Items.FindIndex(item => item.ID == id);
            DO.Product p = Dal.Product.Read(id);

            if (idx == -1)
            {
                if (p.Amount > 0)
                {
                    List<BO.OrderItem> OrderItems = new();
                    items.Items = OrderItems;
                    BO.OrderItem orderItem = new();
                    orderItem.ProductId = p.Id;
                    orderItem.Id = BO.Config.OrderItemId;
                    orderItem.Price = p.Price;
                    orderItem.Name = p.Name;
                    orderItem.Amount = 1;
                    orderItem.TotalPrice = orderItem.Price;
                    items.TotalPrice = orderItem.Price;
                    items.Items.Add(orderItem);
                }
            }
            else
            {
                if (p.Amount > 0)
                {
                    items.Items[idx].Amount++;
                    items.Items[idx].TotalPrice += items.Items[idx].Price;
                    items.TotalPrice += items.Items[idx].Price;
                }
            }
        }
        catch (NonExistentObject ex) { throw new BO.NonExistentObject(ex); }
        catch (InvalidInput ex) { throw new BO.InvalidInput(ex); }
        return items;
    }

    public BO.Cart Update(BO.Cart item, int id, int newAmount)
    {
        int idx = -1;
        if (item.Items != null)
            idx = item.Items.FindIndex(item => item.ID == id);
        if (idx == -1)
        {
            throw new BO.NonExistentObject("The item is not in the cart");
        }
        else
        {
            try
            {
                if (item.Items[idx].Amount < newAmount)
                {
                    DO.Product p = Dal.Product.Read(id);
                    if (p.Amount > 0)
                    {
                        item.Items[idx].Amount += newAmount;
                        item.Items[idx].TotalPrice += item.Items[idx].Price * newAmount;
                        item.TotalPrice += item.Items[idx].Price * newAmount;
                    }
                }
                else if (item.Items[idx].Amount > newAmount && newAmount != 0)
                {
                    item.Items[idx].Amount -= newAmount;
                    item.Items[idx].TotalPrice -= item.Items[idx].Price * newAmount;
                    item.TotalPrice -= item.Items[idx].Price * newAmount;
                }
                else if (newAmount == 0)
                {
                    item.TotalPrice -= item.Items[idx].Price * item.Items[idx].Amount;
                    item.Items[idx].Amount = 0;
                    item.Items[idx].TotalPrice = 0;
                }
            }
            catch (NonExistentObject ex) { throw new BO.NonExistentObject(ex); }
            catch (InvalidInput ex) { throw new BO.InvalidInput(ex); }
        }
        return item;
    }

    public void OrderConfirmation(BO.Cart items, string name, string mail, string Address)
    {
        try
        {
            if (items.CustomerName == "") { throw new BO.InvalidInput("Name is not valid"); }
            if (items.CustomerAddress == "") { throw new BO.InvalidInput("Address is not valid"); }
            if (!new EmailAddressAttribute().IsValid(mail)) { throw new BO.InvalidInput("Email is not valid"); }
            DO.Product p1;
            if (items.Items != null)
            {
                foreach (BO.OrderItem ThisItem in items.Items)
                {
                    if (ThisItem.Amount <= 0) { throw new BO.InvalidInput("Amount is not valid"); }
                    p1 = Dal.Product.Read(ThisItem.ProductId);
                    if (p1.Amount < ThisItem.Amount) { throw new BO.Unsuccessful("Out of stock"); }
                }
            }
            DO.OrderItem oi = new();
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
                    oi.ProductId = i.ProductId;
                    oi.OrderId = orderId;
                    oi.Amount = i.Amount;
                    oi.UnitPrice = i.Price;
                    Dal.OrderItem.Add(oi);
                    product = Dal.Product.Read(oi.ProductId);
                    product.Amount -= oi.Amount;
                    Dal.Product.Update(product);
                }
            }
        }
        catch (ObjectAlreadyExists ex) { throw new BO.ObjectAlreadyExists(ex); }
        catch (DataOverflow ex) { throw new BO.DataOverflow(ex); }
        catch (DataIsEmpty ex) { throw new BO.DataIsEmpty(ex); }
        catch (NonExistentObject ex) { throw new BO.NonExistentObject(ex); }
    }

}
