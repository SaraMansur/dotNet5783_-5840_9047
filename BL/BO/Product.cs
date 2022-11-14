

namespace BO;

public class Product
{
    public int? m_Id { get; set; } 
    public string? m_Name { get; set; }
    public int? m_Price { get; set; }
    public Enums.Category? m_Category { get; set; }
    public int? m_InStok { get; set; }
    public override string ToString() => $@"
    product id= {m_Id}
    name: {m_Name}
    category: {m_Category}
    price: {m_Price}
    amount in stock: {m_InStok}";
}
