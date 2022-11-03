
using DO;
using static Dal.DataSource;

namespace Dal;


    public  class DalOrder
{
    public int? Add(Order O)
    {

        for (int i = 0; i != Config.m_indexEmptyOrder; i++)
        {
            if (O.m_ID == DataSource.m_OrderArray[i].m_ID)
                return null;
        }
        DataSource.m_OrderArray[Config.m_indexEmptyOrder++] = O;
        return O.m_ID;
    }
    public void Delete(Product P) { }
    public void Update(Product P,double price) { }
    public int? GetbyID(Product P) { return P.m_ID; }

    ///public IENumerable getArray() { return IEnumerator }
}
