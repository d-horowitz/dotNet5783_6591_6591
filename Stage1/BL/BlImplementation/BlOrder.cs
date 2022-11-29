using BlApi;
using DalApi;
using Dal;
namespace BlImplementation;
internal class BlOrder:BlApi.IOrder
{
    private IDal Dal = new DalList();
}