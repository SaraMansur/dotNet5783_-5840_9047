using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using static Dal.DataSource;

internal class DalOrder : IOrder
{
    //dir need to be up from bin
    //static string dir = @"C:\Users\שרה\source\repos\dotNet5783_-5840_9047\xml\Order.xml";
    static DalOrder()
    {
        if (!Directory.Exists(OrderFilePath))
            Directory.CreateDirectory(OrderFilePath);
    }

    static string OrderFilePath = @"C:\Users\שרה\source\repos\dotNet5783_-5840_9047\xml\Order.xml";

    public DalOrder()
    {
        if (!File.Exists( OrderFilePath))
            XMLTools.SaveListToXMLSerializer<DO.Order?>(DataSource.m_listOreders, OrderFilePath);
    }

    public int Add(DO.Order? entity)
    {
        List<DO.Order?> OrderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFilePath);
        Order order = entity ?? throw new ArgumentNull();
        order.m_ID = Config.getOrderId();
        OrderList.Add(order);

        XMLTools.SaveListToXMLSerializer<DO.Order?>(OrderList, OrderFilePath);
        return order.m_ID;
    }

    public void Delete(int? ID)
    {
        List<DO.Order?> OrderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFilePath);
        if (!OrderList.Exists(t => t?.m_ID == ID))
        {
            throw new Exception("DL: Student with the same id not found...");
            //throw new SomeException("DL: Student with the same id not found...");
        }

        OrderList.Remove(GetSingle(x => x?.m_ID == ID));

        XMLTools.SaveListToXMLSerializer<DO.Order?>(OrderList, OrderFilePath);
    }

    public IEnumerable<DO.Order?> Get(Func<DO.Order?, bool>? func = null)
    {
        List<DO.Order?> OrderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFilePath);
        if (func == null)
            return OrderList;
        return OrderList.Where(func);
    }

    public DO.Order? GetSingle(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?> OrderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFilePath);
        Order? o = OrderList.FirstOrDefault(func) ?? throw new NotExist();
        return o;
    }

    public void Update(DO.Order? entity)
    {
        List<DO.Order?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderFilePath);

        entity = entity ?? throw new ArgumentNull();
        for (int i = 0; i != m_listOrderItems.Count; i++)
        {
            if (entity?.m_ID == m_listOrderItems[i]?.m_ID)
            {
                OrderItemList[i] = entity;

                XMLTools.SaveListToXMLSerializer<DO.Order?>(OrderItemList, OrderFilePath);
                return;
            }
        }
        throw new NotExist();
    }
}

