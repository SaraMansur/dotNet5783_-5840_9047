﻿using static DO.Enums;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// 
/// </summary>
public struct OrderItem
{
    public int m_ID { get; set; }
    public int m_ProductId { get; set; }
    public int m_OrderId { get; set; }
    public double m_Price { get; set; }
    public int m_amount { get; set; }
    public override string ToString() => ToStringExtension.ToStringProperty(this);
}