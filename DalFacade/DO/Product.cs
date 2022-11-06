
using static DO.Enums;
using System.Diagnostics;
using System.Xml.Linq;
//
namespace DO;
/// <summary>
/// 
/// </summary>
public struct Product
{
    public int? m_ID { get; set; }
    public string? m_Name { get; set; }
    public double? m_Price { get; set; }
    public int? m_InStock { get; set; }
    public Enums.Category? m_Category { get; set; }
    public Enums.Gender? m_Gender { get; set; }
    public Enums.Description? m_Description { get; set; }
    public override string ToString() => $@"
    product id= {m_ID}: {m_Name}, 
    category - {m_Category}
    genser- {m_Gender}
    description:{m_Description}
    price: {m_Price}
    amount in stock: {m_InStock}";

}


