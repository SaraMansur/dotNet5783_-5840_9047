using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;
using System.Xml.Linq;
namespace DO;

public struct Customer
{
    public int m_ID { get; set; }

    public string m_Name { get; set; }    

    public int m_Password { get; set;}

    public List<DO.OrderItem?> m_orderItems { get; set; }

    public List<DO.Order?> m_orders { get; set; }

    public string? m_Email { get; set; }

    public string? m_Adress { get; set; }
}
