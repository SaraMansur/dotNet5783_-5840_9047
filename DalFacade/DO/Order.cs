﻿using static DO.Enums;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// 
/// </summary>
public struct Order
{
    public int m_ID { get; set; }
    public string? m_CustomerName { get; set; }
    public string? m_CustomerEmail { get; set; }
    public string? m_CustomerAdress { get; set; }
    public DateTime? m_OrderTime { get; set; }
    public DateTime? m_ShipDate { get; set; }
    public DateTime? m_DeliveryrDate { get; set; }
    public override string ToString() => ToStringExtension.ToStringProperty(this);
}