using BO;
namespace BlApi;
public interface ICart
{
    public Cart Create(Cart cart, int productId);
    public Cart Update(Cart cart, int productId, int newAmount);
    public int OrderConfirmation(Cart cart);
}
