using DalApi;
using DO;
using static Dal.DataSource;
namespace Dal;
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// A function that adds a new object to the array of orderItem
    /// </summary>
    /// <param name="O"></param>The function accepts a new object to add
    /// <returns></returns>The function returns the ID of the new added orderItem
    public int Add(OrderItem? OI)
    {
        OrderItem oi = OI ?? throw new ArgumentNull();
        oi.m_ID = Config.orderItemId;
        OI = oi;
        m_listOrderItems.Add(OI);
        return oi.m_ID;
    }

    /// <summary>
    /// A function that deletes an object from the array of orderItem
    /// </summary>
    /// <param name="ID"></param>ID  of the requested orderItem
    public void Delete(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        m_listOrderItems.Remove(GetSingle(x => x?.m_ID == ID));
        return;
    }

    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the orderItem that needs to be updated
    /// <exception cref="Exception"></exception>If the orderItem does not exist in the array an exception is thrown
    public void Update(OrderItem? OI)
    {
        OI = OI ?? throw new ArgumentNull();
        for (int i = 0; i != m_listOrderItems.Count; i++)
            if (OI?.m_ID == m_listOrderItems[i]?.m_ID)
            {
                m_listOrderItems[i] = OI;
                return;
            }
        throw new NotExist();
    }

    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> Get(Func<OrderItem?, bool>? func)
    {
        if (func == null)
            return m_listOrderItems;
        return m_listOrderItems.Where(func);
    }

    public OrderItem? GetSingle(Func<OrderItem?, bool>? func)
    {
        OrderItem? od = m_listOrderItems.FirstOrDefault(func) ?? throw new NotExist();
        return od;
    }
}