using DO;
using static Dal.DataSource;
using DalApi;
using System;

namespace Dal;
internal class DalProduct:IProduct
{

    /// <summary>
    /// A function that adds a new object to the array of ptoducts
    /// </summary>
    /// <param name="P"></param>The function receives a new product 
    /// <returns></returns>The function returns the ID of the new added product
    /// <exception cref="Exception"></exception> if The requested product already exist an exception is thrown
    public int Add(Product? P)
    {
        Product help = P?? throw new ArgumentNull();
        var p = m_listPruducts.FirstOrDefault(x => x?.m_ID == P?.m_ID);
        if (p != null)
            throw new AlreadyExist();
        m_listPruducts.Add(P);
        return (int)P?.m_ID;
    }
    /// <summary>
    /// A function that deletes an object from the array of product
    /// </summary>
    /// <param name="ID"></param>ID  of the requested product
    public void Delete(int? ID)
    {
        ID = ID?? throw new ArgumentNull();
        m_listPruducts.Remove(GetSingle(x => x?.m_ID == ID));
        return;
    }
    /// <summary>
    /// The function updates details of an item that exists in the array.
    /// </summary>
    /// <param name="O"></param>The function receives the producte that needs to be updated
    /// <exception cref="Exception"></exception>If the product does not exist in the array an exception is thrown
    public void Update(Product? P)
    {
        P = P ?? throw new ArgumentNull();
        for (int i = 0; i != m_listPruducts.Count; i++)
            if (P?.m_ID == m_listPruducts[i]?.m_ID)
            {
                m_listPruducts[i] = P;
                return;
            }

        throw new NotExist();
    }
 
    /// <summary>
    /// The function returns an array of the objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable <Product?> Get(Func<Product?, bool>? func)
    {
        if (func == null)
            return m_listPruducts;
        return m_listPruducts.Where(func);
    }

    public Product? GetSingle(Func<Product?, bool>? func)
    {
        Product? p = m_listPruducts.FirstOrDefault(func) ?? throw new NotExist();
        return p;
    }

}