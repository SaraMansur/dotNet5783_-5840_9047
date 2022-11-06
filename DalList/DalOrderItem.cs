using DO;
using static Dal.DataSource;

namespace Dal;
public class DalOrderItem
{
    public int? Add(OrderItem OI)
    {

        for (int i = 0; i != Config.m_indexEmptyOrderItem; i++)
        {
            if (OI.m_OrderId == DataSource.m_OrderItemArray[i].m_OrderId)
                return null;
        }
        DataSource.m_OrderItemArray[Config.m_indexEmptyOrderItem++] = OI;
        return OI.m_OrderId;
    }

    public void Delete(Product P) { }
    public void Update(Product P, double price) { }
    public int? GetbyID(Product P) { return P.m_ID; }
}