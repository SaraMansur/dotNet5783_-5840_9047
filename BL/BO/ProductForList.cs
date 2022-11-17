
namespace BO;

public class ProductForList
{
    public int? m_ID { get; set; }
    public string? m_NameProduct { get; set; }
    public double? m_PriceProduct { get; set; }
    public Enums.Category? m_Category { get; set; }
    public override string ToString() => $@"
    product id: {m_ID}
    name of product: {m_NameProduct}
    price of product: {m_PriceProduct}
    category of product: {m_Category}";
}
