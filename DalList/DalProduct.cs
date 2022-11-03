
using DO;

namespace Dal;
public class DalProduct
{
    public int? Add(Product P)
    {

        return P.m_ID;
    }
    public void Delete(Product P) { }
    public void Update(Product P, double price) { }
    public int? GetbyID(Product P) { return P.m_ID; }

}
