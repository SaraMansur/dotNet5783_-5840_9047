using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal;
using DO;
using System.Collections;

using static Dal.DataSource;
using System.Data;

namespace Dal;
internal static class DataSource
{
    static DataSource() {
        s_Initialize();
        SaveProductListLinq(m_listPruducts);
        SaveOrdertListLinq(m_listOreders);
        SaveOrderItemtListLinq(m_listOrderItems);

    }//difult constructor.

    static readonly Random rand = new Random();

    private static XElement initialize;
    private static readonly string productPath = @"Product.xml";
    private static readonly string OrderPath = @"Order.xml";
    private static readonly string OrderItemPath = @"OrderItem.xml";

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

            order.m_ID = Config.getOrderId();

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

        for (int i = 0; i < 40; i++)//The loop initializes 40 ordersItems.
        {
            OrderItem orderItem = new OrderItem();
            orderItem.m_ID = Config.getOrderItemId();
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


    private static void SaveProductListLinq(List<DO.Product?> listPruducts)
    {
        var v = from p in listPruducts
                select new XElement("Product",
                    new XElement("Id", p?.m_ID),
                    new XElement("Name", p?.m_Name),
                    new XElement("InStock", p?.m_InStock),
                    new XElement("Price", p?.m_Price)
                    );

        initialize = new XElement("Product", v);
        initialize.Save(productPath);
    }

    private static void SaveOrdertListLinq(List<DO.Order?> listOrders)
    {
        var v = from p in listOrders
                select new XElement("Order",
                    new XElement("id", p?.m_ID),
                    new XElement("Name", p?.m_CustomerName),
                    new XElement("Email", p?.m_CustomerEmail),
                    new XElement("Adress", p?.m_CustomerAdress),
                    new XElement("OrderTime", p?.m_OrderTime),
                    new XElement("ShipDate", p?.m_ShipDate),
                    new XElement("DeliveryrDate", p?.m_DeliveryrDate)
                    );

        initialize = new XElement("Order", v);
        initialize.Save(OrderPath);
    }

    private static void SaveOrderItemtListLinq(List<DO.OrderItem?> listOrderItems)
    {
        var v = from p in listOrderItems
                select new XElement("OrderItem",
                    new XElement("id", p?.m_ID),
                    new XElement("name", p?.m_ProductId),
                    new XElement("firstName", p?.m_OrderId),
                    new XElement("lastName", p?.m_Price),
                    new XElement("name", p?.m_amount)
                    );

        initialize = new XElement("OrderItem", v);
        initialize.Save(OrderItemPath);
    }

    internal class Config
    {
        private static XElement _configValue;
        private static string _Path = @"Config.xml";
        private static void LoadData()
        {
            try { _configValue = XElement.Load(_Path); }

            catch { throw new Exception("File upload problem"); }
        }

        static Config()
        {
            LoadData();
            if (_configValue == null)
            {
                _configValue = new XElement("config",
                    new XElement("orderId", 100000),
                    new XElement("orderItemId", 100000));
            }
        }

        public static int getOrderId()
        {
            LoadData();
            XElement OrderId = _configValue.Element("config")!.Element("orderId")!;
            OrderId.Value = (Convert.ToInt32(OrderId.Value) + 1).ToString();
            _configValue.Save(_Path);
            return Convert.ToInt32(OrderId.Value);
        }

        public static int getOrderItemId()
        {
            LoadData();
            XElement orderItemId = _configValue.Element("config")!.Element("orderItemId")!;
            orderItemId.Value = (Convert.ToInt32(orderItemId.Value) + 1).ToString();
            _configValue.Save(_Path);
            return Convert.ToInt32(orderItemId.Value);
        }
    }
}
