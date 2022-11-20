
using System.Diagnostics;

namespace BO;

public class OrderItem
{
    public int m_ID { get; set; }
    public int? m_IdProduct { get; set; }
    public string? m_NameProduct { get; set; }
    public double? m_PriceProduct { get; set; }
    public int? m_AmountInCart { get; set; }
    public double? m_TotalPriceItem { get; set; }
    public override string ToString() => $@"
    order item id= {m_ID}
    product id: {m_IdProduct}
    name of product:{m_NameProduct}
    price of product: {m_PriceProduct}
    amount in cart: {m_AmountInCart}
    total price: {m_TotalPriceItem}
     ";
}
