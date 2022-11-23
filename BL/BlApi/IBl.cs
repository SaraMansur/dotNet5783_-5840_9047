using DalApi;

namespace BlApi;

public interface IBl
{
    /// <summary>
    /// 
    /// </summary>
    public ICart Cart { get; }

    /// <summary>
    /// 
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// 
    /// </summary>
    public IProduct Product { get; }
}
