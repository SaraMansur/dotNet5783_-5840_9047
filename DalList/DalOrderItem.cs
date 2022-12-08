using DalApi;
using DO;
using static Dal.DataSource;
namespace Dal;
internal class DalOrderItem:IOrderItem
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
        return OI.m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of orderItem
    /// </summary>
    /// <param name="ID"></param>ID  of the requested orderItem
    public void Delete(int ID)
    {
        for (int i = 0; i < m_listOrderItems.Count; i++)
        {
            if (ID == ((OrderItem)m_listOrderItems[i]).m_ID)
            {
                m_listOrderItems.Remove(m_listOrderItems[i]);
                return;
            }
        }
        throw new NotExist();
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the orderItem that needs to be updated
    /// <exception cref="Exception"></exception>If the orderItem does not exist in the array an exception is thrown
    public void Update(OrderItem OI)
    {
        for (int i = 0; i != m_listOrderItems.Count; i++)
            if (OI.m_ID == ((OrderItem)m_listOrderItems[i]).m_ID)
            {
                m_listOrderItems[i] = OI;
                return;
            }
        throw new NotExist();
    }
    /// <summary>
    /// The function returns the orderItem whose ID was received
    /// </summary>
    /// <param name="ID"></param>The function receives an orderItem ID
    /// <returns></returns>The function returns the requested orderItem
    /// <exception cref="Exception"></exception>If the orderItem does not exist in the array an exception is thrown
    //public OrderItem GetbyID(int ID)
    //{
    //    for (int i = 0; i != m_listOrderItems.Count; i++)
    //        if (ID == m_listOrderItems[i].Value.m_ID)
    //            return m_listOrderItems[i].Value;
    //    throw new NotExist();
    //}
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable <OrderItem?> Get(Func<OrderItem?, bool>? func)
    {
        if (func == null)
            return m_listOrderItems;
        List<OrderItem?> list = new List<OrderItem?>();
        foreach (var item in m_listOrderItems)
        {
            if (func(item))
                list.Add(item);
        }
        return list;

    }
    /// <summary>
    /// The function recives  ID of  product and order  and  returns the appropriate  orderItem 
    /// </summary>
    /// <param name="PID"></param> ID of  product
    /// <param name="OID"></param>ID of order
    /// <returns></returns> returns the appropriate  orderItem
    /// <exception cref="Exception"></exception> if The requested orderItem item does not exist
    //public OrderItem GetbyProductAndOrder(int? PID, int? OID)
    //{
    //    for (int i = 0; i != m_listOrderItems.Count; i++)
    //        if (OID == m_listOrderItems[i].Value.m_OrderId && PID == m_listOrderItems[i].Value.m_ProductId)
    //            return m_listOrderItems[i].Value;
    //    throw new NotExist();
    //}

    /// <summary>
    /// The function accepts an order ID and returns a list of all the products included in the order
    /// </summary>
    /// <param name="orderId"></param> the function recives id of order
    /// <returns></returns returns a list of all the products included in the order 
    //public IEnumerable<OrderItem?> GetOrderItems(int? orderId)
    //{
    //    List<OrderItem?> order = new List<OrderItem?>();
    //    for (int i = 0; i != m_listOrderItems.Count; i++)
    //    {
    //        if (m_listOrderItems[i].Value.m_OrderId == orderId)
    //            order.Add(m_listOrderItems[i].Value);
    //    }
    //    return order;
    //}

    public OrderItem? GetSingle(Func<OrderItem?, bool>? func) 
    { 
        if(m_listOrderItems.FirstOrDefault(func)!=null)
            return m_listOrderItems.FirstOrDefault(func);   
        throw new NotExist();
            
    }

    
}