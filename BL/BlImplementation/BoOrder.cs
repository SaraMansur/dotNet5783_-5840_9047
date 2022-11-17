using BlApi;
using DalApi;

namespace BlImplementation;

internal class BoOrder : IBoOrder
{
    private IDal Dal = new Dal.DalList();

    //The function returns the status of the order:
    private BO.Enums.Status status(DO.Order order)
    {
        if (order.m_DeliveryrDate >= DateTime.Now) //If the order has been delivered
            return BO.Enums.Status.Received;
        else //If the order has not yet been delivered
        {
            if (order.m_ShipDate >= DateTime.Now)//If the order has been shipped
                return BO.Enums.Status.Shipped;
            else//If the order has not yet been sent
            {
                if (order.m_OrderTime >= DateTime.Now) //If the order is made
                    return BO.Enums.Status.Ordered;
                else //If the order is still in progress
                    return BO.Enums.Status.InProcess;
            }
        }
    }

    //The function constructs an object of type BO.Order:
    private BO.Order BuildOrder(BO.Order BOorder, DO.Order DOorder, int orderId)
    {
        BOorder.m_Id = DOorder.m_ID;
        BOorder.m_CustomerName = DOorder.m_CustomerName;
        BOorder.m_CustomerMail = DOorder.m_CustomerEmail;
        BOorder.m_CustomerAdress = DOorder.m_CustomerAdress;
        BOorder.m_OrderTime = DOorder.m_OrderTime;
        BOorder.m_DeliveryrDate = DOorder.m_DeliveryrDate;
        BOorder.m_ShipDate = DOorder.m_ShipDate;
        BOorder.m_OrderStatus = status(DOorder);
        BO.OrderItem BOorderItem = new BO.OrderItem();
        IEnumerable<DO.OrderItem> DOorderItems = Dal.OrderItem.GetOrderItems(orderId);//List of current order details.
        foreach (DO.OrderItem item in DOorderItems)
        { //The loop inserts data into a BOorder order details array.
            BOorderItem.m_ID = item.m_ID;
            BOorderItem.m_IdProduct = item.m_ProductId;
            try
            {
                BOorderItem.m_NameProduct = Dal.Product.GetbyID(item.m_ProductId).m_Name;
                BOorderItem.m_PriceProduct = Dal.Product.GetbyID(item.m_ProductId).m_Price;
            }
            catch
            { throw new BO.MissingID(); }
            BOorderItem.m_AmountInCart = item.m_amount;
            BOorderItem.m_TotalPriceItem = item.m_Price;
            BOorder.m_TotalPrice = BOorder.m_TotalPrice + item.m_Price;
            BOorder.m_orderItems?.Add(BOorderItem);
        }
        return BOorder;
    }

    // The function builds a new order list of the OrderForList type (for the manager screen):
    public IEnumerable<BO.OrderForList> OrderList()
    {
        IEnumerable<DO.Order> orders = Dal.Order.Get(); //A new list of type Orde
        List<BO.OrderForList> orderList = new List<BO.OrderForList>(); //A new list of type OrderForList
        BO.OrderForList orderForList = new BO.OrderForList(); //A new object of type Order For List
        foreach (DO.Order order in orders) //The loop goes through the entire order list.
        {
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetOrderItems(order.m_ID); //List of order details of the current order.
            foreach (DO.OrderItem item in orderItems)
            { //The loop goes through the order details list of the current order:
                orderForList.m_AmountItems = orderForList.m_AmountItems + item.m_amount;
                orderForList.m_TotalPrice = orderForList.m_TotalPrice + item.m_Price;
            }
            orderForList.m_CustomerName = order.m_CustomerName;
            orderForList.m_Id = order.m_ID;
            orderForList.m_OrderStatus = status(order);//Updates the status of the order.

            orderList.Add(orderForList);
        }
        return orderList;
    }

    // The function receives an order ID and returns an object of type BO.Order that contains all the order details.
    public BO.Order orderDetails(int orderId)
    {
        if (orderId < 0)
            throw new BO.incorrectData();
        DO.Order DOorder = new DO.Order();
        try//בדיקה אם מזהה הזמנה תקין
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); }
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    // The function allows the manager to update that the order has been sent to the customer.
    public BO.Order sendingAnInvitation(int orderId)
    {
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); }
        if (DOorder.m_ShipDate > DateTime.Now)//If the order has already been sent, throw that the order has already been sent.
            throw new BO.incorrectData();
        DOorder.m_ShipDate = DateTime.Now;
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    //The function allows the manager to update that the order has been delivered to the customer.
    public BO.Order orderDeliveryint(int orderId)
    {
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); }
        if (DOorder.m_ShipDate > DateTime.Now && DOorder.m_DeliveryrDate < DateTime.Now)
            throw new BO.incorrectData();//If the order has already been delivered, throw that the order has been delivered.
        DOorder.m_DeliveryrDate = DateTime.Now;
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    // The function allows the administrator to track the order:
    public BO.OrderTracking orderTracking(int orderId)
    {
        BO.OrderTracking OT = new BO.OrderTracking();
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); }
        OT.m_Status = status(DOorder);
        OT.m_ID = orderId;
        if(OT.m_Status== BO.Enums.Status.Received)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_ShipDate.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_DeliveryrDate.ToString());
            OT.m_DescriptionProgress?.Add("The order has been delivered");
        }
        if (OT.m_Status == BO.Enums.Status.Shipped)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_ShipDate.ToString());
            OT.m_DescriptionProgress?.Add("The order is sent");
        }
        if (OT.m_Status == BO.Enums.Status.Ordered)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add("The order has been created");
        }
        return OT;
    }
}
