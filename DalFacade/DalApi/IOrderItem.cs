using DO;
namespace DalApi;

public interface IOrderItem:ICrud<OrderItem>
{
    OrderItem GetbyID(int ID);
    OrderItem GetbyProductAndOrder(int? PID, int? OID);
    IEnumerable<OrderItem> GetOrderItems(int? orderId);
}
