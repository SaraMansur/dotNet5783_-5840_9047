using DO;
using static Dal.DataSource;
using DalApi;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Dal;


internal class DalOrder : IOrder
{
    /// <summary>
    /// A function that adds a new object to the array of orders
    /// </summary>
    /// <param name="O"></param>The function accepts a new object to add
    /// <returns></returns>The function returns the ID of the new added order
    public int Add(Order? O)
    {
        Order help = O ?? throw new ArgumentNull();
        help.m_ID = Config.orderId;
        O = help;
        m_listOreders.Add(O);
        return help.m_ID;
    }

    /// <summary>
    /// A function that deletes an object from the array of orders
    /// </summary>
    /// <param name="ID"></param>ID  of the requested order
    public void Delete(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        m_listOreders.Remove(GetSingle(x => x?.m_ID == ID));
        return;
    }

    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the order that needs to be updated
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    public void Update(Order? O)
    {
        O = O ?? throw new ArgumentNull();
        for (int i = 0; i != m_listOreders.Count; i++)
            if (O?.m_ID == m_listOreders[i]?.m_ID)
            {
                m_listOreders[i] = O;
                return;
            }
        throw new NotExist();
    }

    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> Get(Func<Order?, bool>? func = null)
    {
        if (func == null)
            return m_listOreders;
        List<Order?> list = new List<Order?>();
        return m_listOreders.Where(func);
    }

    public Order? GetSingle(Func<Order?, bool>? func)
    {
        Order? O = m_listOreders.FirstOrDefault(func) ?? throw new NotExist();
        return O;
    }

}