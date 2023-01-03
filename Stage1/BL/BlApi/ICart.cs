using BO;
namespace BlApi;
public interface ICart
{
    public Cart Create(Cart item, int id);
    public Cart Update(Cart item, int id, int newAmount);
    public void OrderConfirmation(Cart item);
}
