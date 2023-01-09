using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;


internal class DalOrderItem : IOrderItem
{
    //dir need to be up from bin
    static string dir = @"..\xml\";
    static DalOrderItem()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    string OrderItemFilePath = @"OrderItems.xml";

    //public DalOrderItem()
    //{
    //    if (!File.Exists(dir + OrderItemFilePath))
    //        XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(DataSource.m_listOrderItems, dir + OrderItemFilePath);
    //}


    public int Add(OrderItem? OI)
    {
        List<DO.OrderItem?> OrderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dir + OrderItemFilePath);
        OrderItem oi = OI ?? throw new ArgumentNull();
        oi.m_ID = XMLTools.configOrderItemId();
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
        for (int i = 0; i != OrderItemList.Count; i++)
        {
            if (OI?.m_ID == OrderItemList[i]?.m_ID)
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