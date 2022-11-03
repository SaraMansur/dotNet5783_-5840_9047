
using DO;

namespace Dal;

internal static class DataSource
{
    public static readonly int m_num;
    internal static Product[] m_ProductArray = new Product[50];
    internal static Order[] m_OrderArray = new Order[100];
    internal static OrderItem[] m_OrderItemArray = new OrderItem[200];

    static string[] m_nameArray = new string[]
    { "String necklace", "Pendant necklace", "Wedding ring", "Wide ring", "Name necklace", "Hard bracelet", "Foot bracelet", "Tight earring", "Hoop earing", "Delicate bracelet" };

    static int[] m_priceArray = new int[]
    {250, 300, 800, 579, 230, 190, 99, 135, 340, 178};

    static int[] m_stock = new int[]
    {50, 67, 0, 78, 56, 23, 56, 90, 89, 100};

    //difult constructor:
    static DataSource() { s_Initialize(); }

    private static void s_Initialize()
    {
        for (int i = 1; i < 10; i++) { addProduct(); } //The loop initializes 10 products.
        for (int i = 1; i < 20; i++) { addOrder(); }   //The loop initializes 20 orders.
        for (int i = 1; i < 40; i++) { addOrderItem(); }//The loop initializes 40 ordersItems.
    }

    //Adds an entity of type Product to the array:
    private static void addProduct()
    {
        Random rand = new Random(DateTime.Now.Millisecond);
        m_ProductArray[Config.m_indexEmptyProduct].m_makat = rand.Next(100000, 999999);//Generate a random number with 6 digits.
        for (int j = 0; j < Config.m_indexEmptyProduct; j++)
        {
            if (m_ProductArray[j].m_makat == m_ProductArray[Config.m_indexEmptyProduct].m_makat)
            { m_ProductArray[Config.m_indexEmptyProduct].m_makat = rand.Next(100000, 999999); j = 0; }
        }
        m_ProductArray[Config.m_indexEmptyProduct].name = m_nameArray[Config.m_indexEmptyProduct];
        m_ProductArray[Config.m_indexEmptyProduct].price = m_ProductArray[Config.m_indexEmptyProduct];
        m_ProductArray[Config.m_indexEmptyProduct].stock = m_stock[Config.m_indexEmptyProduct];
        Config.m_indexEmptyProduct++;
    }

    private static void addOrder()
    {
        m_OrderArray[]
    }

    private static void addOrderItem() 
    { 
        
    }


    internal static class Config
    {
        internal static int m_indexEmptyProduct = 0 ;
        internal static int m_indexEmptyOrder = 0 ;   
        internal static int m_indexEmptyOrderItem = 0 ;
        private static int m_orderId = 100000;
        private static int m_orderItemId = 100000 ; 
        public static int orderId { get { return m_orderId; } }
        public static int orderItemId { get { return m_orderItemId; } }
    }
}
