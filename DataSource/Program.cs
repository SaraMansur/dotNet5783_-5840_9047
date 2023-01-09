﻿using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dal.hi.program;

namespace Dal;
public class hi
{
    public class program
    {
        static readonly Random rand = new Random();
        internal static List<DO.Product?> m_listPruducts = new List<DO.Product?>();
        internal static List<DO.Order?> m_listOreders = new List<DO.Order?>();
        internal static List<DO.OrderItem?> m_listOrderItems = new List<DO.OrderItem?>();

        static string[] nameProduct =
        {
        "Delicate Ring Silver","Gentle Ring Gold" ,"Ring inlaid with green stones","Gold ring inlaid with spotlight stones",
        "Silver Hoop earrings","Hexagonal Hoop Gold earrings","Stud earrings","Gold bracelet","Silver bracelet","Silver watche","Ross watche",
        "Gold necklace","Silver necklace"
    };

        static string[] firstName = { "David", "Sara", "Yael", "Lea", "Yair", "Yossi", "Meir", "Tali" };

        static string[] lastName = { "Cohen", "Ochana", "Mansur", "Levi", "Dahan", "Ben-Califa", "Zafrani", "Deryi" };

        static string[] citie = { "Haifa", "Jerusalem", "Rechassim", "Tveria", "Netivot", "Rechovot", "Gadera", "Gadera", "Tveria", "Jerosulem" };

        static string[] street = { "Ben Guryon", "Harimonim", "Hazait", "Vaitzman", "Begin", "Hatamar", "Savion", "Haoranim" };
        public int nu = 100000;
        public int num = 100000;
        private static void s_Initialize()
        {

            for (int i = 0; i < 13; i++) //The loop initializes 10 products.
            {
                Product product = new Product();
                product.m_ID = rand.Next(100000, 999999);//Generate a random number with 6 digits.
                for (int j = 0; j < m_listPruducts.Count; j++) //Checking if the ID number exists.
                {
                    if (m_listPruducts.Exists(match => match?.m_ID == product.m_ID))
                    { product.m_ID = rand.Next(100000, 999999); j = 0; } //If found, replace ID number.
                }
                product.m_Name = nameProduct[i]; //initialization product name
                product.m_Price = rand.Next(200, 1000); //Product price initialization.
                product.m_InStock = rand.Next(10, 30); //Initialize quantity in stock for product.

                if (product.m_Name.Contains("Necklace") || product.m_Name.Contains("necklace"))
                    product.m_Category = Enums.Category.Necklaces;
                if (product.m_Name.Contains("Ring") || product.m_Name.Contains("ring"))
                    product.m_Category = Enums.Category.Rings;
                if (product.m_Name.Contains("Bracelet") || product.m_Name.Contains("bracelet"))
                    product.m_Category = Enums.Category.Bracelets;
                if (product.m_Name.Contains("Earring") || product.m_Name.Contains("earring"))
                    product.m_Category = Enums.Category.Earrings;
                if (product.m_Name.Contains("Watche") || product.m_Name.Contains("watche"))
                    product.m_Category = Enums.Category.Watches;
                m_listPruducts.Add(product);
            }

            for (int i = 0; i < 20; i++)//The loop initializes 20 orders.
            {
                DO.Order order = new DO.Order();

                //order.m_ID = nu++;

                string custumerFirstName = firstName[rand.Next(0, 7)];
                string custumerLastName = lastName[rand.Next(0, 7)];

                order.m_CustomerName = custumerFirstName + " " + custumerLastName; //Initialize the Customer name.
                order.m_CustomerEmail = custumerFirstName + rand.Next(100, 999) + "@gmail.com"; //Initialize the customer email.
                order.m_CustomerAdress = street[rand.Next(0, 7)] + " " + rand.Next(1, 100) + " " + citie[rand.Next(0, 9)]; //Client address initialization.

                if (i >= 10 && i < 16)//80 from orders also have a delivery date.
                {
                    order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-16, -14), 0, 0, 0));
                    order.m_ShipDate = order.m_OrderTime?.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0));
                    order.m_DeliveryrDate = DateTime.MinValue;
                }

                if (i >= 0 && i < 10)//To 60 percent of the 80 percent of the orders that also have a delivery date, have a delivery date.
                {
                    order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-30, -15), 0, 0, 0));
                    order.m_ShipDate = order.m_OrderTime?.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0));
                    order.m_DeliveryrDate = order.m_ShipDate?.Add(new TimeSpan(0, rand.Next(1, 24), 0, 0));
                }

                if (i >= 16 && i < 20)//20 percent of orders have no delivery date and delivery date.
                {
                    order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-13, -1), 0, 0, 0));
                    order.m_ShipDate = DateTime.MinValue;
                    order.m_DeliveryrDate = DateTime.MinValue;
                }
                m_listOreders.Add(order);
            }
            int num = 100000;
            for (int i = 0; i < 40; i++)//The loop initializes 40 ordersItems.
            {
                OrderItem orderItem = new OrderItem();
                orderItem.m_ID = num++;
                Product p = (Product)m_listPruducts[rand.Next(0, 9)];
                orderItem.m_OrderId = ((Order)m_listOreders[i / 2]).m_ID;
                for (int k = 0, j = 0; j < m_listOrderItems.Count; j++) //Test that each order will be 1 to 4 order items
                {
                    if (m_listOrderItems[j]?.m_OrderId == orderItem.m_OrderId)
                    {
                        k++;
                        if (k > 4) { orderItem.m_OrderId = ((Order)m_listOreders[rand.Next(0, 19)]).m_ID; j = 0; }
                    }
                }
                orderItem.m_ProductId = p.m_ID;
                for (int j = 0; j < m_listOrderItems.Count; j++)  //An examination that this item does not exist in the current order.
                {
                    if (m_listOrderItems[j]?.m_ProductId == orderItem.m_ProductId && m_listOrderItems[j]?.m_OrderId == orderItem.m_OrderId)
                    { //If the item exists.
                        p = (Product)m_listPruducts[rand.Next(0, 9)];
                        orderItem.m_ProductId = p.m_ID; j = 0;//The price is equal to double price.
                    }
                }
                orderItem.m_amount = rand.Next(1, 4); //Random number of product amount.
                orderItem.m_Price = orderItem.m_amount * p.m_Price; //
                m_listOrderItems.Add(orderItem);
            }
        }


        public class Config
        {
            int IdOrder;
            int IdOrderItem;
            Config()
            {
                IdOrderItem = 100039;
                IdOrder = 100019;

            }
        }


        static void Main(string[] args)
        {
            //s_Initialize();
            //private static DalApi.IDal? dal = DalApi.Factory.Get();
            List<Order?> orderlist = m_listOreders;
            List<OrderItem?> orderitemlist = m_listOrderItems;
            List<Product?> productlist = m_listPruducts;
            string path = @"Config.xml";
            string path1 = @"Orders.xml";
            string path2 = @"Product.xml";
            XElement? config;
            config = new XElement("Config", new XElement("IdOrder", 100019), new XElement("IdOrderItem", 100039));
            config.Save(path);
            //XMLTools.SaveListToXML<OrderItem?>(orderlist, path);

            // XMLTools.SaveListToXML<OrderItem?>(orderitemlist, path1);
            //XMLTools.SaveListToXML<Product?>(productlist, path2);
            Console.WriteLine("jsj");
            //List<Product> list2 = XMLTools.LoadListFromXML<Product>(path2);    
        }
    }
}