using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// this function cheach if there is enough of the object in stock:
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="inStock"></param>
    /// <returns></returns>
    /// <exception cref="BO.MissingInStock"></exception>
    private bool checkInStock(int? amount, int? inStock)
    {
        if (amount > inStock)
            throw new BO.MissingInStock();
        return true;
    }

    /// <summary>
    /// The function adds a product to the shopping cart (for the catalog screen):
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public BO.Cart AddItemToCart(BO.Cart? cart, int? ID)
    {
        DO.Product product = new DO.Product();//A new product is released.
        cart = cart ?? throw new ArgumentNull(); //cheack that the cart is ligal.

        try { product = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == ID); } //Checks if the product exists
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        {
            if (cart.m_orderItems[i]?.m_IdProduct == ID)//If the product exists
            {
                if (checkInStock((cart.m_orderItems[i]?.m_AmountInCart + 1), product.m_InStock))
                {   //if there is enough of the object in stock:
                    cart.m_orderItems[i]!.m_AmountInCart = cart.m_orderItems[i]!.m_AmountInCart + 1; //The quantity of the item plus one.
                    cart.m_orderItems[i]!.m_TotalPriceItem = cart.m_orderItems[i]!.m_TotalPriceItem + product.m_Price;  //Total price of the item
                    cart.m_TotalPrice = cart.m_TotalPrice + product.m_Price; //Total price update of the shopping curt.
                    return cart; //Returning an updated shopping basket.
                }
            }
        }
        //If the product is not yet in the cart:
        if (checkInStock(1, product.m_InStock))
        {   //if there is enough of the object in stock:
            BO.OrderItem orderItem = new BO.OrderItem() { m_IdProduct = ID ?? throw new BO.IlegalInput(), m_TotalPriceItem = product.m_Price, m_AmountInCart = 1, m_NameProduct = product.m_Name, m_PriceProduct = product.m_Price };
            cart.m_orderItems?.Add(orderItem);//If the product does not exist, we will add it to the product list.
            cart.m_TotalPrice = cart.m_TotalPrice + product.m_Price;//Total price update of the shopping curt.
        }
        return cart; //Returning an updated shopping basket.
    }

    /// <summary>
    /// The function updates the quantity of a product in the shopping basket (for the cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.MissingID"></exception>
    public BO.Cart UpdateAmount(BO.Cart? cart, int? ID, int? amount)
    {
        DO.Product product = new DO.Product();//A new product is released.
        cart = cart ?? throw new ArgumentNull(); //cheack that the cart is ligal.

        try { product = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == ID); } //Checks if the product exists
        catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        {
            if (cart.m_orderItems[i]?.m_IdProduct == ID)//If the product exists
            {
                if (amount < 0) 
                {
                    if((cart.m_orderItems[i]!.m_AmountInCart + amount)<0) throw new BO.IlegalInput();
                    cart.m_orderItems[i]!.m_AmountInCart += amount ?? throw new ArgumentNull(); //The quantity of the item to amount that customer wont.
                    cart.m_TotalPrice = (cart.m_TotalPrice + (cart.m_orderItems[i]!.m_AmountInCart * product.m_Price) - cart.m_orderItems[i]?.m_TotalPriceItem) ?? throw new ArgumentNull(); //Total price update of the shopping curt.
                    cart.m_orderItems[i]!.m_TotalPriceItem = (cart.m_orderItems[i]!.m_AmountInCart * product.m_Price) ;  //Total price of the item
                    return cart; //Returning an updated shopping basket.

                }
                if (amount == 0)
                {//If the customer wants to remove the product from the cart.
                    cart.m_TotalPrice = cart.m_TotalPrice - cart.m_orderItems[i]!.m_TotalPriceItem;//Update the cart price minus the product
                    cart.m_orderItems.Remove(cart.m_orderItems[i]);//The missing product from the product list.
                    return cart; //Returning an updated shopping basket.
                }
                if (checkInStock(amount, product.m_InStock))
                {   //if there is enough of the object in stock:
                    cart.m_orderItems[i]!.m_AmountInCart = amount ?? throw new ArgumentNull(); //The quantity of the item to amount that customer wont.
                    cart.m_TotalPrice = (cart.m_TotalPrice + (amount * product.m_Price) - cart.m_orderItems[i]?.m_TotalPriceItem) ?? throw new ArgumentNull(); //Total price update of the shopping curt.
                    cart.m_orderItems[i]!.m_TotalPriceItem = (amount * product.m_Price) ?? throw new ArgumentNull();  //Total price of the item
                    return cart; //Returning an updated shopping basket.
                }
            }
        }
        throw new BO.NotExist();//Throws an exception if the product does not exist in the shopping cart at all.
    }

    /// <summary>
    /// The function places an order (for the shopping cart screen or the order completion screen):
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="nameCustomr"></param>
    /// <param name="mailCustomer"></param>
    /// <param name="addressCustomr"></param>
    /// <exception cref="BO.incorrectData"></exception>
    public void OrderConfirmation(BO.Cart? cart, string? nameCustomr, string? mailCustomer, string? addressCustomr)
    {
        BO.Cart c = cart ?? throw new ArgumentNull(); //cheack that the cart is ligal.
        if (!mailCustomer!.EndsWith("@gmail.com") || nameCustomr?.Length == 0 || mailCustomer?.Length == 0 || addressCustomr?.Length == 0 )
            throw new BO.IlegalInput(); //Throwing an exception in case one or more of the details is wrong.
        for (int i = 0; i < nameCustomr.Length; i++) {
            if (!(nameCustomr[i]<='z'&& nameCustomr[i] >='a') && (nameCustomr[i]!=' ') && !(nameCustomr[i] <= 'Z' && nameCustomr[i] >= 'A')) 
                throw new BO.IlegalInput(); }

        foreach (var item in cart?.m_orderItems)
        {
            DO.Product product = new DO.Product();//A new product is released.
            try { product = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == item.m_IdProduct); } //Checks if the product exists
            catch (Exception inner) { throw new FaildGetting(inner); } //Throwing in the event of a wrong ID number
            if (!checkInStock(item.m_AmountInCart, product.m_InStock) || item?.m_AmountInCart <= 0)
                throw new BO.IlegalInput(); //If the quantity in the basket is greater than the quantity in stock or negative.
            if (item.m_NameProduct?.Length == 0 || item.m_TotalPriceItem <= 0 || cart?.m_TotalPrice <= 0)
                throw new BO.IlegalInput(); //Throwing an exception in case one or more of the details is wrong.
        }

        ;//A new order is released.
        DO.Order order = new DO.Order() { m_CustomerAdress = addressCustomr, m_CustomerEmail = mailCustomer, m_CustomerName = nameCustomr, m_OrderTime = DateTime.Now, m_DeliveryrDate = null, m_ShipDate = null };
        try
        {
            order.m_ID = Dal!.Order.Add(order);
            for (int i = 0; i < cart?.m_orderItems?.Count; i++) //The loop adds product details to the list of product details
            {
                DO.OrderItem orderItem = new DO.OrderItem() { m_ID = cart.m_orderItems[i]!.m_ID, m_amount = cart.m_orderItems[i]!.m_AmountInCart, m_OrderId = order.m_ID, m_Price = cart.m_orderItems[i]!.m_TotalPriceItem, m_ProductId = cart.m_orderItems[i]!.m_IdProduct };
                Dal.OrderItem.Add(orderItem); //Add to list.
                DO.Product p = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == orderItem.m_ProductId);
                p.m_InStock = p.m_InStock - orderItem.m_amount;
                Dal.Product.Update(p); //Product inventory update.
            }
        }
        catch (Exception inner) { throw new FaildGetting(inner); }
    }
}
