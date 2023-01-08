using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using static Dal.DataSource;

internal class DalOrderItem : IOrderItem
{
    //dir need to be up from bin
    static string dir = @"..\..\..\..\xmlData\";
    static DalOrderItem()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    string OrderItemFilePath = @"TestList.xml";

    public DalOrderItem()
    {
        if (!File.Exists(dir + OrderItemFilePath))
            XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(DataSource.m_listOrderItems, dir + OrderItemFilePath);
    }


    public int Add(OrderItem? OI)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);
        OrderItem oi = OI ?? throw new ArgumentNull();
        oi.m_ID = Config.getOrderId();
        OrderItemList.Add(oi);

        XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(OrderItemList, dir + OrderItemFilePath);
        return oi.m_ID;
    }

    public void Delete(int? ID)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);
        if (!OrderItemList.Exists(t => t?.m_ID == ID))
        {
            throw new Exception("DL: Student with the same id not found...");
            //throw new SomeException("DL: Student with the same id not found...");
        }

        OrderItemList.Remove(GetSingle(x => x?.m_ID == ID));

        XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(OrderItemList, dir + OrderItemFilePath);
    }

    public void Update(OrderItem? OI)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);

        OI = OI ?? throw new ArgumentNull();
        for (int i = 0; i != m_listOrderItems.Count; i++)
        {
            if (OI?.m_ID == m_listOrderItems[i]?.m_ID)
            {
                OrderItemList[i] = OI;

                XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(OrderItemList, dir + OrderItemFilePath);
                return;
            }
        }
        throw new NotExist();
    }

    public IEnumerable<OrderItem?> Get(Func<OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);
        if (func == null)
            return OrderItemList;
        return OrderItemList.Where(func);
    }

    public OrderItem? GetSingle(Func<OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);
        OrderItem? od = OrderItemList.FirstOrDefault(func) ?? throw new NotExist();
        return od;
    }
}



