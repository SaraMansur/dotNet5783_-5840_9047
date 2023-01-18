
using DalApi;
using DO;

namespace Dal;

sealed internal class DalList:IDal
{
    private DalList() { }
    public IProduct Product=>new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public ICustomer Customer=>new DalCustomer();   
    public static IDal Instance { get; } = new DalList();
}
