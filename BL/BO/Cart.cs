namespace BO;

public class Cart
{
    /// <summary>
    /// the name of the customrt
    /// </summary>
    public string? m_CustomerName { get; set; }
    /// <summary>
    /// the meil of the customer
    /// </summary>
    public string? m_CustomerMail { get; set; }
    /// <summary>
    /// the address of the customer
    /// </summary>
    public string? m_CustomerAdress { get; set; }
    /// <summary>
    /// list of the items that in the cart of the customer
    /// </summary>
    public List<BO.OrderItem?>? m_orderItems { get; set; }
    /// <summary>
    /// the price of all the cart
    /// </summary>
    public double m_TotalPrice { get; set; }
    /// <summary>
    /// This function will print all the cart attributes
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    customer name: {m_CustomerName}
    customer mail: {m_CustomerMail}
    customer adress: {m_CustomerAdress}
    total price: {m_TotalPrice}
    order item: {'\n'}
    {string.Join('\n', m_orderItems)}";
}
