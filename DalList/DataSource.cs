using Dal;
using DO;
using System.Collections;
using static Dal.DataSource;

namespace Dal;
internal static class DataSource
{
    static readonly Random rand = new Random();

    internal static List<Product?> m_listPruducts = new List<Product?>();
    internal static List<Order?> m_listOreders = new List<Order?>();
    internal static List<OrderItem?> m_listOrderItems = new List<OrderItem?>();
    internal static List<Customer?> m_listCustomer = new List<Customer?>();

    static DataSource() => s_Initialize(); //difult constructor.

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
            Order order = new Order();
            order.m_ID = Config.orderId;

            string custumerFirstName = firstName[rand.Next(0, 7)];
            string custumerLastName = lastName[rand.Next(0, 7)];

            order.m_CustomerName = custumerFirstName + " " + custumerLastName; //Initialize the Customer name.
            order.m_CustomerEmail = custumerFirstName + rand.Next(100, 999) + "@gmail.com"; //Initialize the customer email.
            order.m_CustomerAdress = street[rand.Next(0, 7)] + " " + rand.Next(1, 100) + " " + citie[rand.Next(0, 9)]; //Client address initialization.

            if (i >= 10 && i < 16)//80% from orders also have a Ship date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-16, -14), 0, 0, 0));//The order date was made before: 14 to 16 days
                order.m_ShipDate = order.m_OrderTime?.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0));//Delivery date is made within 14 business days
                order.m_DeliveryrDate = DateTime.MinValue;
            }

            if (i >= 0 && i < 10)//To 60% of the 80% of the orders that also have a Ship date, have a delivery date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-30, -15), 0, 0, 0)); ////The order date was made before: 15 to 30 days
                order.m_ShipDate = order.m_OrderTime?.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0)); //Delivery date is made within 14 business days
                order.m_DeliveryrDate = order.m_ShipDate?.Add(new TimeSpan(0, rand.Next(1, 24), 0, 0));
            }

            if (i >= 16 && i < 20)//20% of orders have no delivery date and delivery date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-7, -1), 0, 0, 0));
                order.m_ShipDate = DateTime.MinValue;
                order.m_DeliveryrDate = DateTime.MinValue;
            }
            m_listOreders.Add(order);
        }

        for (int i = 0; i < 40; i++)//The loop initializes 40 ordersItems.
        {
            OrderItem orderItem = new OrderItem();
            orderItem.m_ID = Config.orderItemId;
            Product p = (Product)m_listPruducts[rand.Next(0, 9)];
            orderItem.m_OrderId = ((Order)m_listOreders[i/2]).m_ID;
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

        for (int i = 0; i < 5; i++)
        {
            Customer customer = new Customer();
            customer.m_ID = Config.CustomerId;
            customer.m_Name = firstName[rand.Next(0, 7)] + ' ' + lastName[rand.Next(0, 7)];
            customer.m_Password = rand.Next(1000, 9999);
            int n = 0;
            customer.m_orderItems = new List<OrderItem?>();
            for (int j = rand.Next(1, 4); j > 0; j--)
            {
                customer.m_orderItems.Add(m_listOrderItems[rand.Next(0, 20)]);
            }
            m_listCustomer.Add(customer);
        }
    }


    internal static class Config
    {
        private static int m_CustomerId = 100000;
        private static int m_orderId = 100000;
        private static int m_orderItemId = 100000;
        internal static int orderId { get { return m_orderId++; } }
        internal static int orderItemId { get { return m_orderItemId++; } }
        internal static int CustomerId { get { return m_CustomerId++; } }
    }

}
