using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICustomer
{
    /// <summary>
    /// The function receives a username and password and returns the client.
    /// If it does not exist, an error is thrown.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public BO.Customer CustomerId(string name, int password);

    /// <summary>
    /// A function updates the client in the data layer and returns the updated client.
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public void UpdateCustomer(BO.Customer customer, int num=0);

    /// <summary>
    /// The function returns the list of all customer orders.
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public List<DO.Order?> AllOrder(BO.Customer customer);

    /// <summary>
    /// The function adds a customer to the customer database.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="mail"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public BO.Customer AddCustomer(string name,string address, string mail, int password);
}
