﻿using BlApi;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// The function checks the correctness of the product ID, if it is wrong it throws an exception:
    /// </summary>
    /// <param name="p"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="BO.MissingID"></exception>
    private DO.Product cheackId(DO.Product p, int? ID)
    {
        try //Checks if the product exists
        {
            p= Dal.Product.GetbyID(ID);
        }
        catch (Exception)//If the product is not in stock.
        {
            throw new BO.MissingID();
        }
        return p;
    }

    /// <summary>
    /// this function cheach if there is enough of the object in stock:
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="inStock"></param>
    /// <returns></returns>
    /// <exception cref="BO.MissingInStock"></exception>
    private bool checkInStock(int? amount,int? inStock)
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
    public BO.Cart AddItemToCart(BO.Cart cart, int ID)
    {
        DO.Product product = new DO.Product();//A new product is released.
        product = cheackId(product, ID); //Product ID integrity check.
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        { 
            if (cart.m_orderItems[i].m_IdProduct == ID)//If the product exists
            { 
                if (checkInStock((cart.m_orderItems[i].m_AmountInCart + 1),product.m_InStock))
                {   //if there is enough of the object in stock:
                    cart.m_orderItems[i].m_AmountInCart = cart.m_orderItems[i].m_AmountInCart + 1; //The quantity of the item plus one.
                    cart.m_orderItems[i].m_TotalPriceItem = cart.m_orderItems[i].m_TotalPriceItem + product.m_Price;  //Total price of the item
                    cart.m_TotalPrice = cart.m_TotalPrice + product.m_Price; //Total price update of the shopping curt.
                    return cart; //Returning an updated shopping basket.
                }
            }
        }
        //If the product is not yet in the cart:
        if(checkInStock(1,product.m_InStock)) //
        {   //if there is enough of the object in stock:
            BO.OrderItem orderItem = new BO.OrderItem() { m_IdProduct = ID, m_TotalPriceItem = product.m_Price, m_AmountInCart = 1, m_NameProduct = product.m_Name, m_PriceProduct = product.m_Price };
            cart.m_orderItems.Add(orderItem);//If the product does not exist, we will add it to the product list.
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
    public BO.Cart UpdateAmount(BO.Cart cart, int ID, int amount)
    {
        DO.Product product = new DO.Product();//A new product is released.
        product = cheackId(product, ID);//Product ID integrity check.
        for (int i = 0; i < cart.m_orderItems?.Count; i++)//The loop checks if the product is in the shopping cart.
        {
            if (cart.m_orderItems[i].m_IdProduct == ID)//If the product exists
            {
                if (amount == 0)
                {//If the customer wants to remove the product from the cart.
                    cart.m_TotalPrice = cart.m_TotalPrice - cart.m_orderItems[i].m_TotalPriceItem;//Update the cart price minus the product
                    cart.m_orderItems.Remove(cart.m_orderItems[i]);//The missing product from the product list.
                    return cart; //Returning an updated shopping basket.
                }
                if (checkInStock( amount, product.m_InStock))
                {   //if there is enough of the object in stock:
                    cart.m_orderItems[i].m_AmountInCart =  amount; //The quantity of the item to amount that customer wont.
                    cart.m_TotalPrice = cart.m_TotalPrice + (amount * product.m_Price)- cart.m_orderItems[i].m_TotalPriceItem; //Total price update of the shopping curt.
                    cart.m_orderItems[i].m_TotalPriceItem = (amount * product.m_Price);  //Total price of the item
                    return cart; //Returning an updated shopping basket.
                }
            }
        }
        throw new BO.MissingID();//Throws an exception if the product does not exist in the shopping cart at all.
    }


    /// <summary>
    /// The function places an order (for the shopping cart screen or the order completion screen):
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="nameCustomr"></param>
    /// <param name="mailCustomer"></param>
    /// <param name="addressCustomr"></param>
    /// <exception cref="BO.incorrectData"></exception>
    public void OrderConfirmation(BO.Cart cart, string nameCustomr, string mailCustomer, string addressCustomr)
    {
        if (!mailCustomer.EndsWith("@gmail.com") || nameCustomr.Length == 0 || mailCustomer.Length == 0 || addressCustomr.Length == 0)
            throw new BO.incorrectData(); //Throwing an exception in case one or more of the details is wrong.
        for (int i = 0; i < cart.m_orderItems?.Count; i++) //The loop checks data integrity.
        {
            DO.Product product = new DO.Product();//A new product is released.
            product = cheackId(product, cart.m_orderItems[i].m_IdProduct);//Product ID integrity check.
            if (!checkInStock(cart.m_orderItems[i].m_AmountInCart, product.m_InStock) || cart.m_orderItems[i].m_AmountInCart <= 0) 
                throw new BO.incorrectData(); //If the quantity in the basket is greater than the quantity in stock or negative.
            if (cart.m_orderItems[i].m_NameProduct?.Length==0|| cart.m_orderItems[i].m_TotalPriceItem<=0||cart.m_TotalPrice<=0)
                throw new BO.incorrectData(); //Throwing an exception in case one or more of the details is wrong.
        }
        ;//A new order is released.
        DO.Order order = new DO.Order() { m_CustomerAdress = addressCustomr, m_CustomerEmail = mailCustomer, m_CustomerName = nameCustomr, m_OrderTime = DateTime.Now, m_DeliveryrDate = DateTime.MinValue, m_ShipDate = DateTime.MinValue };
        order.m_ID = Dal.Order.Add(order);
        for (int i = 0; i < cart.m_orderItems?.Count; i++) //The loop adds product details to the list of product details
        {
            DO.OrderItem orderItem = new DO.OrderItem() { m_ID = cart.m_orderItems[i].m_ID, m_amount = cart.m_orderItems[i].m_AmountInCart, m_OrderId = order.m_ID, m_Price = cart.m_orderItems[i].m_TotalPriceItem, m_ProductId = cart.m_orderItems[i].m_IdProduct };
            Dal.OrderItem.Add(orderItem); //Add to list.
            DO.Product p = Dal.Product.GetbyID(orderItem.m_ProductId);
            p.m_InStock = p.m_InStock - orderItem.m_amount;
            Dal.Product.Update(p); //Product inventory update.
        }
    }
}