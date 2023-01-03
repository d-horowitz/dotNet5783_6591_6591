using BlApi;
using DalApi;
namespace BlImplementation;
using Dal;
internal class BlOrder : BlApi.IOrder
{
    private readonly IDal Dal = DalXml.Instance;//DalApi.Factory.Get();//DalList.Instance;
    public IEnumerable<BO.OrderForList> Read()
    {
        List<BO.OrderForList> OrderList = new();
        try
        {
            Dal.Order.Read().ToList().ForEach(o => OrderList.Add(
                new BO.OrderForList
                {
                    CustomerName = o.Name
                }
                ));
        }catch (Exception ex) { }
        /*List<BO.OrderForList> OrdersList = new();
        try
        {
            IEnumerable<DO.Order> orders = Dal.Order.Read();
            foreach (var order in orders)
            {
                BO.OrderForList newOrder = new();
                newOrder.Id = order.Id;
                newOrder.CustomerName = order.Name;
                if (DateTime.Now <= order.Shipping)
                    newOrder.OrderStatus = BO.EOrderStatus.Processed;
                else if (DateTime.Now < order.Delivery)
                    newOrder.OrderStatus = BO.EOrderStatus.Shipped;
                else
                    newOrder.OrderStatus = BO.EOrderStatus.Delivered;
                int amount = 0;
                double price = 0;
                foreach (var orderItem in Dal.OrderItem.Read(oi => oi.OrderId == order.Id))
                {
                    amount++;
                    price += orderItem.Amount * orderItem.UnitPrice;

                }
                newOrder.AmountOfItems = amount;
                newOrder.TotalPrice = price;
                OrdersList.Add(newOrder);
            }
        }
        catch (DataIsEmpty ex) { throw new BO.DataIsEmpty(ex); }
        return OrdersList;
        */
    }
    public BO.Order Read(int orderId)
    {
        if (orderId <= 0)
            throw new BO.InvalidInput("id is not valid");
        try
        {
            DO.Order DOorder = Dal.Order.ReadSingle(order=>order.Id==orderId);
            BO.Order BOorder = new();
            BOorder.Id = orderId;
            BOorder.OrderCreated = DOorder.OrderCreated;
            BOorder.Shipping = DOorder.Shipping;
            BOorder.Delivery = DOorder.Delivery;
            BOorder.CustomerAddress = DOorder.Address;
            BOorder.CustomerEmail = DOorder.Email;
            BOorder.CustomerName = DOorder.Name;
            BOorder.TotalPrice = 0;
            List<BO.OrderItem> oiList = new();
            BO.OrderItem oi = new BO.OrderItem();
            foreach (var item in Dal.OrderItem.Read(orderItem=>orderItem.OrderId==orderId))
            {
                oi.Name = Dal.Product.ReadSingle(product=>product.Id==item.ProductId).Name;
                oi.ProductId = item.ProductId;
                oi.Price = item.UnitPrice;
                oi.Id = item.Id;
                oi.Amount = item.Amount;
                oi.TotalPrice = item.Amount * item.UnitPrice;
                BOorder.TotalPrice += oi.TotalPrice;
                oiList.Add(oi);
            }
            BOorder.Items = oiList;
            return BOorder;
        }
        catch (NonExistentObject ex) { throw new BO.NonExistentObject(ex); }
    }
    public BO.Order UpdateShipping(int OrderId)
    {
        try
        {
            DO.Order DOorder = Dal.Order.ReadSingle(order=>order.Id==OrderId);
            if (DOorder.Shipping < DateTime.Now)
            {
                BO.Order BOorder = new();
                DOorder.Shipping = DateTime.Now;
                Dal.Order.Update(DOorder);
                BOorder.Shipping = DateTime.Now;
                BOorder.Id = DOorder.Id;
                BOorder.OrderCreated = DOorder.OrderCreated;
                BOorder.Delivery = DOorder.Delivery;
                BOorder.CustomerAddress = DOorder.Address;
                BOorder.CustomerEmail = DOorder.Email;
                BOorder.CustomerName = DOorder.Name;
                return BOorder;
            }
            throw new NonExistentObject();
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }
    public BO.Order UpdateDelivery(int OrderId)
    {
        try
        {
            DO.Order DOorder = Dal.Order.ReadSingle(order=>order.Id==OrderId);
            if (DOorder.Delivery < DateTime.Now)
            {
                BO.Order BOorder = new();
                DOorder.Delivery = DateTime.Now;
                Dal.Order.Update(DOorder);
                BOorder.Delivery = DateTime.Now;
                BOorder.Id = DOorder.Id;
                BOorder.OrderCreated = DOorder.OrderCreated;
                BOorder.Shipping = DOorder.Shipping;
                BOorder.CustomerAddress = DOorder.Address;
                BOorder.CustomerEmail = DOorder.Email;
                BOorder.CustomerName = DOorder.Name;
                return BOorder;
            }
            throw new NonExistentObject();
        }
        catch (NonExistentObject ex)
        {
            throw new BO.NonExistentObject(ex);
        }
    }
}