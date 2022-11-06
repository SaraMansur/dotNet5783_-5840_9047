using DO;

using DO;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using static Dal.DalProduct;
using static DO.Enums;

namespace Dal;

internal static class DataSource 
{
    public static readonly int m_num;
    internal static Product[] m_ProductArray = new Product[50];
    internal static Order[] m_OrderArray = new Order[100];
    internal static OrderItem[] m_OrderItemArray = new OrderItem[200];

    

    static string[] m_nameProduct = new string[]
    { "String necklace", "Pendant necklace", "Wedding ring", "Wide ring", "Name necklace", "Hard bracelet", "Foot bracelet", "Tight earring", "Hoop earing", "Delicate bracelet" };

    static int[] m_price = new int[]
    {250, 300, 800, 579, 230, 190, 99, 135, 340, 178};

    static int[] m_stock = new int[]
    {50, 67, 0, 78, 56, 23, 56, 90, 89, 100};

    static string[] m_nameOfCustomer = new string[]
    {"David","Sara","Yael","Lea","Yair","Yossi","Meir","Tali","Moshe","Miriam",
        "Yonit","Hadas","Efrat","Tamar","Noa","Tal","Tamir","Tomi","Rami","Ari"};

    static string[] m_mail = new string[]
    {"111@gmail.com","568@gmail.com","78@gmail.com","567@gmail.com","664@gmail.com","552@gmail.com","226@gmail.com","524@gmail.com","888@gmail.com","288@gmail.com",
    "241@gmail.com","784@gmail.com","334@gmail.com","342@gmail.com","822@gmail.com","726@gmail.com","522@gmail.com","999@gmail.com","100@gmail.com","995@gmail.com",};

    static string[] m_address = new string[]
    {"Haifa","Jerusalem","Rechassim","Tveria","Netivot","Rechovot","Gadera","Gadera","Tveria","Jerosulem",
    "Rosh Pina","Hadera","Nahariya","Jerusalem","Rechassim","Rechassim","Gadera","Netivot","Netivot","Haifa","Haifa"};

    //difult constructor:
    static DataSource() { s_Initialize(); }

    private static void s_Initialize()
    {
        for (int i = 1; i <= 10; i++) { addProduct(); } //The loop initializes 10 products.
        for (int i = 1; i <= 20; i++) { addOrder(i); }   //The loop initializes 20 orders.
        for (int i = 1; i <= 40; i++) { addOrderItem(); }//The loop initializes 40 ordersItems.
    }

    //Adds an entity of type Product to the array:
    private static void addProduct()
    {
        Random rand = new Random(DateTime.Now.Millisecond);
        Product product = new Product();
        product.m_ID = rand.Next(100000, 999999);//Generate a random number with 6 digits.
        for (int j = 0; j < m_ProductArray.Length; j++)
        {
            if (m_ProductArray[j].m_ID == product.m_ID)
            { product.m_ID = rand.Next(100000, 999999); j = 0; }
        }
        product.m_Name = m_nameProduct[Config.m_indexEmptyProduct];
        product.m_Price = m_price[Config.m_indexEmptyProduct];
        product.m_InStock = m_stock[Config.m_indexEmptyProduct];

        if (product.m_Name.StartsWith("Necklace")|| product.m_Name.EndsWith("necklace"))
            product.m_Category = Enums.Category.Necklaces;
        if (product.m_Name.StartsWith("Ring") || product.m_Name.EndsWith("ring"))
            product.m_Category = Enums.Category.Rings;
        if (product.m_Name.StartsWith("Bracelet") || product.m_Name.EndsWith("bracelet"))
            product.m_Category = Enums.Category.Bracelets;
        if (product.m_Name.StartsWith("Earring") || product.m_Name.EndsWith("earring"))
            product.m_Category = Enums.Category.Earrings;

        //Add(product);
    }
    
    private static void addOrder(int i)
    {
        Order order = new Order();
        order.m_ID = Config.orderId;
        order.m_CustomerName = m_nameOfCustomer[Config.m_indexEmptyOrder];
        order.m_CustomerEmail = m_mail[Config.m_indexEmptyOrder];
        order.m_CustomerAdress = m_mail[Config.m_indexEmptyOrder];

        Random rand = new Random(DateTime.Now.Millisecond);
        System.DateTime date = DateTime.Now;
        if (i >= 1 && i <= 10) 
        {
            order.m_OrderTime = date.AddDays(rand.Next(-30, -15));
            order.m_ShipDate = order.m_OrderTime.AddDays(rand.Next(1, 14));
            order.m_DeliveryrDate = order.m_ShipDate.AddDays(rand.Next(1, 24));
        }

        if (i > 10 && i <= 16) 
        {
            order.m_OrderTime = date.AddDays(rand.Next(-16, -14));
            order.m_ShipDate = order.m_OrderTime.AddDays(rand.Next(1, 14));
            order.m_DeliveryrDate = DateTime.MinValue;
        }

        if (i > 16 && i <= 20) 
        {
            order.m_OrderTime = date.AddDays(rand.Next(-13, -1));
            order.m_ShipDate = DateTime.MinValue;
            order.m_DeliveryrDate = DateTime.MinValue;
        }
    } 

    private static void addOrderItem() 
    {
        OrderItem orderItem = new OrderItem();
        Random rand = new Random(DateTime.Now.Millisecond);
        orderItem.m_Id = Config.orderItemId;
        int num = rand.Next(0, 20);
        orderItem.m_OrderId = m_OrderArray[num].m_ID;
        for (int k=0,j = 0; j < m_OrderItemArray.Length; j++)
        {
            if (m_OrderItemArray[j].m_OrderId == orderItem.m_OrderId)
            {
                k++;
                if (k > 4) 
                {
                    num = rand.Next(0, 20);
                    orderItem.m_OrderId = m_OrderArray[num].m_ID;
                    j = 0;
                }
            }
        }
        int index = rand.Next(0, 9);
        orderItem.m_ProductId = m_ProductArray[index].m_ID;
        for (int j = 0; j < m_OrderItemArray.Length; j++)
        {
            if (m_OrderItemArray[j].m_ProductId == orderItem.m_ProductId&& m_OrderItemArray[j].m_OrderId == orderItem.m_OrderId)
            {
                 index = rand.Next(0, 9);
                orderItem.m_ProductId = m_ProductArray[index].m_ID;
                j = 0;
            }
        }
        orderItem.m_amount = rand.Next(11, 3);
        orderItem.m_Price = orderItem.m_amount * m_ProductArray[index].m_Price;
    }

    

    internal static class Config
    {
        internal static int m_indexEmptyProduct = 0;
        internal static int m_indexEmptyOrder = 0;
        internal static int m_indexEmptyOrderItem = 0;
        private static int m_orderId = 100000;
        private static int m_orderItemId = 100000 ; 
        public static int orderId { get { return m_orderId++; } }
        public static int orderItemId { get { return m_orderItemId++; } }
    }
}
