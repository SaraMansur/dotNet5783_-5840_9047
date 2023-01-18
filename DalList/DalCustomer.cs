using DO;
using static Dal.DataSource;
using DalApi;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Dal;


internal class DalCustomer : ICustomer
{
    public int Add(Customer? customer)
    {
        Customer C = customer ?? throw new ArgumentNull();
        C.m_ID = Config.CustomerId;
        customer = C;
        m_listCustomer.Add(customer);
        return C.m_ID;
    }

    public void Delete(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        m_listCustomer.Remove(GetSingle(x => x?.m_ID == ID));
        return;
    }

    public IEnumerable<Customer?> Get(Func<Customer?, bool>? func = null)
    {
        if (func == null)
            return m_listCustomer;
        return m_listCustomer.Where(func);
    }

    public Customer? GetSingle(Func<Customer?, bool>? func)
    {
        Customer? customer = m_listCustomer.FirstOrDefault(func) ?? throw new NotExist();
        return customer;
    }

    public void Update(Customer? customer)
    {
        customer = customer ?? throw new ArgumentNull();
        for (int i = 0; i != m_listCustomer.Count; i++)
            if (customer?.m_ID == m_listCustomer[i]?.m_ID)
            {
                m_listCustomer[i] = customer;
                return;
            }
        throw new NotExist();
    }
}
