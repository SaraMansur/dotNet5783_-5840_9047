
namespace BO;

public class Order
{
    public int? m_Id { get; set; } 
    public string? m_CustomerName { get; set; }
    public string? m_CustomerMail { get; set; }
    public string? m_CustomerAdress { get; set; }
    public string? m_OrderStatus { get; set; }
    public DateTime m_OrderTime { get; set; }
    public DateTime m_ShipDate { get; set; }
    public DateTime m_DeliveryrDate { get; set; }
    public List<DO.OrderItem>? m_orderItems { get; set; }
    public int? m_TotalPrice { get; set; }
    public override string ToString() => $@"
    product id= {m_Id}
    name of Customer: {m_CustomerName}
    mail of Customer:{m_CustomerMail}
    adress of customer:{m_CustomerAdress}
    order status: {m_OrderStatus}
    total price: {m_TotalPrice}
    order time: {m_OrderTime}
    ship date: {m_ShipDate}
    delivery date: {m_DeliveryrDate}
    order item: {m_orderItems}
     ";
 
}
