using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class BoCart : IBoCart
{
    private IDal Dal = new Dal.DalList();

    //The function checks the correctness of the product ID, if it is wrong it throws an exception.
    private DO.Product cheackId(DO.Product p, int? ID)
    {
        try //Checks if the product exists
        {
            p= Dal.Product.GetbyID(ID);
        }
        catch (Exception)//If the product is not in stock.
        {
            throw new BO.incorrectData();
        }
        return p;
    } 

    // The function adds a product to the shopping cart (for the catalog screen):
    public BO.Cart AddItemToCart(BO.Cart cart, int ID)
    {
        DO.Product product = new DO.Product();//A new product is released.
        product= cheackId(product, ID); //Product ID integrity check.
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        { 
            if (cart.m_orderItems[i].m_ID == ID)//If the product exists
            { 
                if ((cart.m_orderItems[i].m_AmountInCart + 1) < product.m_InStock)
                { //If the product exists but there is not enough in the shopping basket
                    throw new BO.incorrectData();
                }
                cart.m_orderItems[i].m_AmountInCart = cart.m_orderItems[i].m_AmountInCart + 1; //The quantity of the item plus one.
                cart.m_orderItems[i].m_TotalPriceItem = cart.m_orderItems[i].m_TotalPriceItem + product.m_Price;  //Total price of the item
                cart.m_TotalPrice = cart.m_TotalPrice + product.m_Price; //Total price update of the shopping curt.
                return cart; //Returning an updated shopping basket.
            }
        }
        //If the product is not yet in the cart:
        BO.OrderItem orderItem = new BO.OrderItem();
        orderItem.m_TotalPriceItem = product.m_Price;
        orderItem.m_AmountInCart = 1;
        orderItem.m_NameProduct = product.m_Name;
        orderItem.m_PriceProduct = product.m_Price;
        cart.m_orderItems?.Add(orderItem);//If the product does not exist, we will add it to the product list.
        cart.m_TotalPrice = cart.m_TotalPrice + product.m_Price;//Total price update of the shopping curt.
        return cart; //Returning an updated shopping basket.
    }

    // The function updates the quantity of a product in the shopping basket (for the cart screen)
    public BO.Cart UpdateAmount(BO.Cart cart, int ID, int amount)
    {
        DO.Product product = new DO.Product();//A new product is released.
        product = cheackId(product, ID);//Product ID integrity check.
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        {
            if (cart.m_orderItems[i].m_ID == ID)//If the product exists
            {
                if ((cart.m_orderItems[i].m_AmountInCart + amount) < product.m_InStock)
                { //If the product exists but there is not enough in the shopping basket
                    throw new BO.incorrectData(); 
                }
                if (amount == 0)
                {//If the buyer wants to remove the product from the cart.
                    cart.m_TotalPrice = cart.m_TotalPrice - cart.m_orderItems[i].m_TotalPriceItem;//Update the cart price minus the product
                    cart.m_orderItems.Remove(cart.m_orderItems[i]);//The missing product from the product list.
                    return cart; //Returning an updated shopping basket.
                }
                cart.m_orderItems[i].m_AmountInCart = cart.m_orderItems[i].m_AmountInCart + amount; //The quantity of the item plus amount that customer wont.
                cart.m_orderItems[i].m_TotalPriceItem = cart.m_orderItems[i].m_TotalPriceItem + (amount * product.m_Price);  //Total price of the item
                cart.m_TotalPrice = cart.m_TotalPrice + (amount * product.m_Price); //Total price update of the shopping curt.
                return cart; //Returning an updated shopping basket.
            }
        }
        throw new BO.MissingID();//Throws an exception if the product does not exist in the shopping cart at all.
    }

    //The function adds an order to the order list and returns an order ID:
    private int addOrder(DO.Order order, string nameCustomr, string mailCustomer, string addressCustomr)
    {
        order.m_CustomerAdress = addressCustomr;
        order.m_CustomerEmail = mailCustomer;
        order.m_CustomerName = nameCustomr;
        order.m_OrderTime = DateTime.Now;
        order.m_DeliveryrDate = DateTime.MinValue;
        order.m_ShipDate = DateTime.MinValue;
        return Dal.Order.Add(order);
    }

    // The function places an order (for the shopping cart screen or the order completion screen):
    public void OrderConfirmation(BO.Cart cart, string nameCustomr, string mailCustomer, string addressCustomr)
    {
        for (int i = 0; i < cart.m_orderItems?.Count; i++) //The loop checks data integrity.
        {
            DO.Product product = new DO.Product();//A new product is released.
            product = cheackId(product, cart.m_orderItems[i].m_IdProduct);//Product ID integrity check.
            if (cart.m_orderItems[i].m_AmountInCart > product.m_InStock || cart.m_orderItems[i].m_AmountInCart <= 0) 
                throw new BO.incorrectData(); //If the quantity in the basket is greater than the quantity in stock or negative.
            if (!mailCustomer.EndsWith("@gmail.com") || nameCustomr.Length == 0 || mailCustomer.Length == 0 || addressCustomr.Length == 0)
                throw new BO.incorrectData(); //Throwing an exception in case one or more of the details is wrong.
        }
        DO.Order order = new DO.Order();//A new order is released.
        order.m_ID = addOrder(order, nameCustomr, mailCustomer, addressCustomr);
        for (int i = 0; i < cart.m_orderItems?.Count; i++) //The loop adds product details to the list of product details
        {
            DO.OrderItem orderItem = new DO.OrderItem();//A new product is released.
            orderItem.m_amount = cart.m_orderItems[i].m_AmountInCart;
            orderItem.m_OrderId = order.m_ID;
            orderItem.m_Price = cart.m_orderItems[i].m_TotalPriceItem;
            orderItem.m_ProductId = cart.m_orderItems[i].m_IdProduct;
            Dal.OrderItem.Add(orderItem); //Add to list.
            DO.Product p = Dal.Product.GetbyID(orderItem.m_ProductId);
            p.m_InStock = p.m_InStock - orderItem.m_amount;
            Dal.Product.Update(p); //Product inventory update.
        }
    }
}
