using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;

internal class DalCustomer : ICustomer
{
    //dir need to be up from bin
    static string dir = @"..\xml\";
    static DalCustomer()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    string CustomerFilePath = @"Customer.xml";

    public int Add(Customer? customer)
    {
        List<DO.Customer?> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer?>(dir + CustomerFilePath);
        Customer C = customer ?? throw new ArgumentNull();
        C.m_ID = XMLTools.configCustomer();
        customerList.Add(C);
        XMLTools.SaveListToXMLSerializer<DO.Customer?>(customerList, dir + CustomerFilePath);
        return C.m_ID;
    }

    public void Delete(int? ID)
    {
        List<DO.Customer?> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer?>(dir + CustomerFilePath);
        if (!customerList.Exists(t => t?.m_ID == ID))
        {
            throw new Exception("DL: Student with the same id not found...");
        }

        customerList.Remove(GetSingle(x => x?.m_ID == ID));

        XMLTools.SaveListToXMLSerializer<DO.Customer?>(customerList, dir + CustomerFilePath);
    }

    public IEnumerable<Customer?> Get(Func<Customer?, bool>? func = null)
    { 
        List<DO.Customer?> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer?>(dir + CustomerFilePath);
        if (func == null)
            return customerList;
        return customerList.Where(func);
    }

    public Customer? GetSingle(Func<Customer?, bool>? func)
    {
        List<DO.Customer?> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer?>(dir + CustomerFilePath);
        DO.Customer? customer = customerList.FirstOrDefault(func) ?? throw new NotExist();
        return customer;
    }

    public void Update(Customer? customer)
    {
        List<DO.Customer?> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer?>(dir + CustomerFilePath);

        customer = customer ?? throw new ArgumentNull();
        for (int i = 0; i != customerList.Count; i++)
        {
            if (customer?.m_ID == customerList[i]?.m_ID)
            {
                customerList[i] = customer;

                XMLTools.SaveListToXMLSerializer<DO.Customer?>(customerList, dir + CustomerFilePath);
                return;
            }
        }
        throw new NotExist();
    }
}
