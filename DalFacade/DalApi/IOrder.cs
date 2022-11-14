using DO;
namespace DalApi;

public interface IOrder:ICrud<Order>
{
    Order GetbyID(int ID);
}
