
using BO;
namespace BlApi;

public interface IBoCart
{
    /// <summary>
    /// The function adds a product to the shopping cart (for the catalog screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    Cart AddItemToCart(Cart cart, int ID);

    /// <summary>
    /// The function updates the quantity of a product in the shopping basket (for the cart screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Cart UpdateAmount(Cart cart, int ID, int amount);

    /// <summary>
    /// The function places an order (for the shopping cart screen or the order completion screen).
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="nameCustomr"></param>
    /// <param name="mailCustomer"></param>
    /// <param name="addressCustomr"></param>
    void OrderConfirmation(Cart cart,string nameCustomr,string mailCustomer, string addressCustomr);  
}
