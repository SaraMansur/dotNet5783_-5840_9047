using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// The function builds a new order list of the OrderForList type (for the manager screen).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> OrderList();

    /// <summary>
    /// The function receives an order ID and returns an object of type BO.Order that contains all the order details.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order orderDetails(int? orderId);

    /// <summary>
    /// The function allows the manager to update that the order has been sent to the customer.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order sendingAnInvitation(int? orderId);

    /// <summary>
    /// The function allows the manager to update that the order has been delivered to the customer.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public BO.Order orderDelivery (int? orderId);

    /// <summary>
    /// The function allows the administrator to track the order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.OrderTracking orderTracking(int? orderId);
}
