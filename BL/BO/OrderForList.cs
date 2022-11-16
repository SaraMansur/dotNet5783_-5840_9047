
namespace BO;

public class OrderForList
{
    public int? m_Id { get; set; }
    public string? m_CustomerName { get; set; }
    public double? m_OrderStatus { get; set; }
    public int? m_AmountItems { get; set; }
    public double? m_TotalPrice { get; set; }
    public override string ToString() => $@"
    product id= {m_Id}
    name of Customer: {m_CustomerName}
    order status: {m_OrderStatus}
    total price: {m_TotalPrice}
    amount items: {m_AmountItems}
     ";
}
