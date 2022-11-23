using BlApi;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// The function returns the status of the order:
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.Enums.Status status(DO.Order order)
    {
        if (order.m_DeliveryrDate > DateTime.MinValue) //If the order has been delivered
            return BO.Enums.Status.Received;
        else //If the order has not yet been delivered
        {
            if (order.m_ShipDate > DateTime.MinValue)//If the order has been shipped
                return BO.Enums.Status.Shipped;
            else//If the order has not yet been sent
            {
                if (order.m_OrderTime > DateTime.MinValue) //If the order is made
                    return BO.Enums.Status.Ordered;
                else //If the order is still in progress
                    return BO.Enums.Status.InProcess;
            }
        } 
    }

    /// <summary>
    /// The function constructs an object of type BO.Order:
    /// </summary>
    /// <param name="BOorder"></param>
    /// <param name="DOorder"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.MissingID"></exception>
    private BO.Order BuildOrder(BO.Order BOorder,DO.Order DOorder, int orderId)
    {
        List<BO.OrderItem> orderItems = new List<BO.OrderItem>();
        IEnumerable<DO.OrderItem> DOorderItems = Dal.OrderItem.GetOrderItems(orderId);//List of current order details.
        foreach (DO.OrderItem item in DOorderItems)
        { //The loop inserts data into a BOorder order details array.
            BO.OrderItem BOorderItem = new BO.OrderItem();
            BOorderItem.m_ID = item.m_ID;
            BOorderItem.m_IdProduct = item.m_ProductId;
            try
            {
                BOorderItem.m_NameProduct = Dal.Product.GetbyID(item.m_ProductId).m_Name;
                BOorderItem.m_PriceProduct = Dal.Product.GetbyID(item.m_ProductId).m_Price;
            }
            catch
            { throw new BO.MissingID(); } //Throwing in the event of a wrong ID number
            BOorderItem.m_AmountInCart = item.m_amount;
            BOorderItem.m_TotalPriceItem = item.m_Price;
            BOorder.m_TotalPrice = BOorder.m_TotalPrice + item.m_Price;
            orderItems.Add(BOorderItem);
        }
        return new BO.Order() //Fast boot:
        {
            m_Id = DOorder.m_ID,
            m_CustomerName = DOorder.m_CustomerName,
            m_CustomerMail = DOorder.m_CustomerEmail,
            m_CustomerAdress = DOorder.m_CustomerAdress,
            m_OrderTime = DOorder.m_OrderTime,
            m_DeliveryrDate = DOorder.m_DeliveryrDate,
            m_ShipDate = DOorder.m_ShipDate,
            m_OrderStatus = status(DOorder),
            m_TotalPrice = BOorder.m_TotalPrice,
            m_orderItems = new (orderItems.ToList())
        };
    }

    /// <summary>
    /// The function builds a new order list of the OrderForList type (for the manager screen):
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> OrderList()
    {
        IEnumerable<DO.Order> DoOrders = Dal.Order.Get(); //A new list of type Orde
        List<BO.OrderForList> ListorderForList = new List<BO.OrderForList>(); //A new list of type OrderForList
        foreach (DO.Order order in DoOrders) //The loop goes through the entire order list.
        {
            int amount = 0 ; double price = 0 ;   
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetOrderItems(order.m_ID); //List of order details of the current order.
            foreach (DO.OrderItem item in orderItems)
            { //The loop goes through the order details list of the current order:
                amount += item.m_amount;
                price +=  item.m_Price;
            }
            ListorderForList.Add(new BO.OrderForList { m_CustomerName = order.m_CustomerName, m_Id = order.m_ID, m_OrderStatus = status(order), m_AmountItems=amount, m_TotalPrice=price }); 
        }
        return ListorderForList;
    }

    /// <summary>
    /// The function receives an order ID and returns an object of type BO.Order that contains all the order details.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.incorrectData"></exception>
    /// <exception cref="BO.MissingID"></exception>
    public BO.Order orderDetails(int orderId)
    {
        if (orderId < 0)
            throw new BO.incorrectData(); //Negative ID number - wrong
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.MissingID(); } //Throwing in the event of a wrong ID number
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    /// <summary>
    /// The function allows the manager to update that the order has been sent to the customer.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.incorrectData"></exception>
    public BO.Order sendingAnInvitation(int orderId)
    {
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); } //Throwing in the event of a wrong ID number
        if (DOorder.m_ShipDate > DateTime.Now)//If the order has already been sent, 
            throw new BO.incorrectData(); //throw that the order has already been sent.
        DOorder.m_ShipDate = DateTime.Now;
        Dal.Order.Update(DOorder);
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    /// <summary>
    /// The function allows the manager to update that the order has been delivered to the customer.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.incorrectData"></exception>
    public BO.Order orderDelivery(int orderId)
    {
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = Dal.Order.GetbyID(orderId);
        }
        catch
        { throw new BO.incorrectData(); }
        if (DOorder.m_DeliveryrDate > DateTime.Now)
            throw new BO.incorrectData();//If the order has already been delivered, throw that the order has been delivered.
        DOorder.m_DeliveryrDate = DateTime.Now;
        Dal.Order.Update(DOorder);
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }

    /// <summary>
    ///  The function allows the administrator to track the order:
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.incorrectData"></exception>
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
        OT.m_Status = status(DOorder); OT.m_ID = orderId;
        OT.m_DescriptionProgress = new List<string>();
        //If the order has already been shipped and delivered to the customer:
        if (OT.m_Status== BO.Enums.Status.Received)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_ShipDate.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_DeliveryrDate.ToString());
            OT.m_DescriptionProgress?.Add("The order has been delivered");
        }
        //If the order has been sent but not yet delivered to the customer:
        if (OT.m_Status == BO.Enums.Status.Shipped)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add(DOorder.m_ShipDate.ToString());
            OT.m_DescriptionProgress?.Add("The order is sent");
        }
        //If the order has been confirmed but not yet sent to the customer:
        if (OT.m_Status == BO.Enums.Status.Ordered)
        {
            OT.m_DescriptionProgress?.Add(DOorder.m_OrderTime.ToString());
            OT.m_DescriptionProgress?.Add("The order has been created");
        }
        return OT;
    }
}
