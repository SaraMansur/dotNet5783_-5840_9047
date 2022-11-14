using DalApi;

namespace BlApi;

public interface IBI
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IOrderItem orderItem { get; }
}
