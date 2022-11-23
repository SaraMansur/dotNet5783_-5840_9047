using DO;
namespace DalApi;

public interface IOrderItem:ICrud<OrderItem>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    OrderItem GetbyID(int ID);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="OID"></param>
    /// <returns></returns>
    OrderItem GetbyProductAndOrder(int? PID, int? OID);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    IEnumerable<OrderItem> GetOrderItems(int? orderId);
}
