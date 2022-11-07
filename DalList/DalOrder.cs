using DO;
using System.Collections;
using static Dal.DataSource;
namespace Dal;



public class DalOrder
{
    /// <summary>
    /// A function that adds a new object to the array of orders
    /// </summary>
    /// <param name="O"></param>The function accepts a new object to add
    /// <returns></returns>The function returns the ID of the new added order
    public int Add(Order O)
    {
        m_OrderArray[Config.m_indexEmptyOrder++] = O;
        m_OrderArray[Config.m_indexEmptyOrder - 1].m_ID = Config.orderId;
        return m_OrderArray[Config.m_indexEmptyOrder - 1].m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of orders
    /// </summary>
    /// <param name="ID"></param>ID  of the requested order
    public void Delete(int ID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrder; i++)
        {
            if (ID == m_OrderArray[i].m_ID)
            {
                if (i == Config.m_indexEmptyOrder - 1)
                    Config.m_indexEmptyOrder--;
                else
                    for (int j = i; j < Config.m_indexEmptyOrder; j++)
                        if (j + 1 != Config.m_indexEmptyOrder)
                            m_OrderArray[j] = m_OrderArray[j + 1];
                Config.m_indexEmptyOrder--;
                return;
            }
        }
        throw new Exception("The requested orderItem item does not exist");
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the order that needs to be updated
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    public void Update(Order O)
    {
        for (int i = 0; i != Config.m_indexEmptyOrder; i++)
            if (O.m_ID == m_OrderArray[i].m_ID)
            {
                m_OrderArray[i] = O;
                return;
            }
        throw new Exception("The requested order does not exist");
    }
    /// <summary>
    /// The function returns the order w hose ID was received
    /// </summary>
    /// <param name="ID"></param>The function receives an order ID
    /// <returns></returns>The function returns the requested order
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    public Order GetbyID(int ID)
    {
        for (int i = 0; i != Config.m_indexEmptyOrder; i++)
        {
            if (ID == m_OrderArray[i].m_ID)
                return m_OrderArray[i];
        }
        throw new Exception("The requested order does not exist");
    }
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetArray()
    {
        Order[] array = new Order[Config.m_indexEmptyOrder];
        for (int i = 0; i != Config.m_indexEmptyOrder; i++)
            array[i] = m_OrderArray[i];
        IEnumerable e = array.AsEnumerable();
        return e;
    }
}