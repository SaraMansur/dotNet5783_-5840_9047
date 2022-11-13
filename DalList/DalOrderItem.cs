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
        
        OI.m_ID= Config.orderItemId;
        m_listOrderItems.Add(OI);
        return m_listOrderItems[Config.m_indexEmptyOrderItem - 1].m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of orderItem
    /// </summary>
    /// <param name="ID"></param>ID  of the requested orderItem
    public void Delete(int ID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
        {
            if (ID == m_listOrderItems[i].m_ID)
            {
                m_listOrderItems.Remove(m_listOrderItems[i]);
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
            if (OI.m_ID == m_listOrderItems[i].m_ID)
            {
                m_listOrderItems[i] = OI;
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
            if (ID == m_listOrderItems[i].m_ID)
                return m_listOrderItems[i];
        throw new Exception("The requested order item does not exist");
    }
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetArray()
    {
        return m_listOrderItems;
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
            if (OID == m_listOrderItems[i].m_OrderId && PID == m_listOrderItems[i].m_ProductId)
                return m_listOrderItems[i];
        throw new Exception("The requested orderItem item does not exist");
    }

    /// <summary>
    /// The function accepts an order ID and returns a list of all the products included in the order
    /// </summary>
    /// <param name="orderId"></param> the function recives id of order
    /// <returns></returns returns a list of all the products included in the order 
    public IEnumerable<OrderItem> GetOrderItems(int? orderId)
    {
        List<OrderItem> order = new List<OrderItem>();
        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
        {
            if (m_listOrderItems[i].m_OrderId == orderId)
                order.Add(m_listOrderItems[i]);
        }
        return order;
    }
}