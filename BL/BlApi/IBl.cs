using BlImplementation;
using DalApi;

namespace BlApi;

/// <summary>
/// A main interface named IBl that centers all the interfaces of the layer
/// </summary>
public interface IBl
{
    /// <summary>
    /// interface of Cart
    /// </summary>
    public ICart Cart { get; }

    /// <summary>
    /// interface of Order
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// interface of Product
    /// </summary>
    public IProduct Product { get; }

    public ICustomer Customer { get; }

}
