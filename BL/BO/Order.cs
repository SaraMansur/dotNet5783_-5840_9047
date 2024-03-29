﻿using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// the id of the order
    /// </summary>
    public int m_Id { get; set; }
    /// <summary>
    /// the name of the customrt
    /// </summary>

    //private string? CustomerName;
    public string? m_CustomerName { get; set; }

    /// <summary>
    /// the meil of the customer
    /// </summary>
    //private string? CustomerMail;
    public string? m_CustomerMail { get; set; } 
    /// <summary>
    /// the address of the customer
    /// </summary>
   // private string? CustomerAdress;
    public string? m_CustomerAdress { get; set; }   

    public Enums.Status? m_OrderStatus { get; set; }
    /// <summary>
    /// the total price of the order
    /// </summary>
   // public double m_TotalPrice { get; set; }
   // private double TotalPrice;
    public double m_TotalPrice { get; set; }    
    /// <summary>
    /// the time that the order made
    /// </summary>
    /// 
    public DateTime? m_OrderTime { get; set; }
    /// <summary>
    /// the time that the order shiped
    /// </summary>
    public DateTime? m_ShipDate { get; set; }
    /// <summary>
    /// The time the order was delivered
    /// </summary>
    public DateTime? m_DeliveryrDate { get; set; }
    /// <summary>
    /// list of the item that in the order
    /// </summary>
    public List<BO.OrderItem?>? m_orderItems { get; set; }

   // public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// This function will print all order attributes
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    order id= {m_Id}
    name of Customer: {m_CustomerName}
    mail of Customer:{m_CustomerMail}
    adress of customer:{m_CustomerAdress}
    order status: {m_OrderStatus}
    total price: {m_TotalPrice}
    order time: {m_OrderTime}
    ship date: {m_ShipDate}
    delivery date: {m_DeliveryrDate}
    order items: {'\n'}  
    {string.Join('\n', m_orderItems)} 
     ";
}
