using DO;
using static Dal.DataSource;
using DalApi;
using System.Collections.Generic;

namespace Dal;


internal class DalOrder:IOrder
{
    /// <summary>
    /// A function that adds a new object to the array of orders
    /// </summary>
    /// <param name="O"></param>The function accepts a new object to add
    /// <returns></returns>The function returns the ID of the new added order
    public int Add(Order O)
    {
        O.m_ID = Config.orderId;
        m_listOreders.Add(O);
        return O.m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of orders
    /// </summary>
    /// <param name="ID"></param>ID  of the requested order
    public void Delete(int ID)
    {
        for (int i = 0; i != m_listOreders.Count; i++)
        {
            if (ID == ((Order)m_listOreders[i]).m_ID)
            {
                m_listOreders.Remove(m_listOreders[i]);
                return;
            }
        }
        throw new NotExist();
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the order that needs to be updated
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    public void Update(Order O)
    {
        for (int i = 0; i != m_listOreders.Count; i++)
            if (O.m_ID == ((Order)m_listOreders[i]).m_ID)
            {
                m_listOreders[i] = O;
                return;
            }
        throw new NotExist();
    }
    /// <summary>
    /// The function returns the order w hose ID was received
    /// </summary>
    /// <param name="ID"></param>The function receives an order ID
    /// <returns></returns>The function returns the requested order
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    //public Order GetbyID(int ID)
    //{
    //    for (int i = 0; i != m_listOreders.Count; i++)
    //    {
    //        if (ID == m_listOreders[i].Value.m_ID)
    //            return m_listOreders[i].Value;
    //    }
    //    throw new NotExist();
    //}
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> Get( Func<Order?, bool>? func = null)
    {
        if(func==null)
           return m_listOreders;
        List<Order?> list = new List<Order?>();
        foreach (var item in m_listOreders)
        {
            if(func(item))
                list.Add(item); 
        }
        return list;
    }

    public Order? GetSingle(Func<Order?, bool>? func)
    {
        if (m_listOreders.FirstOrDefault(func) != null)
            return m_listOreders.FirstOrDefault(func);
        throw new NotExist();
    }

}