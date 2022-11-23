using DO;
namespace DalApi;

public interface IDal
{
    /// <summary>
    /// 
    /// </summary>
    IProduct Product { get; }   

    /// <summary>
    /// 
    /// </summary>
    IOrder Order { get; }

    /// <summary>
    /// 
    /// </summary>
    IOrderItem OrderItem { get; }    
}
