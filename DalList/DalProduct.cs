//
using DO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using static Dal.DataSource;

namespace Dal;
public class DalProduct
{
    public int? Add(Product P)
    {

        for (int i = 0; i != Config.m_indexEmptyProduct; i++)
        {
            if (P.m_ID == DataSource.m_ProductArray[i].m_ID)
                return null;
        }
        DataSource.m_ProductArray[Config.m_indexEmptyProduct++] = P;
        return P.m_ID;
    }
    public void Delete(Product P) { }
    public void Update(Product P, double price) { }
    public Product GetbyID(int? ID)
    {
        for (int i = 0; i != Config.m_indexEmptyProduct; i++)
        {
            if (DataSource.m_ProductArray[i].m_ID == ID)
                return DataSource.m_ProductArray[i];
        }
        return new Product();
    }

}