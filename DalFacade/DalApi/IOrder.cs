using DO;
namespace DalApi;

public interface IOrder:ICrud<Order>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    Order GetbyID(int ID);
}
