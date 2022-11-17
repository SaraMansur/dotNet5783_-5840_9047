
namespace BO;

public class ProductItem
{
    public int? m_ID { get; set; }
    public string? m_NameProduct { get; set; }
    public double? m_PriceProduct { get; set; }
    public Enums.Category? m_Category { get; set; }
    public bool? m_InStock { get; set; }
    public int? m_AmountInCart { get; set; }
    public override string ToString() => $@"
    product item id: {m_ID}
    name of product: {m_NameProduct}
    price of product: {m_PriceProduct}
    category of product: {m_Category}
    If the product is in stock: {m_InStock}
    amount of product: {m_AmountInCart}";
}
