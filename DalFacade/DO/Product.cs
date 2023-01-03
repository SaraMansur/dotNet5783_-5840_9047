
namespace DO;
/// <summary>
/// 
/// </summary>
public struct Product
{
    public int m_ID { get; set; }
    public string? m_Name { get; set; }
    public double m_Price { get; set; }
    public int m_InStock { get; set; }
    public Enums.Category? m_Category { get; set; }
    public string m_ImageSource { get; set; }
    public override string ToString() => ToStringExtension.ToStringProperty(this);
}