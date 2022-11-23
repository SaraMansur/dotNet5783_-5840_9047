namespace BO;

public class Product
{
    /// <summary>
    /// the id of the product
    /// </summary>
    public int m_Id { get; set; } 
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? m_Name { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double m_Price { get; set; }
    /// <summary>
    /// the category (type) of the product
    /// </summary>
    public Enums.Category? m_Category { get; set; }
    /// <summary>
    /// the amount of the product in the stock
    /// </summary>
    public int? m_InStock { get; set; }
    /// <summary>
    /// This function will print all the features of the product
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    product id= {m_Id}
    name: {m_Name}
    category: {m_Category}
    price: {m_Price}
    amount in stock: {m_InStock}";
}
