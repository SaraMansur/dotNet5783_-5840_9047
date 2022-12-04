namespace BO;

public class ProductItem
{
    /// <summary>
    /// the id of the product item
    /// </summary>
    public int m_ID { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? m_NameProduct { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double m_PriceProduct { get; set; }
    /// <summary>
    /// the category of the product
    /// </summary>
    public Enums.Category? m_Category { get; set; }
    /// <summary>
    /// the amount of the product in the stock
    /// </summary>
    public bool m_InStock { get; set; }
    /// <summary>
    /// the amount of the product in the cart
    /// </summary>
    public int m_AmountInCart { get; set; }
    /// <summary>
    /// this function print all the features of the product item
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    product item id: {m_ID}
    name of product: {m_NameProduct}
    price of product: {m_PriceProduct}
    category of product: {m_Category}
    If the product is in stock: {m_InStock}
    amount of product: {m_AmountInCart}";
}
