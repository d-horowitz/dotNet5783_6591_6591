using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal Dal = new DalList();
}
