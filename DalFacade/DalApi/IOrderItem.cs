using DO;
namespace DalApi;

public interface IOrderItem:ICrud<OrderItem>
{
    /// <summary>
    /// Gets the products on order by order id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    //OrderItem GetbyID(int ID);

    /// <summary>
    /// Gets the products on order by order id and product id
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="OID"></param>
    /// <returns></returns>
    //OrderItem GetbyProductAndOrder(int? PID, int? OID);

    /// <summary>
    /// Gets the list of products on order by order id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    //IEnumerable<OrderItem?> GetOrderItems(int? orderId);
}
