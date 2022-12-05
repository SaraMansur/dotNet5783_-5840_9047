using DO;
namespace DalApi;

public interface IOrder:ICrud<Order>
{
    /// <summary>
    /// Gets the order by order id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    //Order GetbyID(int ID);
}
