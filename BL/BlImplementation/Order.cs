using BlApi;
using BO;
using DO;
using System;
using System.Runtime.Intrinsics.Arm;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

   
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
    private BO.Order BuildOrder(BO.Order? BOorder, DO.Order? DOorder, int? orderId)
    {
        List<BO.OrderItem> orderItems = new List<BO.OrderItem>();
        IEnumerable<DO.OrderItem?> DOorderItems = Dal!.OrderItem.Get(x => x?.m_OrderId == orderId);//List of current order details.
        foreach (DO.OrderItem item in DOorderItems)
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
            BOorder.m_TotalPrice = BOorder.m_TotalPrice + item.m_Price;
            orderItems.Add(BOorderItem);
        }
        DO.Order order = (DO.Order)DOorder;
        return new BO.Order() //Fast boot:
        {
            m_Id = order.m_ID,
            m_CustomerName = order.m_CustomerName,
            m_CustomerMail = order.m_CustomerEmail,
            m_CustomerAdress = order.m_CustomerAdress,
            m_OrderTime = order.m_OrderTime,
            m_DeliveryrDate = order.m_DeliveryrDate,
            m_ShipDate = order.m_ShipDate,
            m_OrderStatus = status(order),
            m_TotalPrice = BOorder.m_TotalPrice,
            m_orderItems = new(orderItems.ToList())
        };
    }

    /// <summary>
    /// The function builds a new order list of the OrderForList type (for the manager screen):
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> OrderList()
    {
        IEnumerable<DO.Order?> DoOrders = Dal!.Order.Get(); //A new list of type Orde
        List<BO.OrderForList> ListorderForList = new List<BO.OrderForList>(); //A new list of type OrderForList
        foreach (DO.Order order in DoOrders) //The loop goes through the entire order list.
        {
            int amount = 0; double price = 0;
            IEnumerable<DO.OrderItem?> orderItems = Dal.OrderItem.Get(x => x?.m_OrderId == order.m_ID); //List of order details of the current order.
            foreach (DO.OrderItem item in orderItems)
            { //The loop goes through the order details list of the current order:
                amount += item.m_amount;
                price += item.m_Price;
            }
            ListorderForList.Add(new BO.OrderForList { m_CustomerName = order.m_CustomerName, m_Id = order.m_ID, m_OrderStatus = status(order), m_AmountItems = amount, m_TotalPrice = price });
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
    public BO.Order orderDetails(int? orderId)
    {
        orderId = orderId ?? throw new ArgumentNull();
        if (orderId < 0)
            throw new BO.IlegalInput(); //Negative ID number - wrong
        DO.Order DOorder = new DO.Order();
        try//Checking if Order ID is correct
        {
            DOorder = (DO.Order)Dal.Order.GetSingle(x => x?.m_ID == orderId);
        }
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
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
    public BO.Order sendingAnInvitation(int? orderId)
    {
        orderId = orderId ?? throw new ArgumentNull();

        DO.Order DOorder = new DO.Order();
        try { DOorder = (DO.Order)Dal.Order.GetSingle((x => x?.m_ID == orderId)); } //Checking if Order ID is correct
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
        if (DOorder.m_ShipDate > DateTime.MinValue)//If the order has already been sent, 
            throw new BO.IlegalInput(); //throw that the order has already been sent.
        DOorder.m_ShipDate = DateTime.Now;
        try { Dal.Order.Update(DOorder); }
        catch (Exception inner) { throw new FaildUpdating(inner); }
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
    public BO.Order orderDelivery(int? orderId)
    {
        orderId = orderId ?? throw new ArgumentNull();

        DO.Order DOorder = new DO.Order();//Checking if Order ID is correct 
        try { DOorder = (DO.Order)Dal.Order.GetSingle((x => x?.m_ID == orderId)); } //Checking if Order ID is correct
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
        if (DOorder.m_DeliveryrDate > DateTime.MinValue || DOorder.m_ShipDate == null)
            throw new BO.IlegalInput();//If the order has already been delivered, throw that the order has been delivered.
        DOorder.m_DeliveryrDate = DateTime.Now;
        try { Dal.Order.Update(DOorder); }
        catch (Exception inner) { throw new FaildUpdating(inner); }
        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);
        return BOorder;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNull"></exception>
    public BO.Order changeOrder(int? orderId, int? productId, int? amount)
    {
        DO.Product product = new DO.Product();//A new product is released.
        DO.Order DOorder = new DO.Order();//Checking if Order ID is correct 
        DO.OrderItem Oitem = new DO.OrderItem();

        try
        {
            if (amount < 0) throw new("Please enter a positive update amount.");
            orderId = orderId ?? throw new ArgumentNull();
            productId = productId ?? throw new ArgumentNull();
            amount = amount ?? throw new ArgumentNull();

            product = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == productId);
            DOorder = (DO.Order)Dal.Order.GetSingle((x => x?.m_ID == orderId));
        }
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number

        BO.Order BOorder = new BO.Order();
        BOorder = BuildOrder(BOorder, DOorder, orderId);

        if (DOorder.m_ShipDate != DateTime.MinValue)
            throw new("The order has been sent, so it is not possible to update the quantity of items.");

        try { Oitem = (DO.OrderItem)Dal.OrderItem.GetSingle(x => (x?.m_ProductId == productId) && (x?.m_OrderId == orderId)); }
        catch (Exception inner) {
            Oitem = new DO.OrderItem { m_amount = (int)amount, m_OrderId = (int)orderId, m_Price = (int)amount * product.m_Price, m_ProductId = product.m_ID };
            Oitem.m_ID = Dal.OrderItem.Add(Oitem);
            BOorder.m_orderItems.Add(new BO.OrderItem { m_AmountInCart= (int)amount ,m_ID= Oitem.m_ID ,m_IdProduct= product.m_ID ,m_NameProduct= product.m_Name,m_PriceProduct= product.m_Price, m_TotalPriceItem= (int)amount * product.m_Price });
            product.m_InStock -= (int)amount;
            BOorder.m_TotalPrice += (int)amount * product.m_Price;
            Dal.Product.Update(product);
            return BOorder;
        }

        if (amount == 0) 
        {
            try { Dal.OrderItem.Delete(Oitem.m_ID); }
            catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
             var item = BOorder.m_orderItems.FirstOrDefault(x => x.m_ID == Oitem.m_ID);
             int index = BOorder.m_orderItems.IndexOf(item);
            BOorder.m_orderItems.Remove(BOorder.m_orderItems[index]);

            product.m_InStock += Oitem.m_amount;
            BOorder.m_TotalPrice -= Oitem.m_Price;
            Dal.Product.Update(product);
            return BOorder;
        }

        try
        {
            if (product.m_InStock < amount)
                throw new BO.MissingInStock();
            BOorder.m_TotalPrice -= (int)(Oitem.m_amount - amount) * product.m_Price;
            product.m_InStock = (int)(product.m_InStock + (Oitem.m_amount - amount));
            Dal.Product.Update(product);
            Oitem = new DO.OrderItem { m_amount = (int)amount, m_ID = Oitem.m_ID, m_OrderId = (int)orderId, m_Price = (int)amount * product.m_Price, m_ProductId = product.m_ID };
            Dal.OrderItem.Update(Oitem);
            var item = BOorder.m_orderItems.FirstOrDefault(x => x.m_ID == Oitem.m_ID);
            int index = BOorder.m_orderItems.IndexOf(item);
            BOorder.m_orderItems.RemoveAt(index);
            BOorder.m_orderItems.Insert(index, new BO.OrderItem { m_AmountInCart = (int)amount, m_ID = Oitem.m_ID, m_IdProduct = product.m_ID, m_NameProduct = product.m_Name, m_PriceProduct = product.m_Price, m_TotalPriceItem = (int)amount * product.m_Price });
        }
        catch (Exception inner) { throw new FaildUpdating(inner); }
        return BOorder;
    }


    /// <summary>
    ///  The function allows the administrator to track the order:
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.incorrectData"></exception>
    public BO.OrderTracking orderTracking(int? orderId)
    {
        orderId = orderId ?? throw new ArgumentNull();

        DO.Order DOorder = new DO.Order();
        try { DOorder = (DO.Order)Dal.Order.GetSingle((x => x?.m_ID == orderId)); } //Checking if Order ID is correct
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
        BO.OrderTracking OT = new BO.OrderTracking { m_ID = orderId ?? throw new BO.IlegalInput(), m_Status = status(DOorder) };
        OT.m_DescriptionProgress = new List<Tuple<string?, DateTime?>>();

        //If the order has been confirmed but not yet sent to the customer:
        if (DOorder.m_OrderTime > DateTime.MinValue)
        { OT.m_DescriptionProgress.Add(new Tuple<string?, DateTime?>("The order has been created", DOorder.m_OrderTime)); }

        //If the order has been sent but not yet delivered to the customer:
        if (DOorder.m_ShipDate > DateTime.MinValue)
        { OT.m_DescriptionProgress.Add(new Tuple<string?, DateTime?>("The order is sent", DOorder.m_ShipDate)); }

        //If the order has already been shipped and delivered to the customer:
        if (DOorder.m_DeliveryrDate > DateTime.MinValue)
        { OT.m_DescriptionProgress.Add(new Tuple<string?, DateTime?>("The order has been delivered", DOorder.m_DeliveryrDate)); }
        return OT;
    }
}

