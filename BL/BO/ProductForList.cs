namespace BO;

public class ProductForList
{
    /// <summary>
    /// the id of the product
    /// </summary>
    public int? m_ID { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? m_NameProduct { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double? m_PriceProduct { get; set; }
    /// <summary>
    /// the category of the product
    /// </summary>
    public Enums.Category? m_Category { get; set; }
    /// <summary>
    /// this function print all the features of the product for list
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    product id: {m_ID}
    name of product: {m_NameProduct}
    price of product: {m_PriceProduct}
    category of product: {m_Category}";
}
