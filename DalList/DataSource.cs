using Dal;
using DO;
using System.Collections;
using static Dal.DataSource;

namespace Dal;
internal static class DataSource
{
    static readonly Random rand = new Random();

    internal static List<Product> m_listPruducts = new List<Product>();
    internal static List<Order> m_listOreders = new List<Order>();
    internal static List<OrderItem> m_listOrderItems = new List<OrderItem>();


    static DataSource() => s_Initialize(); //difult constructor.

    static string[] nameProduct = { "String necklace", "Pendant necklace", "Wedding ring", "Wide ring", "Name necklace", "Hard bracelet", "Foot bracelet", "Tight earring", "Hoop earing", "Delicate bracelet" };

    static string[] firstName = { "David", "Sara", "Yael", "Lea", "Yair", "Yossi", "Meir", "Tali" };

    static string[] lastName = { "Cohen", "Ochana", "Mansur", "Levi", "Dahan", "Ben-Califa", "Zafrani", "Deryi" };

    static string[] citie = { "Haifa", "Jerusalem", "Rechassim", "Tveria", "Netivot", "Rechovot", "Gadera", "Gadera", "Tveria", "Jerosulem" };

    static string[] street = { "Ben Guryon", "Harimonim", "Hazait", "Vaitzman", "Begin", "Hatamar", "Savion", "Haoranim" };

    private static void s_Initialize()
    {
   
        for (int i = 0; i < 10; i++) //The loop initializes 10 products.
        {
            Product product = new Product();
            product.m_ID = rand.Next(100000, 999999);//Generate a random number with 6 digits.
            for (int j = 0; j < Config.m_indexEmptyProduct; j++) //Checking if the ID number exists.
            {
                if (m_listPruducts.Exists(match => match.m_ID == product.m_ID))
                { product.m_ID = rand.Next(100000, 999999); j = 0; } //If found, replace ID number.
            }
            product.m_Name = nameProduct[i]; //initialization product name
            product.m_Price = rand.Next(200, 1000); //Product price initialization.
            product.m_InStock = rand.Next(10, 30); //Initialize quantity in stock for product.

            if (product.m_Name.StartsWith("Necklace") || product.m_Name.EndsWith("necklace"))
                product.m_Category = Enums.Category.Necklaces;
            if (product.m_Name.StartsWith("Ring") || product.m_Name.EndsWith("ring"))
                product.m_Category = Enums.Category.Rings;
            if (product.m_Name.StartsWith("Bracelet") || product.m_Name.EndsWith("bracelet"))
                product.m_Category = Enums.Category.Bracelets;
            if (product.m_Name.StartsWith("Earring") || product.m_Name.EndsWith("earring"))
                product.m_Category = Enums.Category.Earrings;
            addProduct(product);
        }

        for (int i = 0; i < 20; i++)//The loop initializes 20 orders.
        {
            Order order = new Order();

            string custumerFirstName = firstName[rand.Next(0, 7)];
            string custumerLastName = lastName[rand.Next(0, 7)];

            order.m_CustomerName = custumerFirstName + " " + custumerLastName; //Initialize the Customer name.
            order.m_CustomerEmail = custumerFirstName + rand.Next(100, 999) + "@gmail.com"; //Initialize the customer email.
            order.m_CustomerAdress = street[rand.Next(0, 7)] + " " + rand.Next(1, 100) + " " + citie[rand.Next(0, 9)]; //Client address initialization.

            if (i >= 10 && i < 16)//80 from orders also have a delivery date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-16, -14), 0, 0, 0));
                order.m_ShipDate = order.m_OrderTime.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0));
                order.m_DeliveryrDate = DateTime.MinValue;
            }

            if (i >= 0 && i < 10)//To 60 percent of the 80 percent of the orders that also have a delivery date, have a delivery date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-30, -15), 0, 0, 0));
                order.m_ShipDate = order.m_OrderTime.Add(new TimeSpan(rand.Next(1, 14), 0, 0, 0));
                order.m_DeliveryrDate = order.m_ShipDate.Add(new TimeSpan(0, rand.Next(1, 24), 0, 0));
            }

            if (i >= 16 && i < 20)//20 percent of orders have no delivery date and delivery date.
            {
                order.m_OrderTime = DateTime.Now.Add(new TimeSpan(rand.Next(-13, -1), 0, 0, 0));
                order.m_ShipDate = DateTime.MinValue;
                order.m_DeliveryrDate = DateTime.MinValue;
            }
            addOrder(order);
        }

        for (int i = 0; i < 40; i++)//The loop initializes 40 ordersItems.
        {
            OrderItem orderItem = new OrderItem();
            Product p = m_listPruducts[rand.Next(0, 9)];
            orderItem.m_OrderId = m_listOreders[rand.Next(0, 19)].m_ID;
            for (int k = 0, j = 0; j < Config.m_indexEmptyOrderItem; j++) //Test that each order will be 1 to 4 order items
            {
                if (m_listOrderItems[j].m_OrderId == orderItem.m_OrderId)
                {
                    k++;
                    if (k > 4) { orderItem.m_OrderId = m_listOreders[rand.Next(0, 19)].m_ID; j = 0; }
                }
            }
            orderItem.m_ProductId = p.m_ID;
            for (int j = 0; j < m_listOrderItems.Count; j++)  //An examination that this item does not exist in the current order.
            {
                if (m_listOrderItems[j].m_ProductId == orderItem.m_ProductId && m_listOrderItems[j].m_OrderId == orderItem.m_OrderId)
                { //If the item exists.
                    p = m_listPruducts[rand.Next(0, 9)];
                    orderItem.m_ProductId = p.m_ID; j = 0;//The price is equal to double price.
                }
            }
            orderItem.m_amount = rand.Next(1, 4); //Random number of product amount.
            orderItem.m_Price = orderItem.m_amount * p.m_Price; //
            addOrderItem(orderItem);
        }
    }

    //The function adds a product to the product pool:
    private static void addProduct(Product product) { m_listPruducts.Add(product); Config.m_indexEmptyProduct++; }

    //The function adds an order to the order pool:
    private static void addOrder(Order order) { m_listOreders.Add(order); Config.m_indexEmptyOrder++; }

    //The function adds an OrderItem to the OrderItem pool:
    private static void addOrderItem(OrderItem orderItem) { m_listOrderItems.Add(orderItem); Config.m_indexEmptyOrderItem++; }

    internal static class Config
    {
        internal static int m_indexEmptyProduct = 0;
        internal static int m_indexEmptyOrder = 0;
        internal static int m_indexEmptyOrderItem = 0;
        private static int m_orderId = 100000;
        private static int m_orderItemId = 100000;
        internal static int orderId { get { return m_orderId++; } }
        internal static int orderItemId { get { return m_orderItemId++; } }
    }

}
