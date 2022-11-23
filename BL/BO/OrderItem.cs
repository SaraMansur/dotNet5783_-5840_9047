using System.Diagnostics;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// the id of the order item
    /// </summary>
    public int m_ID { get; set; }
    /// <summary>
    /// the id of the product item
    /// </summary>
    public int? m_IdProduct { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? m_NameProduct { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double? m_PriceProduct { get; set; }
    /// <summary>
    /// the amount of the product in the cart
    /// </summary>
    public int m_AmountInCart { get; set; }
    /// <summary>
    /// the total price of the item
    /// </summary>
    public double m_TotalPriceItem { get; set; }
    /// <summary>
    /// this function print all the attributes of the order item
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    order item id= {m_ID}
    product id: {m_IdProduct}
    name of product:{m_NameProduct}
    price of product: {m_PriceProduct}
    amount in cart: {m_AmountInCart}
    total price: {m_TotalPriceItem}
     ";
}
