using BlApi;
namespace BlImplementation;
sealed public class Bl : IBl
{
    public IProduct Product => new BlProduct();
    public ICart Cart => new BlCart();
    public IOrder Order => new BlOrder();
}
