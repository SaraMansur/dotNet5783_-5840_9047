

namespace BlApi;

public interface IBoCart
{
    /// <summary>
    /// The function adds a product to the shopping cart (for the catalog screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public BO.Cart AddItemToCart(BO.Cart cart, int ID);

    /// <summary>
    /// The function updates the quantity of a product in the shopping basket (for the cart screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BO.Cart UpdateAmount(BO.Cart cart, int ID, int amount);

    /// <summary>
    /// The function places an order (for the shopping cart screen or the order completion screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="nameCustomr"></param>
    /// <param name="mailCustomer"></param>
    /// <param name="addressCustomr"></param>
    public void OrderConfirmation(BO.Cart cart,string nameCustomr,string mailCustomer, string addressCustomr);  
}
