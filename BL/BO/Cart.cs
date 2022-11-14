
namespace BO;

public class Cart
{
    public string? m_CustomerName { get; set; }
    public string? m_CustomerMail { get; set; }
    public string? m_CustomerAdress { get; set; }
    public List<DO.OrderItem>? m_orderItems { get; set; }
    public int? m_TotalPrice { get; set; }
    public override string ToString() => $@"
    customer name: {m_CustomerName}
    customer mail: {m_CustomerMail}
    customer adress: {m_CustomerAdress}
    order item: {m_orderItems}
    total price: {m_TotalPrice}";
    
}
