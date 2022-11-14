using DO;
using System.Collections;
using static Dal.DataSource;

namespace Dal;
public class DalProduct
{

    /// <summary>
    /// A function that adds a new object to the array of ptoducts
    /// </summary>
    /// <param name="P"></param>The function receives a new product 
    /// <returns></returns>The function returns the ID of the new added product
    /// <exception cref="Exception"></exception> if The requested product already exist an exception is thrown
    public int Add(Product P)
    {

        for (int i = 0; i < Config.m_indexEmptyProduct; i++)
        {
            if (P.m_ID == m_listPruducts[i].m_ID)
                throw new Exception("The requested product already exist");
        }
        m_listPruducts.Add(P);
        Config.m_indexEmptyProduct++;
        return P.m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of product
    /// </summary>
    /// <param name="ID"></param>ID  of the requested product
    public void Delete(int? ID)
    {
        for (int i = 0; i != Config.m_indexEmptyProduct; i++)
        {
            if (ID == m_listPruducts[i].m_ID)
            {
                m_listPruducts.Remove(m_listPruducts[i]);
                Config.m_indexEmptyProduct--;
                return;
            }
        }
        throw new Exception("The requested orderItem item does not exist");
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the producte that needs to be updated
    /// <exception cref="Exception"></exception>If the product does not exist in the array an exception is thrown
    public void Update(Product P)
    {
        for (int i = 0; i != Config.m_indexEmptyProduct; i++)
            if (P.m_ID == m_listPruducts[i].m_ID)
            {
                m_listPruducts[i] = P;
                return;
            }

        throw new Exception("The requested product does not exist");
    }
    /// <summary>
    /// The function returns the product whose ID was received
    /// </summary>
    /// <param name="ID"></param>The function receives an order ID
    /// <returns></returns>The function returns the requested order
    /// <exception cref="Exception"></exception>If the order does not exist in the array an exception is thrown
    public Product GetbyID(int? ID)
    {
        for (int i = 0; i != Config.m_indexEmptyProduct; i++)
        {
            if (m_listPruducts[i].m_ID == ID)
                return m_listPruducts[i];
        }
        throw new Exception("The requested product does not exist");
    }
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable <Product> GetArray()
    {
        return DataSource.m_listPruducts;
    } 
   
}