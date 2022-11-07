//
using DO;
using System.Collections;
using static Dal.DataSource;
namespace Dal;
public class DalOrderItem
{
    /// <summary>
    /// A function that adds a new object to the array of orderItem
    /// </summary>
    /// <param name="O"></param>The function accepts a new object to add
    /// <returns></returns>The function returns the ID of the new added orderItem
    public int Add(OrderItem OI)
    {
        m_OrderItemArray[Config.m_indexEmptyOrderItem++] = OI;
        m_OrderItemArray[Config.m_indexEmptyOrderItem - 1].m_ID = Config.orderItemId;
        return m_OrderItemArray[Config.m_indexEmptyOrderItem - 1].m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of orderItem
    /// </summary>
    /// <param name="ID"></param>ID  of the requested orderItem
    public void Delete(int ID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
        {
            if (ID == m_OrderItemArray[i].m_ID)
            {
                if (i == Config.m_indexEmptyOrderItem - 1)
                    Config.m_indexEmptyOrderItem--;
                else
                    for (int j = i; j < Config.m_indexEmptyOrderItem; j++)
                        if (j + 1 != Config.m_indexEmptyOrderItem)
                            m_OrderItemArray[j] = m_OrderItemArray[j + 1];
                Config.m_indexEmptyOrderItem--;
                return;
            }
        }
        throw new Exception("The requested orderItem item does not exist");
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the orderItem that needs to be updated
    /// <exception cref="Exception"></exception>If the orderItem does not exist in the array an exception is thrown
    public void Update(OrderItem OI)
    {
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
            if (OI.m_ID == m_OrderItemArray[i].m_ID)
            { 
                m_OrderItemArray[i] = OI; 
                return; 
            }
        throw new Exception("The requested orderItem does not exist");
    }
    /// <summary>
    /// The function returns the orderItem whose ID was received
    /// </summary>
    /// <param name="ID"></param>The function receives an orderItem ID
    /// <returns></returns>The function returns the requested orderItem
    /// <exception cref="Exception"></exception>If the orderItem does not exist in the array an exception is thrown
    public OrderItem GetbyID(int ID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
            if (ID == m_OrderItemArray[i].m_ID)
                return m_OrderItemArray[i];
        throw new Exception("The requested order item does not exist");
    }
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetArray()
    {
        OrderItem[] array = new OrderItem[Config.m_indexEmptyOrderItem];
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
            array[i] = m_OrderItemArray[i];
        IEnumerable e = array.AsEnumerable();
        return e;
    }
    /// <summary>
    /// The function recives  ID of  product and order  and  returns the appropriate  orderItem 
    /// </summary>
    /// <param name="PID"></param> ID of  product
    /// <param name="OID"></param>ID of order
    /// <returns></returns> returns the appropriate  orderItem
    /// <exception cref="Exception"></exception> if The requested orderItem item does not exist
    public OrderItem GetbyProductAndOrder(int? PID, int? OID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
            if (OID == m_OrderItemArray[i].m_OrderId && PID == m_OrderItemArray[i].m_ProductId)
                return m_OrderItemArray[i];
        throw new Exception("The requested orderItem item does not exist");
    }

    /// <summary>
    /// The function accepts an order ID and returns a list of all the products included in the order
    /// </summary>
    /// <param name="orderId"></param> the function recives id of order
    /// <returns></returns returns a list of all the products included in the order 
    public IEnumerable GetOrderItems(int? orderId)
    {
        List<OrderItem> order=new List<OrderItem>();
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
        {
            if (m_OrderItemArray[i].m_OrderId== orderId)
                order.Add(m_OrderItemArray[i]);
        }
        IEnumerable e=order.AsEnumerable();
        return e;
    }
}
