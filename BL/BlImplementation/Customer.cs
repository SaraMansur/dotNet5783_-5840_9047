using BlApi;
using BO;
using DO;
using System;
using System.Runtime.Intrinsics.Arm;

namespace BlImplementation;

internal class Customer : ICustomer
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    private BO.Cart Build (List<DO.OrderItem?> DOorderItem , BO.Cart BOcart)
    {
        BOcart.m_orderItems = new List<BO.OrderItem?>();
        foreach (DO.OrderItem item in DOorderItem)
        { //The loop inserts data into a BOorder order details array.
            BO.OrderItem BOorderItem = new BO.OrderItem();
            BOorderItem.m_ID = item.m_ID;
            BOorderItem.m_IdProduct = item.m_ProductId;
            try
            {
                BOorderItem.m_NameProduct = Dal.Product.GetSingle((x => x?.m_ID == item.m_ProductId))?.m_Name;
                BOorderItem.m_PriceProduct = (double)Dal.Product.GetSingle((x => x?.m_ID == item.m_ProductId))?.m_Price;
            }
            catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
            BOorderItem.m_AmountInCart = item.m_amount;
            BOorderItem.m_TotalPriceItem = item.m_Price;
            BOcart.m_TotalPrice = BOcart.m_TotalPrice + item.m_Price;
            BOcart.m_orderItems.Add(BOorderItem);
        }
        return BOcart;
    }
    /// <summary>
    /// The function receives a username and password and returns the client.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public BO.Customer CustomerId(string name, int password)
    {
        BO.Customer BOcustomer = new BO.Customer();
        DO.Customer DOcustomer = new DO.Customer();
        try { DOcustomer =(DO.Customer)Dal.Customer.GetSingle(x => x?.m_Name == name && x?.m_Password == password); }
        catch(Exception ex) { throw new("The entered user is not a registered user in the system."); }
        BOcustomer.Name = name;
        BOcustomer.Password = password;
        BOcustomer.m_ID = DOcustomer.m_ID;  
        BO.Cart BOcart = new BO.Cart() {m_TotalPrice=0 };
        BOcart.m_CustomerName = name;
        BOcustomer.m_Cart =Build(DOcustomer.m_orderItems, BOcart);
        BOcustomer.m_Cart.m_CustomerAdress = DOcustomer.m_Adress;
        BOcustomer.m_Cart.m_CustomerMail = DOcustomer.m_Email;
        BOcustomer.m_Cart.m_CustomerName = DOcustomer.m_Name;
        return BOcustomer;
    }

    public void UpdateCustomer(BO.Customer customer, int num=0) 
    {
        DO.Customer DOcustomer = (DO.Customer) Dal.Customer.GetSingle(x=>x.Value.m_ID==customer.m_ID);
        if(num == 0)
        {
            DOcustomer.m_orderItems = new List<DO.OrderItem?>();
            foreach (BO.OrderItem item in customer.m_Cart.m_orderItems)
            {
                DO.OrderItem DOorderItem = new DO.OrderItem() { m_amount = item.m_AmountInCart, m_ID = item.m_ID, m_OrderId = customer.m_OrderId, m_Price = item.m_TotalPriceItem, m_ProductId = item.m_IdProduct };
                DOcustomer.m_orderItems.Add(DOorderItem);
            }
        }

        if (num != 0) 
        {
            DOcustomer.m_orders.Add(Dal.Order.GetSingle(x => x?.m_ID == customer.m_OrderId));
        }
        try { Dal.Customer.Update(DOcustomer); }
        catch (Exception ex) { throw new ("There is a problem with the update, please check the details and try again."); }
    }

    public List<DO.Order?> AllOrder(BO.Customer customer)
    {
        List<DO.Order?> order = new List<DO.Order?>();
        DO.Customer DOcustomer=(DO.Customer)Dal.Customer.GetSingle(x=>x.Value.m_ID== customer.m_ID);
        order = DOcustomer.m_orders;
        return order;   
    }

    public BO.Customer AddCustomer(string name, string address, string mail, int password)
    {
        BO.Customer BOcustomer = new BO.Customer();
        DO.Customer DOcustomer = new DO.Customer();
        if (!mail!.EndsWith("@gmail.com") || name.Length == 0 || mail.Length == 0 || address.Length == 0)
            throw new BO.IlegalInput(); //Throwing an exception in case one or more of the details is wrong.
        for (int i = 0; i < name.Length; i++)
        {
            if (!(name[i] <= 'z' && name[i] >= 'a') && (name[i] != ' ') && !(name[i] <= 'Z' && name[i] >= 'A'))
                throw new BO.IlegalInput();
        }
        IEnumerable<DO.Customer?> list = Dal.Customer.Get();
        foreach(var item in list)
        {
            if (item?.m_Name == name) throw new("This name is already registered in the system.");
            if (item?.m_Email == mail) throw new("This mail is already registered in the system.");
        }
        DOcustomer.m_Name = name;
        DOcustomer.m_Adress = address;
        DOcustomer.m_Email = mail;
        DOcustomer.m_Password = password;
        DOcustomer.m_orderItems = new List<DO.OrderItem?>();
        DOcustomer.m_orders = new List<DO.Order?>();
        BOcustomer.m_ID = Dal.Customer.Add(DOcustomer);
        BOcustomer.m_Cart = new BO.Cart() { m_CustomerAdress = address, m_CustomerName = name, m_CustomerMail = mail, m_orderItems = new List<BO.OrderItem>(), m_TotalPrice = 0 };
        BOcustomer.Name = name;
        BOcustomer.Password = password;

        return BOcustomer;  
    }
}
